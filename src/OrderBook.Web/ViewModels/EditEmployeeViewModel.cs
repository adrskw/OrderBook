using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderBook.Web.ViewModels
{
    public class EditEmployeeViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Login jest wymagany")]
        [Display(Name = "Login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Podany adres email jest nieprawidłowy")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Imię jest wymagane")]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
    }
}