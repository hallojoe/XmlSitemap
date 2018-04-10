using System.ComponentModel;
using HalloJoe.XmlSitemap.Interfaces;
using HalloJoe.XmlSitemap.Models;

namespace HalloJoe.XmlSitemap
{
    public class Parser
    {
        public static Urlset ParseUrlset(string s) => Parse<Urlset>(s);
        public static SitemapIndex ParseSitemapIndex(string s) => Parse<SitemapIndex>(s);

        public static IComposedList<SitemapEntity> Parse(string s)
        {
            if (s.Contains(Constants.LT + Constants.SITEMAPINDEX))
                return Parse<SitemapIndex>(s);
            else if (s.Contains(Constants.LT + Constants.URLSET))
                return Parse<Urlset>(s);
            return null;
        }

        private static T Parse<T>(string s)
        {
            var t = typeof(T);
            var typeConverter = TypeDescriptor.GetConverter(t);
            if (typeConverter != null)
                return (T)(typeConverter.ConvertFrom(s));
            return default(T);
        }
    }
}
