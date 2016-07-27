using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCrawler.Service.Interfaces
{
    public interface ICrawler
    {
        Task<IList<IProperty>> Scrape();
    }
}
