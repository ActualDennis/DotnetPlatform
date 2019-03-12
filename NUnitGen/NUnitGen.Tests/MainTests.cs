using NUnit.Framework;
using NUnitGen;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests {
    public class MainTests {
         
        private PipeLine pipeLine { get; set; }

        [SetUp]
        public void Setup()
        {
            pipeLine = new PipeLine();

            pipeLine.maxFilesToLoad = 5;
            pipeLine.maxFilesToWrite = 5;
            pipeLine.maxTasksExecuted = 5;
            pipeLine.outputFile = "H:";
        }

        [Test]
        public void BasicTest()
        {
            //var st = new Stack<string>();
            //st.Push();
            //pipeLine.Start(st);
            //Task.Delay(10000).GetAwaiter().GetResult();
        }
    }
}