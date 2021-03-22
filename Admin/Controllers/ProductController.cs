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
    public class ProductController : Controller
    {
        private ProductModel productModel;
        private CategoryModel categoryModel;

        public ProductController(ProductModel productModel, CategoryModel categoryModel)
        {
            this.productModel = productModel;
            this.categoryModel = categoryModel;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "login", null);
            }

            var products = await productModel.GetAll();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            if (!HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "login", null);
            }

            var product = await productModel.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            if (!HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "login", null);
            }

            ViewBag.Categories = await categoryModel.GetAll();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product request)
        {
            if (!HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "login", null);
            }

            var product = await productModel.CreateProduct(request);
            
            return new RedirectToActionResult("index", "product", null);
        }

        [HttpPost]
        public async Task<IActionResult> Details(Product request)
        {
            if (!HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "login", null);
            }

            var product = await productModel.UpdateProduct(request);
            
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "login", null);
            }

            await productModel.DeleteProduct(id);
            return new RedirectToActionResult("index", "product", null);
        }
    }
}
