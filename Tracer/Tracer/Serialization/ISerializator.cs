using System;
using System.Collections.Generic;
using System.Text;

namespace TracerLib.Serialization {
    public interface ISerializer {
        string Serialize(TraceResult value);
    }
}
