using F2GWeb.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace F2GWeb.Models
{
    public class Response
    {
        public int ID { get; set; }
        public bool success { get; set; }
        public Request request { get; set; }
    }
}
