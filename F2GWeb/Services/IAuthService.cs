using F2GWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace F2GWeb.Services
{
    public interface IAuthService
    {
        User user { get; set; }
        Tuple<bool, string> signIn(User user);
        void signOut();
    }
}
