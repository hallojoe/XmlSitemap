using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using HalloJoe.XmlSitemap.Interfaces;
using HalloJoe.XmlSitemap.Models;

namespace HalloJoe.XmlSitemap.TypeConverters
{
    public class UrlsetTypeConverter : TypeConverter
    {
        /// <summary>
        /// Validate value
        /// </summary>
        /// <param name="context"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(ITypeDescriptorContext context, object value) => base.IsValid(context, value);

        /// <summary>
        /// Allow string conversions
        /// </summary>
        /// <param name="context"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Allow string conversions
        /// </summary>
        /// <param name="context"></param>
        /// <param name="sourceType"></param>
        /// <returns></returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// Convert to string
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <param name="destinationType"></param>
        /// <returns></returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            //var urlset = (Urlset)value;
            //if (urlset != null)
            //    return TypeConverterHelper.ToString(urlset);
            //       return base.ConvertTo(context, culture, value, destinationType);
            var sitemapIndex = (Urlset)value;
            string result = string.Empty;
            if (sitemapIndex != null)
            {
                using (var stringWriter = new Utf8StringWriter())
                {
                    Type type = typeof(Urlset);
                    XmlSerializer xmlSerialiser = new XmlSerializer(type);
                    xmlSerialiser.Serialize(stringWriter, sitemapIndex);
                    result = stringWriter.ToString();
                }
            }
            return result;

        }

        /// <summary>
        /// Convert back to model
        /// </summary>
        /// <param name="context"></param>
        /// <param name="culture"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (!(value is string))
                return base.ConvertFrom(context, culture, value);
            return TypeConverterHelper.FromString<Urlset>(value.ToString());
        }

    }
}
