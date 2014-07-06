using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolDemo
{
    class Program
    {
        private static readonly Random Rand = new Random();

        static void Main(string[] args)
        {
            Console.WriteLine("[{0}] MainThread whose IsBackground:{1} Started", Thread.CurrentThread.ManagedThreadId,
                Thread.CurrentThread.IsBackground);

            for (int request = 1; request < 10; request++)
            {
                ThreadPool.QueueUserWorkItem(WorkerMethod, request);
            }
            
            Thread.Sleep(Rand.Next(500,1000));

            Console.WriteLine("Main thread exited");
        }

        private static void WorkerMethod(object requestId)
        {
            Console.WriteLine("[{0}] ThreadId whose IsBackground:{1} processing the request {2}",
                Thread.CurrentThread.ManagedThreadId
                , Thread.CurrentThread.IsBackground, requestId);
        }
    }
}
