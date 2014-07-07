using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadOperations
{
    internal delegate int BinaryOperation(int x, int y);

    public class DelegateThreadPoolDemo
    {
        private BinaryOperation _operation;
        public void Example()
        {
            Console.WriteLine("[{0}] Main Thread",Thread.CurrentThread.ManagedThreadId);
            _operation = AdditionWorker;
            _operation.BeginInvoke(10, 20, CallBackMethod, null);
            Thread.Sleep(1000);
        }
        /// <summary>
        /// Todo: learn how to call async method
        /// </summary>
        /// <param name="result"></param>
        private void CallBackMethod(IAsyncResult result)
        {
            Console.WriteLine("[{0}] Worker Thread", Thread.CurrentThread.ManagedThreadId);
            int outputBinaryOp = _operation.EndInvoke(result);
            Console.WriteLine(outputBinaryOp);
        }

        private int AdditionWorker(int x, int y)
        {
            return x + y;
        }
    }
}
