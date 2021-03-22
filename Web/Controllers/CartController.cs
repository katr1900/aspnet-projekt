using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foodie.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Foodie.Web.Controllers
{
    public class CartController : Controller
    {
        private CategoryModel categoryModel;
        private ProductModel productModel;
        private Helpers.CartHelpers cartHelpers;

        public CartController(CategoryModel categoryModel, ProductModel productModel)
        {
            this.categoryModel = categoryModel;
            this.productModel = productModel;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Loggedin = HttpContext.Session.GetInt32("authenticated").HasValue;
            this.cartHelpers = new Helpers.CartHelpers(this.HttpContext.Session);
            ViewBag.Categories = await categoryModel.GetAll();
            var cart = cartHelpers.GetCart();
            return View(cart);
        }

        [HttpGet]
        public async Task<IActionResult> EmptyCart()
        {
            ViewBag.Loggedin = HttpContext.Session.GetInt32("authenticated").HasValue;
            ViewBag.Categories = await categoryModel.GetAll();
            this.cartHelpers = new Helpers.CartHelpers(this.HttpContext.Session);
            cartHelpers.EmptyCart();
            return new RedirectToActionResult("index", "cart", null);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCart([FromForm]int productId, [FromForm]int quantity)
        {
            ViewBag.Loggedin = HttpContext.Session.GetInt32("authenticated").HasValue;
            ViewBag.Categories = await categoryModel.GetAll();
            this.cartHelpers = new Helpers.CartHelpers(this.HttpContext.Session);
            var product = await productModel.GetProduct(productId);
            if (product != null)
            {
                this.cartHelpers.UpdateCart(product, quantity);
            }

            return new RedirectToActionResult("index", "cart", null);
        }
    }
}
