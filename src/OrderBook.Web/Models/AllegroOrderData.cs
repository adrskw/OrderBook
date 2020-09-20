using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderBook.Web.Models
{
    public class AllegroOrderData
    {
        [Key]
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        public string AllegroOrderId { get; set; }
        public string BuyerLogin { get; set; }
        public string BuyerId { get; set; }
        public bool IsBuyerGuest { get; set; } = false;
        public string PaymentId { get; set; }
        public string DeliveryMethodId { get; set; }
        public string DeliveryMethodName { get; set; }
        public bool IsDeliveryMethodSmart { get; set; } = false;

        public Order Order { get; set; }
    }
}