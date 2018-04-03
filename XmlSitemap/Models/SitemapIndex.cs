using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using HalloJoe.XmlSitemap.Interfaces;
using HalloJoe.XmlSitemap.TypeConverters;

namespace HalloJoe.XmlSitemap.Models
{
    [XmlRoot(Constants.SITEMAPINDEX, Namespace = Constants.NS)]
    [TypeConverter(typeof(SitemapIndexTypeConverter))]
    public class SitemapIndex : IComposedList<SitemapEntity>
    {
        [XmlElement(Constants.SITEMAP, Type = typeof(SitemapEntity))]
        public List<SitemapEntity> Entities { get; set; } = new List<SitemapEntity>();
    }
}
