using NUnitGen.Parsers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace NUnitGen {
    public class PipeLine {
        public PipeLine()
        {

        }

        public int maxFilesToLoad { get; set; }

        public int maxTasksExecuted { get; set; }

        public int maxFilesToWrite { get; set; }

        public string outputFile { get; set; }

        public void Start(Stack<string> inputFiles)
        {

            ExecutionDataflowBlockOptions maxFilesToLoadTasks = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = maxFilesToLoad
            };

            ExecutionDataflowBlockOptions maxTasksExecutedTasks = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = maxTasksExecuted
            };

            ExecutionDataflowBlockOptions maxFilesToWriteTasks = new ExecutionDataflowBlockOptions
            {
                MaxDegreeOfParallelism = maxFilesToWrite
            };

            var loadFiles = new TransformBlock<string, Tuple<string, string>>(
               new Func<string, Task<Tuple<string, string>>>(LoadTextFromFile), maxFilesToLoadTasks);
             
            var getTestClasses = new TransformBlock<Tuple<string, string>, Tuple<string, string>>(
               new Func<Tuple<string, string>, Task<Tuple<string, string>>>(GetTestClasses), maxTasksExecutedTasks);

            var writeResult = new ActionBlock<Tuple<string, string>(async input =>
            {
                await WriteResult(input);
            }, maxFilesToWriteTasks);

            loadFiles.LinkTo(getTestClasses, new DataflowLinkOptions() { PropagateCompletion = true });
            getTestClasses.LinkTo(writeResult, new DataflowLinkOptions() { PropagateCompletion = true });

            while(inputFiles.Count != 0)
            {
                var file = inputFiles.Pop();

                loadFiles.Post(file);
            }
        }

        /// <summary>
        /// Loads text from files
        /// </summary>
        /// <returns>Tuple where item1 == name of a file, item2 == its text.</returns>
        private async Task<Tuple<string, string>> LoadTextFromFile(string inputFile)
        {
            using (var fs = new FileStream(inputFile, FileMode.Open))
            using (var reader = new StreamReader(fs))
            {
                return new Tuple<string, string>(
                    inputFile,
                    await reader.ReadToEndAsync());
            }
        }

        private async Task<Tuple<string, string>> GetTestClasses(Tuple<string, string> filesText)
        {
           return await CSharpParser.GenerateNUnitTestClasses(filesText);
        }

        private async Task WriteResult(Tuple<string, string> testClass)
        {
            using (var fs = new FileStream(outputFile + Path.DirectorySeparatorChar + testClass.Item1, FileMode.Create))
            using (var writer = new StreamWriter(fs))
            {
                await writer.WriteAsync(testClass.Item2);
            }
        } 
    }
}
