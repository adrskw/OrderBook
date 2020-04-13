using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderBook.Web.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            switch (statusCode)
            {
                case 403:
                    ViewBag.ErrorMessage = "Nie masz uprawnień do wyświetlenia tej strony";
                    break;

                case 404:
                    ViewBag.ErrorMessage = "Przepraszamy, strona nie została znaleziona";
                    break;
            }

            return View("Error");
        }
    }
}