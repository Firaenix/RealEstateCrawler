using RealEstateCrawler.Service.Interfaces;
using RealEstateCrawler.Service.Models;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Text.RegularExpressions;
using RealEstateCrawler.Service.Utilities;
using HtmlAgilityPack;

namespace RealEstateCrawler.Service.Crawlers
{
    public class DomainCrawler : ICrawler
    {
        private readonly string _baseUrl = "http://www.domain.com.au/";
        private readonly string _searchUrl = "rent/";
        private string _completeUrl = "";
        private IList<Property> properties = new List<Property>();

        public DomainCrawler(Address address)
        {
            //_completeUrl = WebUtility.HtmlEncode($"{_baseUrl}{_searchUrl}{address.ToString()}");
            _completeUrl = address.ToString();
        }

        public async void Scrape()
        {
            var html = await DomainUtils.GetDomainPageHtml(await DomainUtils.ResolveAddress(_completeUrl), "rent");
            var htmlDoc = new HtmlAgilityPack.HtmlDocument();
                    
            htmlDoc.LoadHtml(html);

            var searchresults = htmlDoc.GetElementbyId("searchresults");
            var results = searchresults.Descendants().Where(x => x.Name == "li");

            Console.WriteLine($"Total: {results.Count()}");
            foreach (var result in results)
            {
                // If we find a street address, add to the address list
                if (Regex.IsMatch(result.InnerText, DomainUtils.PropertyRegex))
                {
                    var matchGroups = Regex.Match(result.InnerText, DomainUtils.PropertyRegex).Groups;
                    var property = new Property()
                    {
                        Address = new Address
                        {
                            Number = matchGroups["No"].Value,
                            Street = matchGroups["Street"].Value,
                            Suburb = matchGroups["Suburb"].Value,
                            State = matchGroups["State"].Value,
                            PostCode = matchGroups["PostCode"].Value
                        },
                        Price = matchGroups["Price"].Value,
                        Bathrooms = matchGroups["Bath"].Value,
                        Bedrooms = matchGroups["Bed"].Value,
                        Parking = matchGroups["Park"].Value,
                        PageUrl = LocateHref(result)
                    };
                            
                    Console.WriteLine(property.Price);
                    Console.WriteLine(property.Address);
                    Console.WriteLine(property.Bathrooms);
                    Console.WriteLine(property.Bedrooms);
                    Console.WriteLine(property.Parking);
                    Console.WriteLine(property.PageUrl);

                    properties.Add(property);
                }
            }

            Console.WriteLine("Done");
        }

        private string LocateHref(HtmlNode node)
        {
            if (node.Descendants("a").Any())
            {
                var link = node.Descendants("a").First();
                return link.Attributes["href"].Value;
            }
            
            if (node.Descendants().Any())
            {
                return LocateHref(node);
            }

            return string.Empty;
        }
    }
}
