using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderBook.Web.Models
{
    public enum PaymentType
    {
        Online,
        BankTransfer,
        CashOnDelivery
    }

    public class Payment
    {
        [Key]
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [Required]
        public PaymentType Type { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PaidAmount { get; set; }

        [Required]
        public string Currency { get; set; }

        [Required]
        public DateTime FinishedAt { get; set; }

        [Required]
        public bool IsPaidAmountCorrect { get; set; }

        public string Note { get; set; }

        public Order Order { get; set; }
    }
}