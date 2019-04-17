using Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Data.Base;

namespace POC_ORM_NetCore
{
    internal class Program
    {
        private static List<long> _results;
        private static Stopwatch sw;

        private static void Main(string[] args)
        {
            Console.WriteLine("Creating Database");
            new Migrations().CreateTables();

            Console.WriteLine("Initializing Tests");
            Console.WriteLine("Test Number 1: SELECT * FROM Person");
            Console.WriteLine("-------------------------------------");

            Console.WriteLine("Dapper");
            StartTestDapper();

            //Console.WriteLine("ADO");s
            //StartTestAdo();

            Console.WriteLine("EntityFramework");
            StartTestEf();

            Console.WriteLine("NHibernate");
            StartTestNHibernate();
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

        private static void StartTestAdo()
        {
            var objAdo = new Data.ADO.PersonRepository();
            sw = new Stopwatch();

            objAdo.WarmUp();

            _results = new List<long>();

            for (int i = 0; i < 20; i++)
            {
                sw.Start();
                var ret = objAdo.GetAllPerson();
                sw.Stop();
                _results.Add(sw.ElapsedMilliseconds);
                Console.WriteLine(sw.ElapsedMilliseconds + "ms");
                sw.Reset();
            }
            Console.WriteLine($"Average: {_results.Average()}");
        }

        private static void StartTestEf()
        {
            sw = new Stopwatch();

            _results = new List<long>();

            //WarmUp

            using (var context = new DataContext())
            {
                context.Person.FirstOrDefault();
            }

            for (int i = 0; i < 20; i++)
            {
                sw.Start();

                using (var context = new DataContext())
                {
                    var ret = context.Person.ToArray();
                    sw.Stop();
                    _results.Add(sw.ElapsedMilliseconds);
                    Console.WriteLine(sw.ElapsedMilliseconds + "ms");
                    sw.Reset();
                }
            }
            Console.WriteLine($"Average: {_results.Average()}");
        }

        private static void StartTestNHibernate()
        {
            var objNh = new Data.NHibernate.PersonRepository();
            sw = new Stopwatch();

            _results = new List<long>();

            for (int i = 0; i < 20; i++)
            {
                sw.Start();
                var ret = objNh.GetAllPerson();
                sw.Stop();
                _results.Add(sw.ElapsedMilliseconds);
                Console.WriteLine(sw.ElapsedMilliseconds + "ms");
                sw.Reset();
            }

            Console.WriteLine($"Average: {_results.Average()}");
        }
    }
}