using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Tracer {
    public class TraceResult {

        public TraceResult(Dictionary<int, List<MethodMetadata>> result)
        {
            TracedResult = result;
        }
        public Dictionary<int, List<MethodMetadata>> TracedResult { get; }
    }
}
