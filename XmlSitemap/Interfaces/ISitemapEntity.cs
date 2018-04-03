using System;
using System.Collections.Generic;
using System.Text;

namespace HalloJoe.XmlSitemap.Interfaces
{
    /// <summary>
    /// Represents a sitemap(sitemapindex) or url(sitemap) node
    /// </summary>
    public interface ISitemapEntity
    {
        string Location { get; set; }
        string LastModified { get; set; }
        string ChangeFrequency { get; set; }
        string Priority { get; set; }
    }
}
