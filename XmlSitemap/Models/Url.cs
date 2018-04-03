using HalloJoe.XmlSitemap.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace HalloJoe.XmlSitemap.Models
{
    public abstract class BaseUrl : IUrl
    {
        public virtual string Location { get; set; }

        public BaseUrl(string location) { Location = location; }
        public BaseUrl() : this(string.Empty) { } 
        protected virtual string GetBaseUrl(string url) =>
            string.IsNullOrEmpty(url) ? string.Empty : new Uri(new Uri(url).GetLeftPart(UriPartial.Authority)).ToString();
    }

    public class Url : BaseUrl, IUrl { }
    public class RobotsTxtUrl : BaseUrl, IUrl
    {
        public RobotsTxtUrl(string location) : base(location)
        {
            Location = GetBaseUrl(Location) + "/robots.txt";
        }
    }
    public class SitemapUrl : BaseUrl, IUrl { }
}
