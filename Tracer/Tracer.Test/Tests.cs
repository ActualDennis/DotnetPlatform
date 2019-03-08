using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TracerLib;
using TracerLib.Logging;
using TracerLib.Serialization;

namespace Tests {
    public class Tests {
        Tracer tracer;
        TraceResult result;

        [SetUp]
        public void Setup()
        {
            tracer = new Tracer();
            Method1();
            new Thread(() => Method2()).Start();
            //wait until background thread finishes the work.
            Thread.Sleep(300);
            result = tracer.GetTraceResult();
        }

        [Test]
        public void ClassNameTest()
        {
            const string expectedClassName = "Tests.Tests";
            if (result.TracedResult.Values.FirstOrDefault()?.Count == 0)
            {
                Assert.True(false, "No traced threads in thread list!");
            }
            else
            {
                Assert.AreEqual(expectedClassName, result.TracedResult.Values.FirstOrDefault()?.FirstOrDefault()?.ClassName);
            }
        }

        [Test]
        public void ThreadCountTest()
        {
            const int expectedThreadCount = 2;
            Assert.AreEqual(expectedThreadCount, result.TracedResult.Keys.Count);
        }

        [Test]
        public void TestExecutionTime()
        {
            if (result.TracedResult.Count == 0)
            {
                Assert.True(false, "No traced threads in thread list!");
            }
            else
            {
                const long expectedTime = 100;
                if (expectedTime > result.TracedResult.Values.FirstOrDefault()?.FirstOrDefault()?.ExecutionTime)
                {
                    Assert.True(false, $"Expected time is greater then actual. Time was {result.TracedResult.Values.FirstOrDefault()?.FirstOrDefault()?.ExecutionTime}!");
                }
            }
        }

        private void Method1()
        {
            tracer.StartTrace();
            Task.Delay(5).GetAwaiter().GetResult();
            Method2();
            tracer.StopTrace();
        }

        private void Method2()
        {
            tracer.StartTrace();
            Task.Delay(25).GetAwaiter().GetResult();
            Method3();
            tracer.StopTrace();
        }

        private void Method3()
        {
            tracer.StartTrace();
            Task.Delay(50).GetAwaiter().GetResult();
            Method4();
            tracer.StopTrace();
        }

        private void Method4()
        {
            tracer.StartTrace();
            Task.Delay(100).GetAwaiter().GetResult();
            tracer.StopTrace();
        }
    }
}