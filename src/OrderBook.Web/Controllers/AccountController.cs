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
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "dashboard");
            }

            return View();
        }

        [Route("")]
        [Route("Account")]
        [Route("Account/Login")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("index", "dashboard");
            }

            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(userName: model.Login,
                                                                    password: model.Password,
                                                                    isPersistent: model.RememberMe,
                                                                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }

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
            return RedirectToAction("index", "");
        }
    }
}