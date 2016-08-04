using System.Collections.Generic;
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
        private IService _domainService;

        public DomainController(IService domainService)
        {
            _domainService = domainService;
        }

        [HttpPost("lookup")]
        public async Task<IEnumerable<IProperty>> Lookup([FromBody]Address address = null)
        {
            return await _domainService.SearchPropertiesByAddress(address);
        }

        [HttpGet("suburb/{queryAddress}")]
        public async Task<IEnumerable<IProperty>> Suburb([FromRoute]string queryAddress)
        {
            var address = new Address
            {
                Suburb = queryAddress
            };

            return await _domainService.SearchPropertiesByAddress(address);
        }
    }
}
