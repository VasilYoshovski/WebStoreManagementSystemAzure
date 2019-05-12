using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSystem.Services.Dto
{
    public class OfferWithTotalDto
    {
        public int OfferID { get; set; }
        public string ClientName { get; set; }
        public decimal Discount { get; set; }
        public DateTime CreatedOn { get; set; }
        public decimal Total { get; set; }
    }
}
