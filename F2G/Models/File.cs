using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2G.Models
{
    public class File
    {
        public int ID { get; set; }
        public string name { get; set; }
        public byte[] contents { get; set; }
        public DateTime uploaded { get; set; }
        public User user { get; set; }
    }
}
