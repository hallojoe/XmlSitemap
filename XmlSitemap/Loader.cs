using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using HalloJoe.XmlSitemap.Interfaces;
using HalloJoe.XmlSitemap.Models;
using HalloJoe.RobotsTxt;

namespace HalloJoe.XmlSitemap
{
    public class Loader : IDisposable
    {

        private HttpClient _httpClient = new HttpClient();

        private ConcurrentBag<Uri> _downloadQueue = new ConcurrentBag<Uri>();
        public ConcurrentBag<Sitemap> Sitemaps { get; private set; } = new ConcurrentBag<Sitemap>();
        public ConcurrentBag<SitemapIndex> SitemapIndexes { get; private set; } = new ConcurrentBag<SitemapIndex>();
        public RobotsTxt.IDocument RobotsTxt { get; set; }


        /// <summary>
        /// Robots.txt url constructor
        /// </summary>
        /// <param name="url"></param>
        public Loader(RobotsTxtUrl url)
        {
            _httpClient.DefaultRequestHeaders.Add("Accept", "text/xml");
            string robotTxt = DownloadString(url.Location).Result;
            RobotsTxt = HalloJoe.RobotsTxt.Document.Parse(robotTxt);
            DownloadSitemaps(RobotsTxt);
        }

        /// <summary>
        /// SitemapUrl constructor
        /// </summary>
        /// <param name="url"></param>
        public Loader(SitemapUrl url)
        {
            _httpClient.DefaultRequestHeaders.Add("Accept", "text/xml");
            _downloadQueue.Add(new Uri(url.Location));
            DownloadSitemaps();
        }

        /// <summary>
        /// String url constructor - pass any url and this will construct a robots.txt url frrom it's base
        /// </summary>
        /// <param name="url"></param>
        public Loader(string url) : this(new RobotsTxtUrl(url))
        {
        }

        /// <summary>
        /// Fill _downloadQueue with sitemap urls and then download
        /// </summary>
        public void DownloadSitemaps(RobotsTxt.IDocument document)
        {
            foreach (var uri in document.Content?.Sitemaps.Select(url => new Uri(url)))
               _downloadQueue.Add(uri);
            DownloadSitemaps();            
        }

        /// <summary>
        /// Download
        /// </summary>
        public void DownloadSitemaps()
        {
            var uris = _downloadQueue.Select(url => url);
            _downloadQueue = new ConcurrentBag<Uri>();

            Parallel.ForEach(uris, (uri) =>
            {
                var content = DownloadString(uri.ToString())?.Result;
                if (!string.IsNullOrEmpty(content))
                {
                    IComposedList<SitemapEntity> o;
                    if (content.Contains(Constants.LT + Constants.SITEMAPINDEX))
                    {
                        o = ParseSitemapIndex(content);
                        if (o != null)
                        {
                            var sitemapIndex = o as SitemapIndex;
                            SitemapIndexes.Add(sitemapIndex);
                            EnqueueDownloads(sitemapIndex);
                        }
                    }
                    else if (content.Contains(Constants.LT + Constants.URLSET))
                    {
                        o = ParseSitemap(content);
                        if (o != null)
                            Sitemaps.Add(o as Sitemap);
                    }
                }

            });
            if (_downloadQueue.Any())
                DownloadSitemaps();
        }

        private bool ValidateUrl(string url)
        {
            try { return new Uri(url) != null; }
            catch (Exception) { return false; }
        }

        private void EnqueueDownloads(SitemapIndex o)
        {
            foreach (var entity in o.Entities)
                if (ValidateUrl(entity?.Location))
                    _downloadQueue.Add(new Uri(entity.Location));
        }

        public void Dispose()
        {
            if (_httpClient != null)
                ((IDisposable)_httpClient).Dispose();
        }

        public async Task<string> DownloadString(string url)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

        public static Sitemap ParseSitemap(string s) => Parse<Sitemap>(s);
        public static SitemapIndex ParseSitemapIndex(string s) => Parse<SitemapIndex>(s);

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
