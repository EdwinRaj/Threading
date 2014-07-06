using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadshutdownChoreography
{
    class Program
    {
        private static volatile bool _cancel = false;
        static void Main(string[] args)
        {
            var newThread = new Thread(WorkerMethod);
            newThread.Start();

            Console.WriteLine("Worker thread started");
            Console.WriteLine("Press ENTER to abort");
            Console.Read();

            _cancel = true;
            newThread.Join();
            //NOTE: Bad practice to use IsAlive property
            //while (newThread.IsAlive)
            //{
            //    Console.WriteLine("Waiting for worker to stop");
            //}
            Console.WriteLine("Shutting down");
            Console.Read();
        }

        private static void WorkerMethod()
        {
            int seconds = 1;
            while (!_cancel)
            {
                Thread.Sleep(1000);
                Console.WriteLine("I am working for {0} seconds",seconds++);
                
            }
        }
    }
}
