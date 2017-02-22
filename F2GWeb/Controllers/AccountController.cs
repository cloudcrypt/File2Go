using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using F2GWeb.Services;
using F2G.Models;
using F2G.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace F2GWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly F2GContext _db;
        private readonly IAuthService _auth;

        public AccountController(F2GContext db, IAuthService auth)
        {
            _db = db;
            _auth = auth;
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
                _db.Users.Add(new User(model));
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
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Tuple<bool, string> result = _auth.signIn(new User(model));
                if (result.Item1)
                {
                    return Redirect("/");
                }
                ViewData["Error"] = result.Item2;
                return View(model);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LogOff()
        {
            _auth.signOut();
            return Redirect("/");
        }

    }
}
