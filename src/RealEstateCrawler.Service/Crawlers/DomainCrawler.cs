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
        private string _currentUrl = "http://www.domain.com.au/home/search";
        private IList<IProperty> _properties = new List<IProperty>();

        public DomainCrawler()
        {
        }

        public async Task<IList<IProperty>> Scrape()
        {
            if (string.IsNullOrWhiteSpace(_address))
                return null;

            var resolvedAddress = await ResolveAddress(_address);
            var html = await GetInitialPageHtml(resolvedAddress, "rent");
            var htmlDoc = new HtmlDocument();

            htmlDoc.LoadHtml(html);
            
            try 
            {
                var pageNumber = 1;
                while (PageHasResults(htmlDoc)) 
                {
                    RetrieveResultsFromPage(htmlDoc);

                    pageNumber++;
                    html = await GetPageHtmlByPageNumber(pageNumber);
                    htmlDoc.LoadHtml(html);
                }            
            }
            catch (Exception e) 
            {
                Console.WriteLine(e);
            }
            
            Console.WriteLine("Done");
            Console.WriteLine($"Scraped {_properties.Count} properties.");

            return _properties;
        }

        private bool PageHasResults(HtmlDocument doc) 
        {
            var noResults = doc.DocumentNode.Descendants("div")
                            .Where(x => x.Attributes.Contains("class") &&
                                    x.Attributes["class"].Value.Contains("no-result"));

            if (noResults.Any()) 
            {
                return false;
            }
            return true;
        }

        private void RetrieveResultsFromPage(HtmlDocument htmlDoc) 
        {
            var searchresults = htmlDoc.GetElementbyId("searchresults");
            var results = searchresults.Descendants().Where(x => x.Name == "li");

            Console.WriteLine($"Total: {results.Count()}");

            var count = 0;
            foreach (var result in results)
            {
                // If we find a street address, add to the address list
                if (Regex.IsMatch(result.InnerText, DomainUtils.AddressRegex))
                {
                    var addressGroups = Regex.Match(result.InnerText, DomainUtils.AddressRegex).Groups;
                    var priceGroups = Regex.Match(result.InnerText, DomainUtils.PriceRegex).Groups;
                    var bedGroups = Regex.Match(result.InnerText, DomainUtils.BedRegex).Groups;
                    var bathGroups = Regex.Match(result.InnerText, DomainUtils.BathRegex).Groups;
                    var parkingGroups = Regex.Match(result.InnerText, DomainUtils.ParkingRegex).Groups;

                    var property = new Property()
                    {
                        Address = new Address
                        {
                            Number = addressGroups["No"].Value,
                            Street = addressGroups["Street"].Value,
                            Suburb = addressGroups["Suburb"].Value,
                            State = addressGroups["State"].Value,
                            PostCode = addressGroups["PostCode"].Value
                        },
                        Price = priceGroups["Price"].Value,
                        Bathrooms = bathGroups["Bath"].Value,
                        Bedrooms = bedGroups["Bed"].Value,
                        Parking = parkingGroups["Park"].Value,
                        PageUrl = LocateHref(result)
                    };

                    Console.WriteLine($"Price: {property.Price}");
                    Console.WriteLine(property.Address);
                    Console.WriteLine($"Bathrooms: {property.Bathrooms}");
                    Console.WriteLine($"Bedrooms: {property.Bedrooms}");
                    Console.WriteLine($"Parking: {property.Parking}");
                    Console.WriteLine($"URL: {property.PageUrl}");

                    _properties.Add(property);
                    count++;
                }
                else 
                {
                    Console.WriteLine("---------------No Match!------------------");
                    Console.WriteLine(result.InnerText);
                    Console.WriteLine("------------------------------------------");
                }
            }
            


            Console.WriteLine($"Found {count}/{results.Count()} valid listings");
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

        public async Task<string> GetPageHtmlByPageNumber(int pageNumber)
        {
            var myHttpClient = new HttpClient();
            var response = await myHttpClient.GetAsync($"{_currentUrl}?page={pageNumber}");
            var uri = response.RequestMessage.RequestUri;

            _currentUrl = $"http://{uri.Host}{uri.AbsolutePath}";
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetInitialPageHtml(string address, string searchType)
        {
            var formContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("Terms.Mode", searchType),
                new KeyValuePair<string, string>("Terms.Suburb", address),
                new KeyValuePair<string, string>("Terms.SurroundingSuburbs", "false")
            });

            var myHttpClient = new HttpClient();
            var response = await myHttpClient.PostAsync(_currentUrl, formContent);

            _currentUrl = response.RequestMessage.RequestUri.AbsoluteUri;
            return await response.Content.ReadAsStringAsync();
        }

        public void SetAddress(IAddress address)
        {
            _address = address.ToString();
        }
    }
}
