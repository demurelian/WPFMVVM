using System;
using System.Threading;

namespace WMConsole
{
    class Program
    {
        public static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main thread";

            var thread = new Thread(ThreadMethod);
            thread.Name = "Other thread";
            thread.Start();

            CheckThread();
            
            for(int i = 0; i < 100; i++)
            {
                Console.WriteLine(i);
                Thread.Sleep(100);
            }
        }

        private static void ThreadMethod()
        {
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