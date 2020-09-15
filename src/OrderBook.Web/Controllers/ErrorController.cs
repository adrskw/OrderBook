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
        [Route("Error")]
        [Route("Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            ViewBag.Title = "Wystąpił błąd";

            switch (statusCode)
            {
                case 403:
                    ViewBag.ErrorMessage = "Nie masz uprawnień do wyświetlenia tej strony";
                    break;

                case 404:
                    ViewBag.ErrorMessage = "Przepraszamy, strona, której szukasz, nie została znaleziona";
                    break;

                default:
                    ViewBag.ErrorMessage = "Serwer napotkał niespodziewane trudności, które uniemożliwiły zrealizowanie żądania";
                    break;
            }

            return View("Error");
        }
    }
}