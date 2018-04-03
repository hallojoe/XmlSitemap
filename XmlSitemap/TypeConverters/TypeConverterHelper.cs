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
        internal static T FromString<T>(string s)
        {
            var type = typeof(T);
            var serializer = new XmlSerializer(type);
            using (var sr = new StringReader(s))
                return (T)serializer.Deserialize(sr);
        }

        internal static string ToString(IComposedList<ISitemapEntity> sitemap)
        {
            string result = string.Empty;
            if (sitemap != null)
                using (var stringWriter = new Utf8StringWriter())
                {
                    Type type = sitemap.GetType();
                    XmlSerializer xmlSerialiser = new XmlSerializer(type);
                    xmlSerialiser.Serialize(stringWriter, type);
                    result = stringWriter.ToString();
                }
            return result;
        }
    }
}
