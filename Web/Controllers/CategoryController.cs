using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foodie.Core.Data;
using Foodie.Core.Entities;
using Foodie.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Foodie.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryModel categoryModel;

        public CategoryController(CategoryModel categoryModel)
        {
            this.categoryModel = categoryModel;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Loggedin = HttpContext.Session.GetInt32("authenticated").HasValue;
            ViewBag.Categories = await categoryModel.GetAll();
            return View();
        }

        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Loggedin = HttpContext.Session.GetInt32("authenticated").HasValue;
            if (id == null && !id.HasValue)
            {
                return NotFound();
            }

            var category = await categoryModel.GetCategory(id.Value);

            if (category == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await categoryModel.GetAll();

            return View(category);
        }
    }
}
