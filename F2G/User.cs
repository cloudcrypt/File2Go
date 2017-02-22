using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2G
{
    public class User
    {
        public string email { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        public DateTime created { get; set; }

        public List<File> Files { get; set; }
    }
}
