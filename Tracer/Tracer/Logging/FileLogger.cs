using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TracerLib.Logging {
    public class FileLogger : ILogger {

        public const string DefaultLogPath = "H:/DenTracer/Logs.txt";

        public void Log(string value)
        {
            using (var stream = File.OpenWrite(DefaultLogPath))
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine($"Trace result for : {DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss")}");
                writer.WriteLine(value);
            }
        }
    }
}
