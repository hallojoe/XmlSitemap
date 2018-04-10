using System.ComponentModel;
using HalloJoe.XmlSitemap.Models;

namespace HalloJoe.XmlSitemap
{
    public class Composer
    {
        public static string ComposeUrlset(Urlset o) => Compose(o);
        public static string ComposeSitemapIndex(SitemapIndex o) => Compose(o);

        private static string Compose<T>(T o)
        {
            var t = typeof(T);
            var typeConverter = TypeDescriptor.GetConverter(t);
            if (typeConverter != null)
                return (string)(typeConverter.ConvertTo(o, typeof(string)));
            return default(string);
        }
    }
}
