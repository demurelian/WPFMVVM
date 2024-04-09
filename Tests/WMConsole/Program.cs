using System;
using System.Threading;
using System.Collections.Concurrent;

namespace WMConsole
{
    class Program
    {
        public static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main thread";

            //var thread = new Thread(ThreadMethod);
            //thread.Name = "Other thread";

            //thread.Start(42);

            //var count = 5;
            //var msg = "Hello";
            //var timeout = 250;

            //new Thread(() => PrintMethod(msg, count, timeout)) { IsBackground = true }.Start();

            //CheckThread();

            //for(int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine(i);
            //    Thread.Sleep(100);
            //}

            var values = new List<int>();
            var threads = new Thread[10];

            var lock_object = new object();

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        lock (lock_object)
                            values.Add(Thread.CurrentThread.ManagedThreadId);
                    }
                });
            }

            Monitor.Enter(lock_object);
            try
            {

            }
            finally 
            { 
                Monitor.Exit(lock_object); 
            }
            

            foreach(var thread in threads)
                thread.Start();
            Console.WriteLine(String.Join(",", values));
        }

        private static void PrintMethod(string Message, int Count, int Timeout)
        {
            for (int i = 0; i < Count;i++)
            {
                Console.WriteLine(Message);
                Thread.Sleep(Timeout);
            }
        }

        private static void ThreadMethod(object parametr)
        {
            var value = (int)parametr;
            Console.WriteLine(value);

            CheckThread();

            while(true)
            {
                Thread.Sleep(100);
                Console.Title = DateTime.Now.ToString();
            }
        }

        private static void CheckThread()
        {
            var thread = Thread.CurrentThread;
            Console.WriteLine("{0}:{1}", thread.ManagedThreadId, thread.Name);
        }
    }
}