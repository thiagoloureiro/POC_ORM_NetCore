using Data.Base;
using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Model;

namespace POC_ORM_NetCore
{
    internal class Program
    {
        private static List<long> _resultsDapper;
        private static List<long> _resultsEF;
        private static List<long> _resultsEFRaw;
        private static List<long> _resultsNHibernate;
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

            Console.WriteLine("EntityFramework");
            await StartTestEfAsync();

            Console.WriteLine("EntityFramework Raw");
            await StartTestEfRawAsync();

            Console.WriteLine("NHibernate");
            await StartTestNHibernateAsync();

            double avgDapper = _resultsDapper.Average();
            double avgEF = _resultsEF.Average();
            double avgEFRaw = _resultsEFRaw.Average();
            double avgNHibernate = _resultsNHibernate.Average();

            // Calculate percentage differences
            double diffEF = ((avgEF - avgDapper) / avgDapper) * 100;
            double diffEFRaw = ((avgEFRaw - avgDapper) / avgDapper) * 100;
            double diffNHibernate = ((avgNHibernate - avgDapper) / avgDapper) * 100;

            // Print results
            Console.WriteLine("Average Response Times:");
            Console.WriteLine($"Dapper: {avgDapper} ms");

            if (diffEF < 0)
                Console.WriteLine($"EF: {avgEF} ms ({Math.Round(Math.Abs(diffEF), 2)}% faster than Dapper)");
            else
                Console.WriteLine($"EF: {avgEF} ms ({Math.Round(diffEF, 2)}% slower than Dapper)");

            if (diffEFRaw < 0)
                Console.WriteLine($"EF Raw: {avgEFRaw} ms ({Math.Round(Math.Abs(diffEFRaw), 2)}% faster than Dapper)");
            else
                Console.WriteLine($"EF Raw: {avgEFRaw} ms ({Math.Round(diffEFRaw, 2)}% slower than Dapper)");

            if (diffNHibernate < 0)
                Console.WriteLine($"NHibernate: {avgNHibernate} ms ({Math.Round(Math.Abs(diffNHibernate), 2)}% faster than Dapper)");
            else
                Console.WriteLine($"NHibernate: {avgNHibernate} ms ({Math.Round(diffNHibernate, 2)}% slower than Dapper)");

        }

        private static async Task StartTestDapperAsync()
        {
            var objDapper = new Data.Dapper.PersonRepository();
            sw = new Stopwatch();

            for (int i = 0; i < 3; i++)
            {
                await objDapper.WarmUpAsync();
            }
            
            _resultsDapper = new List<long>();

            for (int i = 0; i < 100; i++)
            {
                sw.Start();
                var ret = await objDapper.GetAllPersonAsync();
                sw.Stop();
                _resultsDapper.Add(sw.ElapsedMilliseconds);
                Console.WriteLine($"{sw.ElapsedMilliseconds} ms");
                sw.Reset();
            }

            Console.WriteLine($"Average: {_resultsDapper.Average()}");
        }

        private static async Task StartTestEfAsync()
        {
            sw = new Stopwatch();

            _resultsEF = new List<long>();

            //WarmUp
            for (int i = 0; i < 3; i++)
            {
                await using (var context = new DataContext())
                {
                    var x = await context.Person.FirstOrDefaultAsync();
                }
            }

            for (int i = 0; i < 100; i++)
            {
                sw.Start();

                await using var context = new DataContext();
                var ret = await context.Person.ToArrayAsync();
                sw.Stop();
                _resultsEF.Add(sw.ElapsedMilliseconds);
                Console.WriteLine(sw.ElapsedMilliseconds + "ms");
                sw.Reset();
            }

            Console.WriteLine($"Average: {_resultsEF.Average()}");
        }

        private static async Task StartTestEfRawAsync()
        {
            sw = new Stopwatch();

            _resultsEFRaw = new List<long>();

            //WarmUp

            for (int i = 0; i < 3; i++)
            {
                await using (var context = new DataContext())
                {
                    var x = await context.Database.SqlQuery<Person>($"select * from Person").ToListAsync();
                }
            }

            for (int i = 0; i < 100; i++)
            {
                sw.Start();

                await using var context = new DataContext();
                var ret = await context.Database.SqlQuery<Person>($"select * from Person").ToListAsync();
                sw.Stop();
                _resultsEFRaw.Add(sw.ElapsedMilliseconds);
                Console.WriteLine(sw.ElapsedMilliseconds + "ms");
                sw.Reset();
            }

            Console.WriteLine($"Average: {_resultsEFRaw.Average()}");
        }

        private static async Task StartTestNHibernateAsync()
        {
            var objNh = new Data.NHibernate.PersonRepository();
            sw = new Stopwatch();

            _resultsNHibernate = new List<long>();

            for (int i = 0; i < 3; i++)
            {
                var ret = await objNh.GetAllPersonAsync();
            }

            for (int i = 0; i < 100; i++)
            {
                sw.Start();
                var ret = await objNh.GetAllPersonAsync();
                sw.Stop();
                _resultsNHibernate.Add(sw.ElapsedMilliseconds);
                Console.WriteLine(sw.ElapsedMilliseconds + "ms");
                sw.Reset();
            }

            Console.WriteLine($"Average: {_resultsNHibernate.Average()}");
        }
    }
}