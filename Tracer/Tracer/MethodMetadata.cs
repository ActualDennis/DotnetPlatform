using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace Tracer {
    public class MethodMetadata {
        public MethodMetadata(MethodBase method)
        {
            Name = method.Name;
            ClassName = method.DeclaringType.ToString() ?? string.Empty;
        }

        public string Name { get; set; }

        public string ClassName { get; set; }

        public long ExecutionTime { get; set; }

        public List<MethodMetadata> InnerMethods { get; private set; } = new List<MethodMetadata>();

        private Stopwatch watch { get; set; } = new Stopwatch();

        public void StartMeasure()
        {
            watch.Start();
        }

        public void StopMeasure()
        {
            ExecutionTime = watch.ElapsedMilliseconds;
            watch.Stop();
        }

        public void NewInnerMethod(MethodMetadata method)
        {
            InnerMethods.Add(method);
        }
    }
}
