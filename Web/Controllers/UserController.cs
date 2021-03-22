using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foodie.Core.Data;
using Foodie.Core.Entities;
using Foodie.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Foodie.Web.Controllers
{
    public class UserController : Controller
    {
        private CategoryModel categoryModel;
        private UserModel userModel;

        public UserController(CategoryModel categoryModel, UserModel userModel)
        {
            this.categoryModel = categoryModel;
            this.userModel = userModel;
        }

        public async Task<IActionResult> Index()
        {
            if (!HttpContext.Session.GetInt32("authenticated").HasValue)
            {
                return new RedirectToActionResult("index", "login", null);
            }
            ViewBag.Loggedin = HttpContext.Session.GetInt32("authenticated").HasValue;
            ViewBag.Categories = await categoryModel.GetAll();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            ViewBag.Loggedin = HttpContext.Session.GetInt32("authenticated").HasValue;
            ViewBag.Categories = await categoryModel.GetAll();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm]User user, [FromForm]Address address, [FromForm]Person person)
        {
            user.Person = person;
            user.Address = address;
            await userModel.RegisterUser(user);
            
            return new RedirectToActionResult("index", "user", null);
        }
    }
}
