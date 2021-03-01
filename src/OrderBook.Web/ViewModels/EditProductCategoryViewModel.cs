using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderBook.Web.ViewModels
{
    public class EditProductCategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa kategorii jest wymagana")]
        [Display(Name = "Nazwa kategorii")]
        public string ProductCategoryName { get; set; }
    }
}