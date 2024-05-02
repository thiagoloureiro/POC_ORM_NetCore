using Data.Base;
using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
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
            await new Migrations().CreateTablesAsync();

            Console.WriteLine("Initializing Tests");
            Console.WriteLine("Test Number 1: SELECT * FROM Person");
            Console.WriteLine("-------------------------------------");

            Console.WriteLine("Dapper");
            await StartTestDapperAsync();

            //Console.WriteLine("ADO");s
            //StartTestAdo();

            Console.WriteLine("EntityFramework");
            await StartTestEfAsync();

            Console.WriteLine("NHibernate");
            await StartTestNHibernateAsync();
        }

        private static async Task StartTestDapperAsync()
        {
            var objDapper = new Data.Dapper.PersonRepository();
            sw = new Stopwatch();

            await objDapper.WarmUpAsync();

            _results = new List<long>();

            for (int i = 0; i < 100; i++)
            {
                sw.Start();
                var ret = await objDapper.GetAllPersonAsync();
                sw.Stop();
                _results.Add(sw.ElapsedMilliseconds);
                Console.WriteLine($"{sw.ElapsedMilliseconds} ms Records: {ret.Length}");
                sw.Reset();
            }

            Console.WriteLine($"Average: {_results.Average()}");
        }

        private static async Task StartTestAdoAsync()
        {
            var objAdo = new Data.ADO.PersonRepository();
            sw = new Stopwatch();

            await objAdo.WarmUpAsync();

            _results = new List<long>();

            for (int i = 0; i < 100; i++)
            {
                sw.Start();
                var ret = await objAdo.GetAllPersonAsync();
                sw.Stop();
                _results.Add(sw.ElapsedMilliseconds);
                Console.WriteLine(sw.ElapsedMilliseconds + "ms");
                sw.Reset();
            }
            Console.WriteLine($"Average: {_results.Average()}");
        }

        private static async Task StartTestEfAsync()
        {
            sw = new Stopwatch();

            _results = new List<long>();

            //WarmUp

            await using (var context = new DataContext())
            {
                var x = await context.Person.FirstOrDefaultAsync();
            }

            for (int i = 0; i < 100; i++)
            {
                sw.Start();

                await using (var context = new DataContext())
                {
                    var ret = await context.Person.ToArrayAsync();
                    sw.Stop();
                    _results.Add(sw.ElapsedMilliseconds);
                    Console.WriteLine(sw.ElapsedMilliseconds + "ms");
                    sw.Reset();
                }
            }
            Console.WriteLine($"Average: {_results.Average()}");
        }

        private static async Task StartTestNHibernateAsync()
        {
            var objNh = new Data.NHibernate.PersonRepository();
            sw = new Stopwatch();

            _results = new List<long>();

            for (int i = 0; i < 100; i++)
            {
                sw.Start();
                var ret = await objNh.GetAllPersonAsync();
                sw.Stop();
                _results.Add(sw.ElapsedMilliseconds);
                Console.WriteLine(sw.ElapsedMilliseconds + "ms");
                sw.Reset();
            }

            Console.WriteLine($"Average: {_results.Average()}");
        }
    }
}