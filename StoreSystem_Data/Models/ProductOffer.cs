using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Models
{
    public class ProductOffer
    {
        public int ProductID { get; set; }
        public Product Product { get; set; }

        public int OfferID { get; set; }
        public Offer Offer { get; set; }

        public decimal ProductDiscount { get; set; }
    }
}
