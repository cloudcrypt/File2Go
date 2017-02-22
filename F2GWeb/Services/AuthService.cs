using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F2GWeb.Models;

namespace F2GWeb.Services
{
    public class AuthService : IAuthService
    {
        public User user { get; set; }
        private readonly F2GContext _db;

        public AuthService(F2GContext db)
        {
            _db = db;
        }

        public Tuple<bool, string> signIn(User user)
        {
            User dbUser = _db.Users.First(u => u.email == user.email);
            if (dbUser == null) { return new Tuple<bool, string>(false, "User does not exist"); }
            if (dbUser.hash == user.hash)
            {
                return new Tuple<bool, string>(true, "Success");
            }
            return new Tuple<bool, string>(false, "Incorrect user name or password");
        }

        public void signOut(User user)
        {
            user = null;
        }
    }
}
