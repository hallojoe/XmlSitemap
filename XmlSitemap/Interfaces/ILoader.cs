using HalloJoe.XmlSitemap.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HalloJoe.XmlSitemap.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILoader
    {
        List<Urlset> Urlsets { get; set; }
        List<SitemapIndex> SitemapIndexes { get; set; }
    }
}
