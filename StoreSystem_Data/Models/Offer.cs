using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DataLayer.Models
{
    public class Offer
    {
        public int OfferID { get; set; }

        [Required]
        public DateTime ExpiredDate { get; set; }

        [Required]
        public DateTime OfferDate { get; set; }

        public DateTime DeliveryDate { get; set; }

        public int ClientID { get; set; }
        public Client Client { get; set; }

        public int? SaleID { get; set; }
        [ForeignKey("SaleId")]
        public Sale Sale { get; set; }

        public ICollection<ProductOffer> ProductsInOffer { get; set; }
    }
}
