using Microsoft.AspNetCore.Mvc;

namespace OrderBook.Web.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        public IActionResult Login()
        {
            return View();
        }
    }
}