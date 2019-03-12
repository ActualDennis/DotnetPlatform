using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace NUnitGen {
    public class PipeLine {
        public PipeLine(int maxFilesToLoad, int maxTasksExecuted, int maxFilesToWrite, Stack<string> inputFiles, string outputFile)
        {
            this.maxFilesToLoad = maxFilesToLoad;
            this.maxFilesToWrite = maxFilesToWrite;
            this.maxTasksExecuted = maxTasksExecuted;
            this.inputFiles = inputFiles;
            this.outputFile = outputFile;
        }

        private int maxFilesToLoad { get; set; }

        private int maxTasksExecuted { get; set; }

        private int maxFilesToWrite { get; set; }

        private string outputFile { get; set; }

        private Stack<string> inputFiles { get; set; }

        public void Start()
        {

        }

        /// <summary>
        /// Loads text from files
        /// </summary>
        /// <returns>Dictionary where key == name of a file, value == its text.</returns>
        private async Task<Dictionary<string, string>> LoadTextFromFiles()
        {
            var result = new Dictionary<string, string>();

            for(int i = 0; i < maxFilesToLoad; ++i)
            {
                using (var fs = new FileStream(inputFiles.Peek(), FileMode.Open))
                using (var reader = new StreamReader(fs))
                {
                    var text = string.Empty;

                    text += await reader.ReadToEndAsync();

                    result.Add(inputFiles.Peek(), text);

                    inputFiles.Pop();
                }

                if (inputFiles.Count.Equals(0))
                    break;
            }

            return result;
        }

        private async Task<Dictionary<string, string>> GetTestClasses(Dictionary<string, string> filesText)
        {

        }

        private async Task WriteResult(Dictionary<string, string> testClasses)
        {

        } 
    }
}
