using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Models
{
    public class Sale
    {
        public int SaleID { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public int? OfferID { get; set; }
        [ForeignKey("OfferId")]
        public Offer Offer { get; set; }

        public int ClientID { get; set; }
        public Client Client { get; set; }

        public ICollection<ProductSale> ProductsInSale { get; set; }

    }
}
