using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OrderBook.Web.ViewModels
{
    public class EditPositionViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Nazwa stanowiska jest wymagana")]
        [Display(Name = "Nazwa stanowiska")]
        public string PositionName { get; set; }
    }
}