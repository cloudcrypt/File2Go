using F2GWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace F2GWeb.Models
{
    public class Client
    {
        public string name { get; set; }
        public string ip { get; set; }
        public User User { get; set; }
    }
}
