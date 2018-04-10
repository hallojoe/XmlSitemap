using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using HalloJoe.XmlSitemap.Interfaces;
using HalloJoe.XmlSitemap.Models;

namespace HalloJoe.XmlSitemap.TypeConverters
{
    internal static class TypeConverterHelper
    {
        internal static T FromString<T>(string s) // where T : IComposedList<ISitemapEntity>
        {
            var type = typeof(T);
            var serializer = new XmlSerializer(type);
            using (var sr = new StringReader(s))
                return (T)serializer.Deserialize(sr);
        }

        internal static string ToString<T>(T o) // where T : IComposedList<ISitemapEntity>
        {
            string result = string.Empty;
            if (o != null)
                using (var stringWriter = new Utf8StringWriter())
                {
                    Type type = typeof(T);
                    XmlSerializer xmlSerialiser = new XmlSerializer(type);
                    xmlSerialiser.Serialize(stringWriter, o);
                    result = stringWriter.ToString();
                }
            return result;
        }
    }
}
