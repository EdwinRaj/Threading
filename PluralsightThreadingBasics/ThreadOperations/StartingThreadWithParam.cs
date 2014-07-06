using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadOperations
{
    public class StartingThreadWithParam
    {
        public void StartThread()
        {
            Console.WriteLine("Main Thread Inititated: Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            var newThread = new Thread(PrintCountryState);
            newThread.Start(new CountryState{Name="India",State="Getting Better"});
            newThread.Join();
            Console.WriteLine("Main Thread Ended: Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
        }

        private void PrintCountryState(object state)
        {
            Console.WriteLine("Spawned Thread Inititated: Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
            var countryState = state as CountryState;
            Console.WriteLine("Country Name: {0} State{1}",countryState.Name,countryState.State);
            Console.WriteLine("Spawned Thread Ended: Thread Id {0}", Thread.CurrentThread.ManagedThreadId);
        }
    }

    public class CountryState
    {
        public string Name { get; set; }
        public string State { get; set; }
    }
}
