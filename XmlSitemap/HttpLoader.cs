using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using HalloJoe.XmlSitemap.Interfaces;
using HalloJoe.XmlSitemap.Models;

namespace HalloJoe.XmlSitemap
{
    /// <summary>
    /// Downloads single or many urlsets and/or sitemapindexes
    /// </summary>
    public class HttpLoader : IDisposable, ILoader
    {
        private HttpClient _httpClient = new HttpClient();
        private ConcurrentBag<Uri> _downloadQueue = new ConcurrentBag<Uri>();
        private ConcurrentBag<Urlset> _urlsets { get; set; } = new ConcurrentBag<Urlset>();
        private ConcurrentBag<SitemapIndex> _sitemapIndexes { get; set; } = new ConcurrentBag<SitemapIndex>();
        private int _maxLevel;
        private int _currentLevel = 0;

        public int MaxLevel { get => _maxLevel; }

        public List<SitemapIndex> SitemapIndexes {
            get => _sitemapIndexes.ToList();
            set => _sitemapIndexes = new ConcurrentBag<SitemapIndex>(); 
        }
        public List<Urlset> Urlsets
        {
            get => _urlsets.ToList();
            set => _urlsets = new ConcurrentBag<Urlset>();
        }

        public RobotsTxt.IDocument RobotsTxt { get; set; }

        /// <summary>
        /// Robots.txt url constructor
        /// </summary>
        /// <param name="url"></param>
        public HttpLoader(RobotsTxtUrl url, int maxLevel = 1)
        {
            _maxLevel = maxLevel;
            _httpClient.DefaultRequestHeaders.Add(Constants.ACCEPT, Constants.TEXT_PLAIN);
            string robotTxt = DownloadString(url.Location).Result;
            RobotsTxt = HalloJoe.RobotsTxt.Document.Parse(robotTxt);
            DownloadUrlsets(RobotsTxt);
        }

        /// <summary>
        /// Url constructor
        /// </summary>
        /// <param name="url"></param>
        public HttpLoader(Url url, int maxLevel = 1)
        {
            _maxLevel = maxLevel;
            _httpClient.DefaultRequestHeaders.Add(Constants.ACCEPT, Constants.TEXT_XML);
            _downloadQueue.Add(new Uri(url.Location));
            DownloadUrlsets();
        }

        /// <summary>
        /// String url constructor - pass any url and this will construct a robots.txt url frrom it's base
        /// </summary>
        /// <param name="url"></param>
        public HttpLoader(string url, int maxLevel = 1) : this(new RobotsTxtUrl(url), maxLevel) { }

        /// <summary>
        /// Fill _downloadQueue with Urlset urls and then download
        /// </summary>
        public void DownloadUrlsets(RobotsTxt.IDocument document)
        {
            foreach (var uri in document.Content?.Sitemaps.Select(url => new Uri(url)))
               _downloadQueue.Add(uri);
            DownloadUrlsets();            
        }

        /// <summary>
        /// Download
        /// </summary>
        public void DownloadUrlsets()
        {
            _currentLevel++;
            if (_currentLevel > _maxLevel)
                return;

            var uris = _downloadQueue.Select(url => url);
            _downloadQueue = new ConcurrentBag<Uri>();

            Parallel.ForEach(uris, (uri) =>
            {
                try
                {
                    var content = DownloadString(uri.ToString())?.Result;
                    if (!string.IsNullOrEmpty(content))
                    {
                        IComposedList<SitemapEntity> o;
                        if (content.Contains(Constants.LT + Constants.SITEMAPINDEX))
                        {
                            o = Parser.ParseSitemapIndex(content);
                            if (o != null)
                            {
                                var sitemapIndex = o as SitemapIndex;
                                _sitemapIndexes.Add(sitemapIndex);
                                EnqueueDownloads(sitemapIndex);
                            }
                        }
                        else if (content.Contains(Constants.LT + Constants.URLSET))
                        {
                            o = Parser.ParseUrlset(content);
                            if (o != null)
                                _urlsets.Add(o as Urlset);
                        }
                    }
                }
                catch (Exception) { }

            });
            if  (_downloadQueue.Any() )
                DownloadUrlsets();
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

    }
}
