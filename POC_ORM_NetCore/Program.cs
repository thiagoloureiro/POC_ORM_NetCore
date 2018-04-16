using System;
using System.Diagnostics;

namespace POC_ORM_NetCore
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            StartTestDapper();
        }

        private static void StartTestDapper()
        {
            var objDapper = new Data.Dapper.MessageRepository();
            var sw = new Stopwatch();

            objDapper.WarmUp();

            for (int i = 0; i < 3; i++)
            {
                sw.Start();
                objDapper.GetAllMessages();
                sw.Stop();
                Console.WriteLine(sw.ElapsedMilliseconds + "ms");
                sw.Reset();
            }

            Console.ReadKey();
        }
    }
}