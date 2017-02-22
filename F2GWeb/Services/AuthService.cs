using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F2G.Models;
using Microsoft.AspNetCore.Http;

namespace F2GWeb.Services
{
    public class AuthService : IAuthService
    {
        public User user { get; set; }
        private readonly F2GContext _db;
        private readonly IHttpContextAccessor _ctxAccessor;

        public AuthService(F2GContext db)
        {
            _db = db;
            //_ctxAccessor = contextAccessor;
        }

        public Tuple<bool, string> signIn(User user)
        {
            User dbUser = _db.Users.FirstOrDefault(u => u.email == user.email);
            if (dbUser == null) { return new Tuple<bool, string>(false, "User does not exist"); }
            if (dbUser.hash == user.hash)
            {
                this.user = dbUser;
                //_ctxAccessor.HttpContext.Authentication.S
                return new Tuple<bool, string>(true, "Success");
            }
            return new Tuple<bool, string>(false, "Incorrect user name or password");
        }

        public void signOut()
        {
            user = null;
        }
    }
}
