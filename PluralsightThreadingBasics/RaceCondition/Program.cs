using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaceCondition
{
    class Program
    {
        static int _sum = 0;
        static void Main(string[] args)
        {
            var threads = new Thread[10];
            for (int i = 0; i < 10; i++)
            {
                threads[i] = new Thread(AddOne);
                threads[i].Start();
            }

            for (int i = 0; i < 10; i++)
            {
                threads[i].Join();
            }

            Console.WriteLine("Sum = "+_sum);
            Console.ReadLine();
        }

        static void AddOne()
        {
            Console.WriteLine("[{0}] Addone Worker called",Thread.CurrentThread.ManagedThreadId);
           
            //Race Condition Simulation
            //int temp = _sum;
            //temp++;
            //Thread.Sleep(10);
            //_sum = temp;

            //Avoiding Race Condition
            Interlocked.Increment(ref _sum);
        }
    }
}
