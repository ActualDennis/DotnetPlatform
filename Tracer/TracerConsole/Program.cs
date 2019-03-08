using System;
using System.Security.Cryptography;
using System.Threading;
using TracerLib.Logging;
using TracerLib;
using TracerLib.Serialization;

namespace TracerConsole {
    class Program {
        static void Main(string[] args)
        {
            var tracer = new Tracer();
            tracer.StartTrace();
            first(tracer);
            var newThread = new Thread(() => third(tracer))
            {
                IsBackground = false
            };

            newThread.Start();

            tracer.StopTrace();
            Console.ReadLine();
        }

        static void first(Tracer tracer)
        {
            tracer.StartTrace();
            second(tracer);

            var random = new Random();

            var buffer = new byte[1024];

            var sha = SHA512.Create();

            for (int i = 0; i < 500; ++i)
            {
                random.NextBytes(buffer);
                var hash = sha.ComputeHash(buffer);
                Console.WriteLine("kek123");
            }

            tracer.StopTrace();
        }

        static void second(Tracer tracer)
        {
            tracer.StartTrace();

            var sha = SHA512.Create();

            var random = new Random();

            var buffer = new byte[1024];

            for (int i = 0; i < 500; ++i)
            {
                random.NextBytes(buffer);
                var hash = sha.ComputeHash(buffer);
                Console.WriteLine("kek123");
            }
            tracer.StopTrace();
        }

        static void third(Tracer tracer)
        {
            tracer.StartTrace();
            second(tracer);

            var random = new Random();

            var buffer = new byte[1024];

            var sha = SHA512.Create();

            for (int i = 0; i < 500; ++i)
            {
                random.NextBytes(buffer);
                var hash = sha.ComputeHash(buffer);
                Console.WriteLine("kek123");
            }

            tracer.StopTrace();

            var logger = new ConsoleLogger();
            logger.Log(new TracerXmlSerializer().Serialize(tracer.GetTraceResult()));
        }

        static void fourth(Tracer tracer)
        {
            tracer.StartTrace();
            second(tracer);

            var random = new Random();

            var buffer = new byte[1024];

            var sha = SHA512.Create();

            for (int i = 0; i < 500; ++i)
            {
                random.NextBytes(buffer);
                var hash = sha.ComputeHash(buffer);
                Console.WriteLine("kek123");
            }

            tracer.StopTrace();
        }
    }
}
