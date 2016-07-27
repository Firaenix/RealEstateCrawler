namespace RealEstateCrawler.Service.Interfaces
{
    public interface IAddress 
    {
        string Number { get; set; }
        string Street { get; set; }
        string Suburb { get; set; }
        string PostCode { get; set; }
        string State { get; set; }
    }
}
