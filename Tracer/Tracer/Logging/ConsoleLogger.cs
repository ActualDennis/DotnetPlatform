using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLib.Logging {
    public class ConsoleLogger : ILogger {
        public void Log(string value)
        {
            Console.WriteLine(value);
        }
    }
}
