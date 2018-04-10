using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HalloJoe.XmlSitemap;
using HalloJoe.XmlSitemap.Models;
using System.Linq;

namespace UnitTestXmlSitemap
{

    /// <summary>
    /// Barely a test
    /// </summary>
    [TestClass]
    public class UnitTestXmlSitemap
    {
        private string _sitemapIndex = @"<?xml version='1.0' encoding='UTF-8'?>
            <urlset xmlns = 'http://www.sitemaps.org/schemas/sitemap/0.9'>
                <url><loc>http://www.dr.dk/</loc><changefreq>always</changefreq></url>
                <url><loc>http://www.dr.dk/Bonanza/</loc><changefreq>daily</changefreq></url>
                <url><loc>http://www.dr.dk/kgf/</loc><changefreq>daily</changefreq></url>
                <url><loc>http://www.dr.dk/levnu/</loc><changefreq>daily</changefreq></url>
                <url><loc>http://www.dr.dk/mad/</loc><changefreq>daily</changefreq></url>
                <url><loc>http://www.dr.dk/musik/</loc><changefreq>daily</changefreq></url>
                <url><loc>http://www.dr.dk/nyheder/</loc><changefreq>always</changefreq></url>
                <url><loc>http://www.dr.dk/nyheder/allenyheder/</loc><changefreq>always</changefreq></url>
                <url><loc>http://www.dr.dk/nyheder/kultur/</loc><changefreq>always</changefreq></url>
                <url><loc>http://www.dr.dk/nyheder/politik/</loc><changefreq>always</changefreq></url>
                <url><loc>http://www.dr.dk/Nyheder/Vejret</loc><changefreq>always</changefreq></url>
                <url><loc>http://www.dr.dk/nyheder/viden/</loc><changefreq>always</changefreq></url>
                <url><loc>http://www.dr.dk/p1/</loc><changefreq>daily</changefreq></url>
                <url><loc>http://www.dr.dk/p2/</loc><changefreq>daily</changefreq></url>
                <url><loc>http://www.dr.dk/p3/</loc><changefreq>daily</changefreq></url>
                <url><loc>http://www.dr.dk/p4/aarhus/</loc><changefreq>always</changefreq></url>
                <url><loc>http://www.dr.dk/p4/bornholm/</loc><changefreq>always</changefreq></url>
                <url><loc>http://www.dr.dk/p4/esbjerg/</loc><changefreq>always</changefreq></url>
                <url><loc>http://www.dr.dk/p4/fyn/</loc><changefreq>always</changefreq></url>
                <url><loc>http://www.dr.dk/p4/kbh/</loc><changefreq>always</changefreq></url>
                <url><loc>http://www.dr.dk/p4/nord/</loc><changefreq>always</changefreq></url>
                <url><loc>http://www.dr.dk/p4/sjaelland/</loc><changefreq>always</changefreq></url>
                <url><loc>http://www.dr.dk/p4/syd/</loc><changefreq>always</changefreq></url>
                <url><loc>http://www.dr.dk/p4/trekanten/</loc><changefreq>always</changefreq></url>
                <url><loc>http://www.dr.dk/p4/vest/</loc><changefreq>always</changefreq></url>
                <url><loc>http://www.dr.dk/p5/</loc><changefreq>daily</changefreq></url>
                <url><loc>http://www.dr.dk/p6beat/</loc><changefreq>daily</changefreq></url>
                <url><loc>http://www.dr.dk/p7mix/</loc><changefreq>daily</changefreq></url>
                <url><loc>http://www.dr.dk/p8jazz/</loc><changefreq>daily</changefreq></url>
                <url><loc>http://www.dr.dk/playlister/</loc><changefreq>always</changefreq></url>
                <url><loc>http://www.dr.dk/podcast/</loc><changefreq>daily</changefreq></url>
                <url><loc>http://www.dr.dk/radio/</loc><changefreq>daily</changefreq></url>
                <url><loc>http://www.dr.dk/ramasjang</loc><changefreq>daily</changefreq></url>
                <url><loc>http://www.dr.dk/ultra</loc><changefreq>daily</changefreq></url>
                <url><loc>http://www.dr.dk/sporten/</loc><changefreq>always</changefreq></url>
                <url><loc>https://www.dr.dk/tv/</loc><changefreq>daily</changefreq></url>
            </urlset>";

        private string _urlset = @"<sitemapindex xmlns='http://www.sitemaps.org/schemas/sitemap/0.9'>
                <sitemap>
                    <loc>https://www.bt.dk/sitemap.xml/articles</loc>
                </sitemap>
                    <sitemap>
                    <loc>https://www.bt.dk/sitemap.xml/tags</loc>
                </sitemap>
                    <sitemap>
                    <loc>https://www.bt.dk/sitemap.xml/custom-pages</loc>
                </sitemap>
            </sitemapindex>";

        /// <summary>
        /// Load above sitemapindex and urlset then
        /// verify some counts are equal
        /// </summary>
        [TestMethod]
        public void Test_Count()
        {
            var loader = new Loader();
            loader.Add(_sitemapIndex);
            loader.Add(_urlset);
            Assert.IsTrue(
                loader.SitemapIndexes.Count == 1 && // expect single sitemap index
                loader.Urlsets.Count == 1 && // expect single urlset
                loader.SitemapIndexes.First().Entities.Count == 3 && // expect 3 entities from sitemapindex
                loader.Urlsets.First().Entities.Count == 36 // expect 36 entities from urlset
            );
        }

        /// <summary>
        /// Load a urlset, convert it to string and then 
        /// load it back to object and expect entity counts to be equal
        /// </summary>
        [TestMethod]
        public void Test_Read_Write_Equals_From_Absolute_Url()
        {
            string absoluteUrl = "https://www.dr.dk/sitemap.xml";
            string s = string.Empty;
            Urlset urlset = null;
            using (var httpLoader = new HttpLoader(new Url(absoluteUrl)))
            {
                try
                {
                    urlset = httpLoader.Urlsets.FirstOrDefault();
                    if (urlset == null)
                        Assert.Fail($"urlset expected from {urlset} is null");
                    s = Composer.ComposeUrlset(httpLoader.Urlsets.First());
                }
                catch (Exception ex)
                {
                    Assert.Fail($"error {ex.Message}");
                }
            }
            var loader = new Loader();
            loader.Add(urlset);

            Assert.IsTrue(loader.Urlsets.First().Entities.Count == urlset.Entities.Count);
        }
    }
}
