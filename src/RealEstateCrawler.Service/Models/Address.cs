using RealEstateCrawler.Service.Interfaces;

namespace RealEstateCrawler.Service.Models
{
    public class Address : IAddress
    {
        public string Number { get; set; }
        public string Street { get; set; }
        public string Suburb { get; set; }
        public string PostCode { get; set; }
        public string State { get; set; }

        public override string ToString()
        {
            return $"{Number} {Street} {Suburb} {PostCode} {State}".Trim();
        }
    }
}
