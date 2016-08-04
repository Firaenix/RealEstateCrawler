using System.Collections.Generic;
using System.Threading.Tasks;
using RealEstateCrawler.Service.Models;

namespace RealEstateCrawler.Service.Interfaces
{
    public interface IService 
    {
        /// <summary>
        /// Save the retrieved properties to the database
        /// </summary>
        /// <param name="properties"></param>
        Task StoreResults(IList<IProperty> properties);

        /// <summary>
        /// Retrieves the properties associated with the address from the database
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        Task<IList<IProperty>> GetPropertiesByAddress(IAddress address);

        /// <summary>
        /// Searches new properties on the associated Crawler
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        Task<IList<IProperty>> SearchPropertiesByAddress(IAddress address);
    }
}