using System;
using System.Collections.Generic;
using System.Linq;
using HalloJoe.XmlSitemap.Models;

namespace HalloJoe.XmlSitemap
{
    public static class SitemapIndexExtensions
    {
        public static IEnumerable<string> GetLocations(this SitemapIndex model) => model.Entities.Select(o => o.Location);

    }
}
