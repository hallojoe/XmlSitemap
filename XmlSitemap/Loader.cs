using System;
using System.Collections.Generic;
using HalloJoe.XmlSitemap.Interfaces;
using HalloJoe.XmlSitemap.Models;

namespace HalloJoe.XmlSitemap
{
    /// <summary>
    /// Takes a string and parses it into an urlset or sitemapiindex. Also has a couple of add methods to ease manual building.
    /// </summary>
    public class Loader : ILoader
    {
        public List<SitemapIndex> SitemapIndexes { get; set; } = new List<SitemapIndex>();
        public List<Urlset> Urlsets { get; set; } = new List<Urlset>();

        public Loader() { }

        public Loader(string s)
        {
            if(!string.IsNullOrEmpty(s))
                Add(s);
        }

        public void Add(string s)
        {
            IComposedList<SitemapEntity> o;
            if (s.Contains(Constants.LT + Constants.SITEMAPINDEX))
            {
                o = Parser.ParseSitemapIndex(s);
                if (o != null)
                    SitemapIndexes.Add(o as SitemapIndex);
            }
            else if (s.Contains(Constants.LT + Constants.URLSET))
            {
                o = Parser.ParseUrlset(s);
                if (o != null)
                    Urlsets.Add(o as Urlset);
            }
        }
        public void Add(SitemapIndex o) => SitemapIndexes.Add(o);
        public void Add(Urlset o) => Urlsets.Add(o);

    }
}
