﻿using Microsoft.AspNetCore.Mvc;

namespace OrderBook.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
    }
}