using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Admin.Models;
using Microsoft.AspNetCore.Http;
using Foodie.Core.Models;

namespace Admin.Controllers
{
    public class HomeController : Controller
    {
        private CategoryModel categoryModel;

        public HomeController(CategoryModel categoryModel)
        {
            this.categoryModel = categoryModel;
        }

        public IActionResult Index()
        {
            if (!HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "login", null);
            }

            return View();
        }
    }
}
