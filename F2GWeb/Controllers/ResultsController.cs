using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using F2GWeb.Services;
using F2G.Models;
using F2G.ViewModels;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace F2GWeb.Controllers
{
    public class ResultsController : Controller
    {
        private readonly F2GContext _db;
        private readonly IAuthService _auth;

        public ResultsController(F2GContext db, IAuthService auth)
        {
            _db = db;
            _auth = auth;
        }

        public async Task<IActionResult> Index()
        {
            User user = await _auth.getUserAsync();
            ViewData["Files"] = _db.Files
                .Include(f => f.response)
                .ThenInclude(r => r.request)
                .ThenInclude(r => r.client)
                .Where(f => f.response.request.User.email == user.email)
                .ToList();
            return View();
        }

        [HttpGet]
        public FileContentResult Download(int file)
        {
            File fle = _db.Files.FirstOrDefault(f => f.ID == file);
            byte[] bytes = fle.contents;
            return File(bytes, "application/octet-stream", fle.name);
        }

        public void Email(int file)
        {
            File fle = _db.Files
                .Include(f => f.response)
                .ThenInclude(r => r.request)
                .ThenInclude(r => r.client)
                .ThenInclude(c => c.User)
                .FirstOrDefault(f => f.ID == file);
            byte[] bytes = fle.contents;
            EmailService.Send(fle, fle.response.request.client.User.email);
        }

        private void byteArrayToFile(byte[] bytes)
        {

        }
    }
}
