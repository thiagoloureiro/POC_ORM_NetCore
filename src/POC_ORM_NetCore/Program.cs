using Data.Base;
using Data.EntityFramework;
using Model;
using RandomTestValues;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace POC_ORM_NetCore
{
    internal class Program
    {
        private static List<long> _results;
        private static Stopwatch sw;

        private static async Task Main(string[] args)
        {
            Console.WriteLine("Creating Database");
            new Migrations().CreateTables();

            Console.WriteLine("Initializing Tests");
            Console.WriteLine("Test Number 1: SELECT * FROM Person");
            Console.WriteLine("-------------------------------------");
            //
            //  Console.WriteLine("Dapper");
            //  StartTestDapper();

            // Console.WriteLine("ADO");
            // await StartTestAdo().ConfigureAwait(false);

            Console.WriteLine("ADO");
            await StartTestAdoDummy().ConfigureAwait(false);

            //Console.WriteLine("EntityFramework");
            //await StartTestEf();

            // Console.WriteLine("NHibernate");
            ///  await StartTestNHibernate();
        }

        private static void StartTestDapper()
        {
            var objDapper = new Data.Dapper.PersonRepository();
            sw = new Stopwatch();

            objDapper.WarmUp();

            _results = new List<long>();

            for (int i = 0; i < 20; i++)
            {
                sw.Start();
                var ret = objDapper.GetAllPerson();
                sw.Stop();
                _results.Add(sw.ElapsedMilliseconds);
                Console.WriteLine(sw.ElapsedMilliseconds + "ms");
                sw.Reset();
            }

            Console.WriteLine($"Average: {_results.Average()}");
        }

        private static async Task StartTestAdo()
        {
            var objAdo = new Data.ADO.PersonRepository();

            objAdo.WarmUp();

            //_results = new List<long>();

            //for (int i = 0; i < 20; i++)
            //{
            //    sw.Start();
            //    var ret = objAdo.GetAllPerson();
            //    sw.Stop();
            //    _results.Add(sw.ElapsedMilliseconds);
            //    Console.WriteLine(sw.ElapsedMilliseconds + "ms");
            //    sw.Reset();
            //}

            var obj = new List<Person>();
            for (int i = 0; i < 100000; i++)
            {
                var person = RandomValue.Object<Person>();
                obj.Add(person);
            }
            sw = new Stopwatch();
            sw.Start();
            await objAdo.InsertPerson(obj).ConfigureAwait(false);

            Console.WriteLine(sw.Elapsed);
        }

        private static async Task StartTestAdoDummy()
        {
            var objAdo = new Data.ADO.PersonRepository();

            //_results = new List<long>();

            //for (int i = 0; i < 20; i++)
            //{
            //    sw.Start();
            //    var ret = objAdo.GetAllPerson();
            //    sw.Stop();
            //    _results.Add(sw.ElapsedMilliseconds);
            //    Console.WriteLine(sw.ElapsedMilliseconds + "ms");
            //    sw.Reset();
            //}

            var list = new List<DummyRecord>();

            for (int i = 0; i < 500000; i++)
            {
                var rec = new DummyRecord(i);
                list.Add(rec);
            }

            sw = new Stopwatch();
            sw.Start();
            objAdo.InsertDummy(list);

            Console.WriteLine(sw.Elapsed);
        }

        private static async Task StartTestEf()
        {
            sw = new Stopwatch();

            _results = new List<long>();

            //WarmUp

            using (var context = new DataContext())
            {
                context.Person.FirstOrDefault();
            }

            var obj = new List<Person>();
            for (int i = 0; i < 100000; i++)
            {
                var person = RandomValue.Object<Person>();
                obj.Add(person);
            }

            sw = new Stopwatch();
            sw.Start();
            using (var context = new DataContext())
            {
                await context.BulkSaveChangesAsync().ConfigureAwait(false);
                await context.BulkInsertAsync(obj).ConfigureAwait(false);
                //   await context.AddRangeAsync(obj);
                //   await context.SaveChangesAsync();
            }
            Console.WriteLine(sw.Elapsed);

            //for (int i = 0; i < 20; i++)
            //{
            //    sw.Start();

            //    using (var context = new DataContext())
            //    {
            //        var ret = context.Person.ToArray();
            //        sw.Stop();
            //        _results.Add(sw.ElapsedMilliseconds);
            //        Console.WriteLine(sw.ElapsedMilliseconds + "ms");
            //        sw.Reset();
            //    }
            //}
        }

        private static async Task StartTestNHibernate()
        {
            var objNh = new Data.NHibernate.PersonRepository();

            _results = new List<long>();

            //for (int i = 0; i < 20; i++)
            //{
            //    sw.Start();
            //    var ret = objNh.GetAllPerson();
            //    sw.Stop();
            //    _results.Add(sw.ElapsedMilliseconds);
            //    Console.WriteLine(sw.ElapsedMilliseconds + "ms");
            //    sw.Reset();
            //}

            var obj = new List<Person>();
            for (int i = 0; i < 100000; i++)
            {
                var person = RandomValue.Object<Person>();
                person._id = 0;
                obj.Add(person);
            }
            sw = new Stopwatch();
            sw.Start();

            await objNh.WriteToDbAsync2(obj).ConfigureAwait(false);
            Console.WriteLine(sw.Elapsed);
        }
    }
}