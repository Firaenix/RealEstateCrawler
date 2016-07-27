namespace RealEstateCrawler.Service.Interfaces
{
    public interface IProperty
    {
        IAddress Address { get; set; }
        string Price { get; set; }
        string PageUrl { get; set; }
        string Bedrooms { get; set; }
        string Bathrooms { get; set; }
        string Parking { get; set; }
    }
}