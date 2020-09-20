using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderBook.Web.Models
{
    public enum DeliveryMethodCarrier
    {
        GLS,
        InpostPaczkomaty
    }

    public class DeliveryMethod
    {
        public int Id { get; set; }

        [Required]
        public DeliveryMethodCarrier Carrier { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public bool IsCashOnDelivery { get; set; }
    }
}