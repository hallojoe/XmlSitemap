using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using HalloJoe.XmlSitemap.Interfaces;
using HalloJoe.XmlSitemap.TypeConverters;

namespace HalloJoe.XmlSitemap.Models
{
    [XmlRoot(Constants.URLSET, Namespace = Constants.NS)]
    [TypeConverter(typeof(SitemapTypeConverter))]
    public class Sitemap : IComposedList<SitemapEntity>
    {
        [XmlElement(Constants.URL, Type = typeof(SitemapEntity))]
        public List<SitemapEntity> Entities { get; set; } = new List<SitemapEntity>();
    }
}
