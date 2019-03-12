using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitGen {
    public class PipeLine {
        public PipeLine(int maxFilesToLoad, int maxTasksExecuted, int maxFilesToWrite)
        {
            this.maxFilesToLoad = maxFilesToLoad;
            this.maxFilesToWrite = maxFilesToWrite;
            this.maxTasksExecuted = maxTasksExecuted;
        }

        private int maxFilesToLoad { get; set; }

        private int maxTasksExecuted { get; set; }

        private int maxFilesToWrite { get; set; }


    }
}
