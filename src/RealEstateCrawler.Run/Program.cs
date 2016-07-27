using System;
using RealEstateCrawler.Service.Crawlers;
using RealEstateCrawler.Service.Models;

namespace RealEstateCrawler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var domain = new DomainCrawler(new Address()
            {
                Street = "North Sydney"
            });

            // TODO: This is shit, fix this
            domain.Scrape().Wait();
            
            Console.ReadKey();
        }
    }
}
