using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2G.Models
{
    public class Response
    {
        public int ID { get; set; }
        public bool success { get; set; }
        public string fileName { get; set; }
        //public Request request { get; set; }
        public Client client { get; set; }
        public User User { get; set; }
    }
}
