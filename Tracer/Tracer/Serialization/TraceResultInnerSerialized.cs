using System;
using System.Collections.Generic;
using System.Text;

namespace Tracer.Serialization {
    public class TraceResultInnerSerialized {
        public int ThreadId { get; set; }

        public long ExecutionTime { get; set; }

        public List<MethodMetadata> Methods { get; set; }
    }
}
