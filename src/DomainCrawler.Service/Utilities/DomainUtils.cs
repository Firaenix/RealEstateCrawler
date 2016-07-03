using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RealEstateCrawler.Service.Utilities
{
    public static class DomainUtils
    {
        public static string PropertyRegex = @"(?i)(?<Price>\$.+)\s*(?<No>(\d)+([A-Z])?(\/((\d)+([A-Z])?))?)\s*(?<Street>.+),\s*(?<Suburb>.+),\s*(?<State>.+)\s*(&n\s*bsp;)(?<PostCode>\w+)\s+(?<Bed>\w+)\s+beds\s+(?<Bath>\w+)\s+bathrooms\s+(?<Park>\w+)";
        public static string LinkRegex = @"href='(?<Link>.+)'\s*data-listing-id";

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
