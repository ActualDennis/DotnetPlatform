using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tracer.Serialization {
    public static class TraceResultHelper {
        public static TraceResultSerialized GetReadyToSerializeVersion(TraceResult value)
        {
            var result = new TraceResultSerialized();
            result.threads = new List<TraceResultInnerSerialized>();

            foreach (var item in value.TracedResult)
            {
                result.threads.Add(new TraceResultInnerSerialized()
                {
                    ExecutionTime = item.Value.Sum(x => x.ExecutionTime),
                    Methods = item.Value,
                    ThreadId = item.Key
                });
            }

            return result;
        }
    }
}
