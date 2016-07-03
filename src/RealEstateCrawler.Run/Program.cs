using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCrawler
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var domain = new RealEstateCrawler.Service.Crawlers.DomainCrawler(new Service.Models.Address()
            {
                Street = "Seaforth"
            });
            domain.Scrape();

            Console.ReadKey();
        }
    }
}
