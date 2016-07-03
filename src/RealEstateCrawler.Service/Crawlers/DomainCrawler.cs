using RealEstateCrawler.Service.Interfaces;
using RealEstateCrawler.Service.Models;
using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using RealEstateCrawler.Service.Utilities;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;

namespace RealEstateCrawler.Service.Crawlers
{
    public class DomainCrawler : ICrawler
    {
        private string _address = "";
        private IList<Property> properties = new List<Property>();

        public DomainCrawler(Address address)
        {
            _address = address.ToString();
        }

        public async void Scrape()
        {
            var resolvedAddress = await ResolveAddress(_address);
            var html = await GetDomainPageHtml(resolvedAddress, "rent");
            var htmlDoc = new HtmlDocument();
                    
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
                            
                    Console.WriteLine($"Price: {property.Price}");
                    Console.WriteLine(property.Address);
                    Console.WriteLine($"Bathrooms: {property.Bathrooms}");
                    Console.WriteLine($"Bedrooms: {property.Bedrooms}");
                    Console.WriteLine($"Parking: {property.Parking}");
                    Console.WriteLine($"URL: {property.PageUrl}");

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

        public async static Task<string> ResolveAddress(string addressString)
        {
            var fixedString = addressString.Replace(' ', '+');

            using (var myHttpClient = new HttpClient())
            {
                var response = await myHttpClient.GetAsync($"http://www.domain.com.au/SearchSuggest/SuggestSuburbs?prefixText={fixedString}&count=1");

                var content = JArray.Parse(await response.Content.ReadAsStringAsync());

                return content.First().Value<string>("Name");
            }
        }

        public async static Task<string> GetDomainPageHtml(string address, string searchType)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Terms.Mode", searchType),
                new KeyValuePair<string, string>("Terms.Suburb", address),
                new KeyValuePair<string, string>("Terms.SurroundingSuburbs", "false")
            });

            var myHttpClient = new HttpClient();
            var response = await myHttpClient.PostAsync("http://www.domain.com.au/home/search", formContent);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
