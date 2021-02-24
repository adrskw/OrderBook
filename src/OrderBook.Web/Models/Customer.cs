using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderBook.Web.Models
{
    public class Customer : ITrackable, IDeletable
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

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
    }
}