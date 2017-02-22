using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using F2GWeb.Services;
using F2G.Models;
using F2G.ViewModels;

namespace F2GWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly F2GContext _db;
        private readonly IAuthService _auth;

        public HomeController(F2GContext db, IAuthService auth)
        {
            _db = db;
            _auth = auth;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            User user = await _auth.getUserAsync();
            if (user != null)
            {
                ViewData["User"] = user;
                ViewData["Clients"] = _db.Clients.Where(c => c.User == user).ToList();
                return View("UserIndex");
            }
            return View("GuestIndex");
        }


        [HttpPost]
        public IActionResult Index(HomeViewModel model)
        {
            if (ModelState.IsValid)
            {
                //_db.Requests.Add(new Request(model));
                //_db.SaveChanges();
                return Content("Requests sent");
            }
            return View(model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
