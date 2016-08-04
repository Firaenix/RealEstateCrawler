using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateCrawler.Service.Crawlers;
using RealEstateCrawler.Service.Interfaces;

namespace RealEstateCrawler.Service.Services
{
    public class DomainService : IService
    {
        DomainCrawler _domainCrawler = new DomainCrawler();

        public DomainService()
        {
        }

        public async Task<IList<IProperty>> GetPropertiesByAddress(IAddress address)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<IProperty>> SearchPropertiesByAddress(IAddress address)
        {
            _domainCrawler.SetAddress(address);
            var properties = await _domainCrawler.Scrape();
            
            return properties;
        }

        public Task StoreResults(IList<IProperty> properties)
        {
            throw new NotImplementedException();
        }
    }
}