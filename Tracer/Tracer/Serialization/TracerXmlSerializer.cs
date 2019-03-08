using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace TracerLib.Serialization {
    public class TracerXmlSerializer : ISerializer {
        public string Serialize(TraceResult value)
        {
            if (value == null)
            {
                return string.Empty;
            }
            try
            {
                TraceResultSerialized serializedResult = TraceResultHelper.GetReadyToSerializeVersion(value);

           
                var xmlserializer = new XmlSerializer(typeof(TraceResultSerialized));
                var stringWriter = new StringWriter();
                using (var writer = XmlWriter.Create(stringWriter, new XmlWriterSettings() { Indent = true, IndentChars = "\t" }))
                {
                    xmlserializer.Serialize(writer, serializedResult);
                    return stringWriter.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred", ex);
            }
        }
    }
}

