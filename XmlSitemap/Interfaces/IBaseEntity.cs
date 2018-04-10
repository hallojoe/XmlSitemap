using System;
using System.Collections.Generic;
using System.Text;

namespace HalloJoe.XmlSitemap.Interfaces
{
    /// <summary>
    /// A sitemap in a sitemapindex or the base of a url in a urlset
    /// </summary>
    public interface IBaseEntity
    {
        string Location { get; set; }
        string LastModified { get; set; }
    }
}
