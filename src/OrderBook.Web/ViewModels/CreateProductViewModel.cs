using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OrderBook.Web.ViewModels
{
    public class CreateProductViewModel
    {
        [Required(ErrorMessage = "Nazwa produktu jest wymagana")]
        [Display(Name = "Nazwa produktu")]
        public string ProductName { get; set; }

        public SelectList ProductCategorySelectList { get; set; }

        [Display(Name = "Kategoria produktu")]
        public int? ProductCategoryId { get; set; }

        [Required(ErrorMessage = "Cena produktu jest wymagana")]
        [Display(Name = "Cena produktu")]
        [Range(0.01, 99999999, ErrorMessage = "Cena produktu musi być większa od 0")]
        public decimal ProductPrice { get; set; }

        [Required(ErrorMessage = "Waluta produktu jest wymagana")]
        [Display(Name = "Waluta")]
        public string ProductCurrency { get; set; }

        [Display(Name = "Nr oferty Allegro")]
        [Range(0, int.MaxValue, ErrorMessage = "Nr oferty Allegro nie może być ujemny")]
        public int? AllegroOfferId { get; set; }
    }
}