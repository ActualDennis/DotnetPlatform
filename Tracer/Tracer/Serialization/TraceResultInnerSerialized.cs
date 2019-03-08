using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TracerLib.Serialization {
    [XmlType(TypeName = "thread")]
    public class TraceResultInnerSerialized {
        [XmlAttribute]
        public int ThreadId { get; set; }
        [XmlAttribute]
        public long ExecutionTime { get; set; }
        [XmlElement(ElementName = "method")]
        public List<MethodMetadata> Methods { get; set; }
    }
}
