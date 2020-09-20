using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderBook.Web.Models
{
    public class Invoice
    {
        [Key]
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public string CompanyName { get; set; }

        public string CompanyTaxId { get; set; }

        [Required]
        public string NaturalPersonName { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string ZipCode { get; set; }

        [Required]
        public string Country { get; set; }

        public Customer Customer { get; set; }
    }
}