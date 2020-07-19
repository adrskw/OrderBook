using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderBook.Web.Models;
using OrderBook.Web.ViewModels;

namespace OrderBook.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public HomeController(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [Route("")]
        [Route("Home")]
        [Route("Home/Index")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(userName: model.Login,
                                                                    password: model.Password,
                                                                    isPersistent: model.RememberMe,
                                                                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "dashboard");
                }

                ModelState.AddModelError("", "Logowanie nie powiodło się");
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("index", "home");
        }
    }
}