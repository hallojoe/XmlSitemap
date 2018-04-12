using System.Collections.Generic;

namespace HalloJoe.XmlSitemap.Interfaces
{
    /// <summary>
    /// Represents result of any content we have to interpret - (optional)robots.txt's, sitemaps and sitemap indexes
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IComposedList<T>
    {
        List<T> Entities { get; set; }
    }


}
