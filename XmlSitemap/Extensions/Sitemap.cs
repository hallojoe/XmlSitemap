using HalloJoe.XmlSitemap.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HalloJoe.XmlSitemap
{
    public static class SitemapExtensions
    {
        public static IEnumerable<string> GetLocations(this Sitemap model) => model.Entities.Select(o => o.Location);
    }
}
