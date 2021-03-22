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
    public class LoginController : Controller
    {
        private UserModel userModel;

        public LoginController(UserModel userModel)
        {
            this.userModel = userModel;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Loggedin = HttpContext.Session.GetInt32("authenticated").HasValue;
            if (HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "user", null);
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string username, string password)
        {
            ViewBag.Loggedin = HttpContext.Session.GetInt32("authenticated").HasValue;
            var user = await userModel.GetUser(username);

            if (user != null && await userModel.IsCorrectPassword(user.Id, password))
            {
                HttpContext.Session.SetInt32("authenticated", user.Id);
                return new RedirectToActionResult("index", "user", null);
            }

            ViewBag.InvalidCredentials = true;
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("authenticated");
            return new RedirectToActionResult("index", "login", null);
        }
    }
}
