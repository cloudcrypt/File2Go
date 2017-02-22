using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F2G.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Authentication;

namespace F2GWeb.Services
{
    public class AuthService : IAuthService
    {
        //public User user { get; set; }
        private readonly F2GContext _db;
        private readonly IHttpContextAccessor _ctxAccessor;

        public AuthService(F2GContext db, IHttpContextAccessor contextAccessor)
        {
            _db = db;
            _ctxAccessor = contextAccessor;
        }

        public Tuple<bool, string> signIn(User user)
        {
            User dbUser = _db.Users.FirstOrDefault(u => u.email == user.email);
            if (dbUser == null) { return new Tuple<bool, string>(false, "User does not exist"); }
            if (dbUser.hash == user.hash)
            {
                List<Claim> claims = new List<Claim>
                {
                    new Claim("fname", dbUser.fname),
                    new Claim("lname", dbUser.lname),
                    new Claim("email", dbUser.email),
                };
                ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "Login");
                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(userIdentity);
                _ctxAccessor.HttpContext.Authentication.SignInAsync("CookieMiddlewareInstance", userPrincipal,
                    new AuthenticationProperties
                    {
                        IsPersistent = false,
                        AllowRefresh = true
                    });
                return new Tuple<bool, string>(true, "Success");
            }
            return new Tuple<bool, string>(false, "Incorrect user name or password");
        }

        public void signOut()
        {
            _ctxAccessor.HttpContext.Authentication.SignOutAsync("CookieMiddlewareInstance");
        }

        public async Task<User> getUserAsync()
        {
            AuthenticateInfo authInfo = await _ctxAccessor.HttpContext.Authentication.GetAuthenticateInfoAsync("CookieMiddlewareInstance");
            if (authInfo.Principal == null || !authInfo.Principal.Identity.IsAuthenticated) { return null; }
            string fname = authInfo.Principal.FindFirst("fname").Value;
            string lname = authInfo.Principal.FindFirst("lname").Value;
            string email = authInfo.Principal.FindFirst("email").Value;
            return new User() { fname = fname, lname = lname, email = email };
        }
    }
}
