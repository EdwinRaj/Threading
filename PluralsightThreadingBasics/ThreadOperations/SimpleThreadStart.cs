using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadOperations
{
    public class SimpleThreadStart
    {
        #region Non-Parameterised Threads
        /// <summary>
        /// Simple thread start demo
        /// </summary>
        public void StartThreadDemo()
        {
            Console.WriteLine("Main Thread Inititated: Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Main Thread Inititated: Processor Count {0}", Environment.ProcessorCount);
            var simpleThread = new Thread(PrintMessage) { Name = "Life Thread", Priority = ThreadPriority.BelowNormal };
            simpleThread.Start();
            Console.WriteLine("Main Thread Ended: Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
        }

        private void PrintMessage()
        {
            Console.WriteLine("Life Thread Initiated: Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("Life is beautiful");
            Console.WriteLine("Life Thread Ended: Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
        }
        #endregion Non-Parameterised Threads
    }
}
