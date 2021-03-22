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
using Foodie.Core.Entities;

namespace Admin.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryModel categoryModel;

        public CategoryController(CategoryModel categoryModel)
        {
            this.categoryModel = categoryModel;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "login", null);
            }

            var categories = await categoryModel.GetAll();
            return View(categories);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (!HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "login", null);
            }

            var category = await categoryModel.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (!HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "login", null);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Category request)
        {
            if (!HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "login", null);
            }

            var category = await categoryModel.CreateCategory(request);
            
            return new RedirectToActionResult("index", "category", null);
        }

        [HttpPost]
        public async Task<IActionResult> Details(Category request)
        {
            if (!HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "login", null);
            }

            var category = await categoryModel.UpdateCategory(request);
            
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "login", null);
            }

            await categoryModel.DeleteCategory(id);
            return new RedirectToActionResult("index", "category", null);
        }
    }
}
