using System;
using System.Collections.Generic;
using System.Text;

namespace Tracer.Serialization {
    public interface ISerializer {
        string Serialize(TraceResult value);
    }
}
