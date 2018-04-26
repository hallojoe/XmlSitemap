# XmlSitemap
A (.NET Standard 2.0) tool for reading xml sitemapindexes and sitemaps - Work in progress.

The tool currently supports:

 - Read sitemap index
 - Read sitemap urlset
 
## Install
Install using NuGet

`PM> Install-Package HalloJoe.XmlSitemap -Version 0.1.0`

## Example

	// get robots.txt file and read sitemap directives 
	// the robots.txt part can be left out
	IUrl url = new RobotsTxtUrl("https://www.amnesty.org/")

	// or get an absolute sitemap url
	IUrl url = new SitemapUrl("https://www.amnesty.org/sitemap.xml")
	


	using (var loader = new Loader(url))
	{
		var count = loader.Sitemaps.Count;
		Console.WriteLine($"Loaded { count } sitemap{ (count > 1 ? "s" : "") }");
		var sum = loader.Sitemaps.Sum(x => x.Entities.Count);
		Console.WriteLine($"Loaded a total of { sum } entit{ (sum > 1 ? "ies" : "y") }");
		...
	}
