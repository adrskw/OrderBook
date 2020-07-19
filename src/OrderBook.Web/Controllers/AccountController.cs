using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OrderBook.Web.Models;
using OrderBook.Web.ViewModels;

namespace OrderBook.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> signInManager;

        public AccountController(SignInManager<ApplicationUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        [Route("")]
        [Route("Account")]
        [Route("Account/Login")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [Route("")]
        [Route("Account")]
        [Route("Account/Login")]
        [HttpPost]
        [AllowAnonymous]
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