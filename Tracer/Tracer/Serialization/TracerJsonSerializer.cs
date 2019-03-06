using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tracer.Serialization {
    public class TracerJsonSerializer : ISerializer {
        public string Serialize(TraceResult value)
        {
            return JsonConvert.SerializeObject(TraceResultHelper.GetReadyToSerializeVersion(value), Formatting.Indented);
        }
    }
}
