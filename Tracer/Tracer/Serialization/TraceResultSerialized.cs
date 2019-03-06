using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Tracer.Serialization {
    [XmlRoot(ElementName = "Root")]
    public class TraceResultSerialized {
        [XmlArray("threads")]
        public List<TraceResultInnerSerialized> threads { get; set; }
    }
}
