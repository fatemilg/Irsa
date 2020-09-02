using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Irsa.Configs
{
    public class Classes
    {
        public string GenerateXmlBody<T>(T  param) where T : class
        {

            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));
            var xmlBody = "";

            using (var sw = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sw))
                {
                    xsSubmit.Serialize(writer, param);
                    xmlBody = sw.ToString(); // Your XML
                }
                return xmlBody;
            }

        }
        public T DeserializeXmlToClass<T>(string input) where T : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(T));

            using (StringReader sr = new StringReader(input))
            {
                return (T)ser.Deserialize(sr);
            }
        }
    }
}
