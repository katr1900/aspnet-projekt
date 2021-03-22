using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Foodie.Core.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Foodie.Web.Controllers
{
    public class CheckoutController : Controller
    {
        private UserModel userModel;
        private OrderModel orderModel;
        private Helpers.CartHelpers cartHelpers;

        public CheckoutController(UserModel userModel, OrderModel orderModel)
        {
            this.userModel = userModel;
            this.orderModel = orderModel;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.Loggedin = HttpContext.Session.GetInt32("authenticated").HasValue;
            if (!HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "login", null);
            }

            cartHelpers = new Helpers.CartHelpers(HttpContext.Session);
            var cart = cartHelpers.GetCart();
            if (cart.CartLines.Count == 0)
            {
                return new RedirectToActionResult("index", "cart", null);
            }

            var user = await userModel.GetUser(HttpContext.Session.GetInt32("authenticated").Value);
            ViewBag.User = user;
            ViewBag.Cart = cart;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout()
        {
            ViewBag.Loggedin = HttpContext.Session.GetInt32("authenticated").HasValue;
            if (!HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "login", null);
            }

            cartHelpers = new Helpers.CartHelpers(HttpContext.Session);
            var cart = cartHelpers.GetCart();
            if (cart.CartLines.Count == 0)
            {
                return new RedirectToActionResult("index", "cart", null);
            }


            var user = await userModel.GetUser(HttpContext.Session.GetInt32("authenticated").Value);
            await orderModel.RegisterOrder(user, cart);

            return View();
        }
    }
}
