using F2GWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace F2GWeb.Models
{
    public class Request
    {
        public int ID { get; set; }
        public string fileName { get; set; }
        public Client client { get; set; }
        public User User { get; set; }
    }
}
