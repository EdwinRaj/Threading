using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BackgroundThreadBehaviour
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main Thread Inititated: Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            var newThread = new Thread(WorkerMethod);
            //By setting the below property to true, the thread execution terminates immediately
            //newThread.IsBackground = true;
            newThread.Start();
            Thread.Sleep(1000);
            Console.WriteLine("Main Thread Ended: Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
        }

        private static void WorkerMethod()
        {
            for (int counter = 1; counter <= 5; counter++)
            {
                Thread.Sleep(1000);
                Console.WriteLine("Worker thread - ThreadId {0}, IsBackground={1} Looping {2} seconds", Thread.CurrentThread.ManagedThreadId,
                    Thread.CurrentThread.IsBackground, counter);
            }
        }
    }

}
