using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;
using Microsoft.AspNetCore.Http;
using Foodie.Core.Models;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly CategoryModel categoryModel;

        public HomeController(CategoryModel categoryModel)
        {
            this.categoryModel = categoryModel;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Loggedin = HttpContext.Session.GetInt32("authenticated").HasValue;
            ViewBag.Categories = await categoryModel.GetAll();

            return View();
        }

        public async Task<IActionResult> About()
        {
            ViewBag.Loggedin = HttpContext.Session.GetInt32("authenticated").HasValue;
            ViewBag.Categories = await categoryModel.GetAll();

            return View();
        }
    }
}
