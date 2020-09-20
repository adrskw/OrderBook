using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderBook.Web.Models
{
    public enum OrderStatus
    {
        Cancelled,
        Unpaid,
        Paid,
        ReadyForShipment,
        Sent,
        Returned,
        Refunded
    }

    public enum OrderType
    {
        Manual,
        Allegro
    }

    public class Order
    {
        public int Id { get; set; }

        [Required]
        public OrderType Type { get; set; }

        [Required]
        public Customer Customer { get; set; }

        [Required]
        public ICollection<OrderDetail> Products { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }
        public DeliveryAddress SelectedDeliveryAddress { get; set; }
        public int NumberOfPackages { get; set; }
        public Payment Payment { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalToPay { get; set; }

        public string Currency { get; set; }

        public bool IsInvoiceRequired { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }
        public DateTime? SentDate { get; set; }
        public string Note { get; set; }
        public AllegroOrderData AllegroOrderData { get; set; }
    }
}