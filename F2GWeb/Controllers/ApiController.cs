using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using F2GWeb.Services;
using F2G.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace F2GWeb.Controllers
{
    public class ApiController : Controller
    {
        private readonly F2GContext _db;
        private readonly IAuthService _auth;

        public ApiController(F2GContext db, IAuthService auth)
        {
            _db = db;
            _auth = auth;
        }

        // GET: /<controller>/
        [HttpGet("api/responses/")]
        public async Task<int> Responses()
        {
            User user = await _auth.getUserAsync();
            return _db.Responses.Where(r => r.User.email == user.email).Count();
        }

        [HttpGet("api/files/delete")]
        public async Task<bool> DeleteFiles()
        {
            User user = await _auth.getUserAsync();
            List<File> files = _db.Files.Where(f => f.response.User.email == user.email).ToList();
            foreach (File f in files)
            {
                _db.Files.Remove(f);
            }
            _db.SaveChanges();
            return true;
        }
    }
}
