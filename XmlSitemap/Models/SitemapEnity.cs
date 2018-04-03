using System.Xml.Serialization;
using HalloJoe.XmlSitemap.Interfaces;

namespace HalloJoe.XmlSitemap.Models
{
    public class SitemapEntity : ISitemapEntity
    {
        [XmlElement(Constants.LOC)]
        public string Location { get; set; }
        [XmlElement(Constants.LASTMOD)]
        public string LastModified { get; set; }
        [XmlElement(Constants.CHANGEFREQ)]
        public string ChangeFrequency { get; set; }
        [XmlElement(Constants.PRIORITY)]
        public string Priority { get; set; }
    }
}
