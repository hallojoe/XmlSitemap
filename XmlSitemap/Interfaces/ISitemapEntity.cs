using System;
using System.Collections.Generic;
using System.Text;

namespace HalloJoe.XmlSitemap.Interfaces
{
    /// <summary>
    /// A sitemap in a sitemapindex or an url in an urlset
    /// </summary>
    public interface ISitemapEntity
    {
        string Location { get; set; }
        string LastModified { get; set; }
        string ChangeFrequency { get; set; }
        string Priority { get; set; }
    }
}
