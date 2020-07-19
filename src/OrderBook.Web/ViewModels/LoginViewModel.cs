using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderBook.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Login jest wymagany")]
        [Display(Name = "Login", Prompt = "login")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło", Prompt = "hasło")]
        public string Password { get; set; }

        [Display(Name = "Zapamiętaj mnie")]
        public bool RememberMe { get; set; }
    }
}