using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ThreadOperations;

namespace ThreadOperationTests
{
    [TestClass]
    public class ThreadTests
    {
        [TestMethod]
        public void StartThreadDemoTest()
        {
            var newThreadStart = new SimpleThreadStart();
            newThreadStart.StartThreadDemo();
        }

        [TestMethod]
        public void ThreadWithParamTest()
        {
            var threadWithParam = new StartingThreadWithParam();
            threadWithParam.StartThread();
        }

        [TestMethod]
        public void ThreadAbortTest()
        {
            var threadAbort = new ThreadInterruptAndAbortExample();
            threadAbort.Example();
        }

        
    }
}
