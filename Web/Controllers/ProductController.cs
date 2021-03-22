using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foodie.Core.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Foodie.Core.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Foodie.Web.Controllers
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

        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Loggedin = HttpContext.Session.GetInt32("authenticated").HasValue;
            if (id == null || !id.HasValue)
            {
                return NotFound();
            }

            var product = await productModel.GetProduct(id.Value);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await categoryModel.GetAll();

            return View(product);
        }
    }
}
