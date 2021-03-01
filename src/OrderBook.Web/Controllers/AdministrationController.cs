using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.Differencing;
using OrderBook.Web.Models;
using OrderBook.Web.Repositories;
using OrderBook.Web.ViewModels;

namespace OrderBook.Web.Controllers
{
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IProductCategoryRepository productCategoryRepository;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
                                        UserManager<ApplicationUser> userManager,
                                        IProductCategoryRepository productCategoryRepository)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.productCategoryRepository = productCategoryRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Employee Actions

        public IActionResult ListEmployees()
        {
            var employees = userManager.Users;

            return View(employees);
        }

        [HttpGet]
        public IActionResult CreateEmployee()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEmployee(CreateEmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser employee = new ApplicationUser()
                {
                    UserName = model.Login,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };

                var result = await userManager.CreateAsync(employee, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListEmployees", "Administration");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditEmployee(string id)
        {
            ViewBag.Title = "Edytuj pracownika";

            var employee = await userManager.FindByIdAsync(id);

            if (employee == null)
            {
                ViewBag.ErrorMessage = "Nie znaleziono pracownika o danym identyfikatorze";

                return View("Error");
            }

            var model = new EditEmployeeViewModel()
            {
                Id = id,
                Login = employee.UserName,
                Email = employee.Email,
                FirstName = employee.FirstName,
                LastName = employee.LastName
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditEmployee(EditEmployeeViewModel model)
        {
            ViewBag.Title = "Edytuj pracownika";

            var employee = await userManager.FindByIdAsync(model.Id);

            if (employee == null)
            {
                ViewBag.ErrorMessage = "Nie znaleziono pracownika o danym identyfikatorze";

                return View("Error");
            }
            else if (employee.UserName == "admin" && employee.UserName != model.Login)
            {
                ModelState.AddModelError("login", "Nie można zmienić loginu administratora");
            }
            else
            {
                employee.UserName = model.Login;
                employee.Email = model.Email;
                employee.FirstName = model.FirstName;
                employee.LastName = model.LastName;

                var result = await userManager.UpdateAsync(employee);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListEmployees");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            ViewBag.Title = "Usuń pracownika";

            var employee = await userManager.FindByIdAsync(id);

            if (employee == null)
            {
                ViewBag.ErrorMessage = "Nie znaleziono pracownika o danym identyfikatorze";
            }
            else if (employee.UserName == "admin")
            {
                ViewBag.ErrorMessage = "Nie możesz usunąć administratora";
            }
            else
            {
                var result = await userManager.DeleteAsync(employee);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListEmployees");
                }

                ViewBag.ErrorMessage = "Wystąpił nieznany błąd podczas usuwania pracownika";
            }

            return View("Error");
        }

        #endregion Employee Actions

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
            ViewBag.Title = "Edytuj stanowisko";

            var position = await roleManager.FindByIdAsync(id);

            if (position == null)
            {
                ViewBag.ErrorMessage = "Nie znaleziono stanowiska o danym identyfikatorze";

                return View("Error");
            }

            if (position.Name == "Admin")
            {
                ViewBag.ErrorMessage = "Nie możesz edytować stanowiska administratora";

                return View("Error");
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
            ViewBag.Title = "Edytuj stanowisko";

            var position = await roleManager.FindByIdAsync(model.Id);

            if (position == null)
            {
                ViewBag.ErrorMessage = "Nie znaleziono stanowiska o danym identyfikatorze";

                return View("Error");
            }
            else if (position.Name == "Admin")
            {
                ViewBag.ErrorMessage = "Nie możesz edytować stanowiska administratora";

                return View("Error");
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
        public async Task<IActionResult> DeletePosition(string id)
        {
            ViewBag.Title = "Usuń stanowisko";

            var position = await roleManager.FindByIdAsync(id);

            if (position == null)
            {
                ViewBag.ErrorMessage = "Nie znaleziono stanowiska o danym identyfikatorze";
            }
            else if (position.Name == "Admin")
            {
                ViewBag.ErrorMessage = "Nie możesz usunąć stanowiska administratora";
            }
            else
            {
                var result = await roleManager.DeleteAsync(position);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListPositions");
                }

                ViewBag.ErrorMessage = "Wystąpił nieznany błąd podczas usuwania stanowiska";
            }

            return View("Error");
        }

        #endregion Position Actions
        #region Product Category Actions

        public IActionResult ListProductCategories()
        {
            var productCategories = productCategoryRepository.GetAllProductCategories();

            return View(productCategories);
        }

        [HttpGet]
        public IActionResult CreateProductCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProductCategory(CreateProductCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                ProductCategory productCategory = new ProductCategory()
                {
                    Name = model.ProductCategoryName
                };

                productCategoryRepository.Add(productCategory);
                productCategoryRepository.Save();

                return RedirectToAction("ListProductCategories");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult EditProductCategory(int id)
        {
            var productCategory = productCategoryRepository.GetById(id);

            if (productCategory == null)
            {
                ViewBag.Title = "Edytuj kategorię produktów";
                ViewBag.ErrorMessage = "Nie znaleziono kategorii o danym identyfikatorze";

                return View("Error");
            }

            var model = new EditProductCategoryViewModel()
            {
                Id = id,
                ProductCategoryName = productCategory.Name
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult EditProductCategory(EditProductCategoryViewModel model)
        {
            var productCategory = productCategoryRepository.GetById(model.Id);

            if (productCategory == null)
            {
                ViewBag.Title = "Edytuj kategorię produktów";
                ViewBag.ErrorMessage = "Nie znaleziono kategorii o danym identyfikatorze";

                return View("Error");
            }

            productCategory.Name = model.ProductCategoryName;
            productCategoryRepository.Update(productCategory);
            productCategoryRepository.Save();

            return RedirectToAction("ListProductCategories");
        }

        [HttpPost]
        public IActionResult DeleteProductCategory(int id)
        {
            var productCategory = productCategoryRepository.GetById(id);

            if (productCategory == null)
            {
                ViewBag.Title = "Usuń kategorię produktów";
                ViewBag.ErrorMessage = "Nie znaleziono kategorii o danym identyfikatorze";
            }

            productCategoryRepository.Delete(productCategory.Id);
            productCategoryRepository.Save();

            return RedirectToAction("ListProductCategories");
        }

        #endregion Product Category Actions
    }
}