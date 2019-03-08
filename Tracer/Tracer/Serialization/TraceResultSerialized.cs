using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TracerLib.Serialization {
    [XmlRoot(ElementName = "Root")]
    public class TraceResultSerialized {
        [XmlArray("threads")]
        public List<TraceResultInnerSerialized> threads { get; set; }
    }
}
