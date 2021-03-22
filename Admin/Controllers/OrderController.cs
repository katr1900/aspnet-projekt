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
    public class OrderController : Controller
    {
        private OrderModel orderModel;

        public OrderController(OrderModel orderModel)
        {
            this.orderModel = orderModel;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "login", null);
            }

            var orders = await orderModel.GetAll();
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "login", null);
            }

            await orderModel.DeleteOrder(id);
            return new RedirectToActionResult("index", "order", null);
        }
    }
}
