using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using HalloJoe.XmlSitemap.Interfaces;
using HalloJoe.XmlSitemap.Models;

namespace HalloJoe.XmlSitemap.TypeConverters
{
    public class SitemapIndexTypeConverter : TypeConverter
    {

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
            SitemapIndex sitemapIndex = (SitemapIndex)value;
            if (sitemapIndex != null)
                TypeConverterHelper.ToString((IComposedList<ISitemapEntity>)sitemapIndex);
            return base.ConvertTo(context, culture, value, destinationType);
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
            return TypeConverterHelper.FromString<SitemapIndex>(value.ToString());
        }

    }
}
