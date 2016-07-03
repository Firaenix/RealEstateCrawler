using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RealEstateCrawler.Service.Models
{
    public class Address
    {
        public string Number;
        public string Street;
        public string Suburb;
        public string PostCode;
        public string State;

        public override string ToString()
        {
            return $"{Number} {Street} {Suburb} {PostCode} {State}".Trim();
        }
    }
}
