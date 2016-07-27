using RealEstateCrawler.Service.Interfaces;

namespace RealEstateCrawler.Service.Models
{
    public class Property : IProperty
    {
        public IAddress Address { get; set; }

        public string Bathrooms { get; set; }

        public string Bedrooms { get; set; }

        public string PageUrl { get; set; }

        public string Parking { get; set; }

        public string Price { get; set; }
    }
}
