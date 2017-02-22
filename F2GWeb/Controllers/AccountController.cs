using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using F2GWeb.ViewModels;
using F2GWeb.Services;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace F2GWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly F2GContext _db;

        public AccountController(F2GContext db)
        {
            _db = db;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // GET: /<controller>/
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Add(new Models.User(model));
                _db.SaveChanges();
                return Content("Registered!");
            }
            return View(model);
        }


        // GET: /<controller>/
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // GET: /<controller>/
        [HttpPost]
        public async Task<IActionResult>Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Add(new Models.User(model));
                _db.SaveChanges();
                return Content("Logged in!");
            }
            return View(model);
        }
    }
}
