using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Tracer {
    public class Tracer : ITracer {

        public TraceResult GetTraceResult()
        {
            return new TraceResult(ConstructResult());
        }

        /// <summary>
        /// Represents Id of a thread and Traced methods of it.
        /// </summary>
        private ConcurrentDictionary<int, TracedMethodsInfo> ThreadsMethods { get; set; } = new ConcurrentDictionary<int, TracedMethodsInfo>();
        
        public void StartTrace()
        {
            var method = new StackTrace().GetFrames()[1].GetMethod();

            if(!ThreadsMethods.ContainsKey(Thread.CurrentThread.ManagedThreadId))
                ThreadsMethods.TryAdd(Thread.CurrentThread.ManagedThreadId, new TracedMethodsInfo());

            ThreadsMethods[Thread.CurrentThread.ManagedThreadId].StartMeasure(method);
        }
        
        public void StopTrace()
        {
            ThreadsMethods[Thread.CurrentThread.ManagedThreadId].StopMeasure();
        }

        private Dictionary<int, List<MethodMetadata>> ConstructResult()
        {
            var result = new Dictionary<int, List<MethodMetadata>>();

            foreach (var threadInfo in ThreadsMethods)
            {
                result.Add(threadInfo.Key, threadInfo.Value.Methods);
            }

            return result;
        }
    }
}
