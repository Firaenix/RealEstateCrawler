using System.Collections.Generic;
using System.Threading.Tasks;

namespace RealEstateCrawler.Service.Interfaces
{
    public interface ICrawler
    {
        Task<IList<IProperty>> Scrape();
        void SetAddress(IAddress address);
    }
}
