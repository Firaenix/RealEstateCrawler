using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealEstateCrawler.Service.Interfaces;
using RealEstateCrawler.Service.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace RealEstateCrawler.Run
{
    [Route("api/[controller]")]
    public class DomainController : Controller
    {
        private ICrawler _domainCrawler;

        public DomainController(ICrawler domainCrawler)
        {
            _domainCrawler = domainCrawler;
        }

        [HttpPost("lookup")]
        public async Task<IEnumerable<IProperty>> Lookup([FromBody]Address address = null)
        {
            _domainCrawler.SetAddress(address);

            return await _domainCrawler.Scrape();
        }

        [HttpGet("suburb/{address}")]
        public async Task<IEnumerable<IProperty>> Suburb([FromRoute]string address)
        {
            _domainCrawler.SetAddress(new Address
            {
                Suburb = address
            });

            return await _domainCrawler.Scrape();
        }
    }
}
