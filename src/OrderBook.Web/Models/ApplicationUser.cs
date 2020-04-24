using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace OrderBook.Web.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Imię nie może przekroczyć 20 znaków")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30, ErrorMessage = "Nazwisko nie może przekroczyć 30 znaków")]
        public string LastName { get; set; }
    }
}