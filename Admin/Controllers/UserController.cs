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
    public class UserController : Controller
    {
        private UserModel userModel;

        public UserController(UserModel userModel)
        {
            this.userModel = userModel;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "login", null);
            }

            var users = await userModel.GetAll();
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (!HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "login", null);
            }

            await userModel.DeleteUser(id);
            return new RedirectToActionResult("index", "user", null);
        }
    }
}
