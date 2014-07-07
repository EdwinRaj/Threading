using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumerWaitSyn
{
    class Program
    {
        private delegate Donut GetDonutHandler();
        static void Main(string[] args)
        {
            var donutConsumerThreads = new GetDonutHandler[6];
            IAsyncResult[] results = new IAsyncResult[6];

            var myBakery = new Bakery();
            for (int counter = 0; counter < 6; counter++)
            {
                donutConsumerThreads[counter] = new GetDonutHandler(myBakery.GetDonut);
                results[counter] = donutConsumerThreads[counter].BeginInvoke(CallBackMethodDonut, donutConsumerThreads[counter]);

            }

            myBakery.FillDonutTray(new List<Donut>()
                                   {
                                       new Donut{ DonutName = "Donut-1"},
                                        new Donut{ DonutName = "Donut-2"},
                                        new Donut{ DonutName = "Donut-3"},
                                          new Donut{ DonutName = "Donut-4"}
                                   });

            for (int counter = 0; counter < 6; counter++)
            {
                while (!results[counter].IsCompleted)
                {
                    Thread.Sleep(1000);
                }
            }

            Console.WriteLine("Program Ends");
        }

        static void CallBackMethodDonut(IAsyncResult result)
        {
            GetDonutHandler donutHandler = result.AsyncState as GetDonutHandler;
            if (donutHandler != null)
            {
                Donut donut = donutHandler.EndInvoke(result);
                Console.WriteLine("[{0}] thread eats donut{1}",Thread.CurrentThread.ManagedThreadId, donut.DonutName);
            }
        }
    }


    public class Donut
    {
        public string DonutName { get; set; }
    }

    public class Bakery
    {
        readonly Queue<Donut> _donutTray = new Queue<Donut>();

        public Donut GetDonut()
        {
            lock (_donutTray)
            {
                Console.WriteLine("[{0}] Thread acquired Lock", Thread.CurrentThread.ManagedThreadId);
                while (_donutTray.Count == 0)
                {
                    Console.WriteLine("[{0}] Thread Lock is released and put to sleep", Thread.CurrentThread.ManagedThreadId);
                    Monitor.Wait(_donutTray);
                }
                Donut donut = _donutTray.Dequeue();
                Console.WriteLine("[{0}] Thread gets the donut:{1}",Thread.CurrentThread.ManagedThreadId,donut.DonutName);
                return donut;
            }
        }

        public void FillDonutTray(List<Donut> donuts)
        {
            lock (_donutTray)
            {
                donuts.ForEach(_donutTray.Enqueue);

                Monitor.PulseAll(_donutTray);
            }
        }
    }
}
