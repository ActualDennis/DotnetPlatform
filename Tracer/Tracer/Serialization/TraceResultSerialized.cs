using System;
using System.Collections.Generic;
using System.Text;

namespace Tracer.Serialization {
    public class TraceResultSerialized {
        public List<TraceResultInnerSerialized> threads { get; set; }
    }
}
