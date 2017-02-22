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
        public async Task<IActionResult> Process(HomeViewModel model)
        {
            User user = await _auth.getUserAsync();
            if (ModelState.IsValid)
            {
                List<File> files = _db.Files.Where(f => f.response.User.email == user.email).ToList();
                foreach (File f in files)
                {
                    _db.Files.Remove(f);
                }
                List<Response> responses = _db.Responses.Where(r => r.client.User.email == user.email).ToList();
                foreach (Response r in responses)
                {
                    _db.Responses.Remove(r);
                }
                _db.SaveChanges();
                int i = 0;
                foreach (Client c in _db.Clients.Where(c => c.User == user).ToList())
                {
                    _db.Requests.Add(new Request() { fileName = model.filename, client = c, User = user });
                    i++;
                }
                _db.SaveChanges();
                ViewData["neededResponses"] = i;
                return View();
            }
            return View("Index", model);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
