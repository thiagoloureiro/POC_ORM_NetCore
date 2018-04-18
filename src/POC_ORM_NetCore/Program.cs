using Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace POC_ORM_NetCore
{
    internal class Program
    {
        private static List<long> _results;
        private static Stopwatch sw;

        private static void Main(string[] args)
        {
            Console.WriteLine("Initializing Tests");
            Console.WriteLine("Test Number 1: SELECT * FROM Messages");
            Console.WriteLine("-------------------------------------");

            Console.WriteLine("Dapper");
            StartTestDapper();

            Console.WriteLine("ADO");
            StartTestAdo();

            Console.WriteLine("EntityFramework");
            StartTestEf();

            Console.WriteLine("NHibernate");
            StartTestNHibernate();

            Console.ReadKey();
        }

        private static void StartTestDapper()
        {
            var objDapper = new Data.Dapper.MessageRepository();
            sw = new Stopwatch();

            objDapper.WarmUp();

            _results = new List<long>();

            for (int i = 0; i < 5; i++)
            {
                sw.Start();
                var ret = objDapper.GetAllMessages();
                sw.Stop();
                _results.Add(sw.ElapsedMilliseconds);
                Console.WriteLine(sw.ElapsedMilliseconds + "ms");
                sw.Reset();
            }

            Console.WriteLine($"Average: {_results.Average()}");
        }

        private static void StartTestAdo()
        {
            var objAdo = new Data.ADO.MessageRepository();
            sw = new Stopwatch();

            objAdo.WarmUp();

            _results = new List<long>();

            for (int i = 0; i < 5; i++)
            {
                sw.Start();
                var ret = objAdo.GetAllMessages();
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
                context.Messages.FirstOrDefault();
            }

            for (int i = 0; i < 5; i++)
            {
                sw.Start();

                using (var context = new DataContext())
                {
                    var ret = context.Messages.ToArray();
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
            var objNH = new Data.NHibernate.MessageRepository();
            sw = new Stopwatch();

            // objDapper.WarmUp();

            _results = new List<long>();

            for (int i = 0; i < 5; i++)
            {
                sw.Start();
                var ret = objNH.GetAllMessages();
                sw.Stop();
                _results.Add(sw.ElapsedMilliseconds);
                Console.WriteLine(sw.ElapsedMilliseconds + "ms");
                sw.Reset();
            }

            Console.WriteLine($"Average: {_results.Average()}");
        }
    }
}