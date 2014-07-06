using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadOperations
{
    public class ThreadInterruptAndAbortExample
    {
        public void Example()
        {
            Console.WriteLine("Main Thread Inititated: Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            var newThread = new Thread(WorkerMethod);
            newThread.Start();
            Console.WriteLine("Main Thread Issuing Abort: Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            newThread.Abort();
            Console.WriteLine("Main Thread Ended: Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
        }

        private void WorkerMethod()
        {
            Console.WriteLine("Spawned Thread Inititated: Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Spawned Thread put to sleep for 10 seconds: Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            int sleepSeconds = 1;
            while (sleepSeconds <= 10)
            {
                Thread.Sleep(sleepSeconds * 1000);
                Console.WriteLine("Spawned Thread - ThreadId {0} slept for {1}", Thread.CurrentThread.ManagedThreadId,sleepSeconds);
                sleepSeconds++;
            }
            Console.WriteLine("Spawned Thread Ended: Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
        }
    }
}
