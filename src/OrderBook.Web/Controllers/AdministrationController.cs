using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.Differencing;
using OrderBook.Web.Models;
using OrderBook.Web.ViewModels;

namespace OrderBook.Web.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
                                        UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Position Actions

        public IActionResult ListPositions()
        {
            var positions = roleManager.Roles.OrderBy(position => position.Name);

            return View(positions);
        }

        [HttpGet]
        public IActionResult CreatePosition()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePosition(CreatePositionViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = model.PositionName
                };

                var result = await roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListPositions", "Administration");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditPosition(string id)
        {
            var position = await roleManager.FindByIdAsync(id);

            if (position == null)
            {
                ModelState.AddModelError("id", "Nie znaleziono stanowiska o danym identyfikatorze");

                return View();
            }

            if (position.Name == "Admin")
            {
                ModelState.AddModelError("", "Nie możesz edytować stanowiska administratora");

                return View();
            }

            var model = new EditPositionViewModel()
            {
                Id = id,
                PositionName = position.Name
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditPosition(EditPositionViewModel model)
        {
            var position = await roleManager.FindByIdAsync(model.Id);

            if (position == null)
            {
                ModelState.AddModelError("id", "Nie znaleziono stanowiska o danym identyfikatorze");

                return View();
            }
            else if (position.Name == "Admin")
            {
                ModelState.AddModelError("", "Nie możesz edytować stanowiska administratora");

                return View();
            }
            else
            {
                position.Name = model.PositionName;
                var result = await roleManager.UpdateAsync(position);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListPositions");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePosition(DeletePositionViewModel model)
        {
            var position = await roleManager.FindByIdAsync(model.Id);

            if (position == null)
            {
                ModelState.AddModelError("id", "Nie znaleziono stanowiska o danym identyfikatorze");
            }
            else if (position.Name == "Admin")
            {
                ModelState.AddModelError("", "Nie możesz usunąć stanowiska administratora");
            }
            else
            {
                var result = await roleManager.DeleteAsync(position);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListPositions");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View();
        }

        #endregion Position Actions
    }
}