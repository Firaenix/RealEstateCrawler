using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCrawler.Service.Models
{
    public class Property
    {
        public Address Address;
        public string Price;
        public string PageUrl;
        public string Bedrooms;
        public string Bathrooms;
        public string Parking;
    }
}
