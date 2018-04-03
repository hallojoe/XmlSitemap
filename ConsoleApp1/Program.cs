using System;
using System.Linq;
using HalloJoe.XmlSitemap;
namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var loader = new Loader("https://babyfryd.dk"))
            {
                var count = loader.Sitemaps.Count;
                Console.WriteLine($"Loaded { count } sitemap{ (count > 1 ? "s" : "") }");
                var sum = loader.Sitemaps.Sum(x => x.Entities.Count);
                Console.WriteLine($"Loaded a total of { sum } entit{ (sum > 1 ? "ies" : "y") }");
            }
            Console.ReadKey();
        }
    }
}
