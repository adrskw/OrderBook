using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OrderBook.Web.ViewModels
{
    public class DeletePositionViewModel
    {
        [BindProperty(Name = "PositionId")]
        public string Id { get; set; }
    }
}