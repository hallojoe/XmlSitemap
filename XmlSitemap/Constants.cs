using System;
using System.Collections.Generic;
using System.Text;

namespace HalloJoe.XmlSitemap
{
    internal static class Constants
    {
        internal const string NS = "http://www.sitemaps.org/schemas/sitemap/0.9"; // schema
        internal const string SITEMAP = "sitemap"; // sitemap
        internal const string SITEMAPINDEX = "sitemapindex"; // sitemap index root
        internal const string URLSET = "urlset"; // sitemap root
        internal const string URL = "url"; // sitemap root
        internal const string LOC = "loc"; // location element name
        internal const string LASTMOD = "lastmod"; // lastmod element name
        internal const string CHANGEFREQ = "changefreq"; // changefreq element name
        internal const string PRIORITY = "priority"; // priority element name
        internal const string ROBOTSTXT_PATH = "/robots.txt"; // base url postfix for making absolute robots.txt url
        internal const char R = '\r'; // carrige
        internal const char N = '\n'; // new      
        internal const char GT = '>'; // greater than
        internal const char LT = '<'; // less than
        internal const char WHITESPACE = ' '; // space bar
        internal const char COLON = ':'; // the better kind
        internal const char FORWARD_SLASH = '/'; // go forwards go forwards
        internal const string HTTP = "http"; // any protocol we like starts with
        internal const string ACCEPT = "accept"; // accept header name
        internal const string TEXT_PLAIN = "text/plain"; // mime txt
        internal const string TEXT_XML = "text/xml"; // mime xml
    }
}
