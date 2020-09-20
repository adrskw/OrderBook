using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderBook.Web.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string CompanyName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        [Required]
        public Address Address { get; set; }

        public ICollection<DeliveryAddress> DeliveryAddresses { get; set; }
        public Invoice Invoice { get; set; }
        public string Note { get; set; }
    }
}