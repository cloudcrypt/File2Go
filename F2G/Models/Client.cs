﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace F2G.Models
{
    public class Client
    {
        public string name { get; set; }
        public string ip { get; set; }
        public bool active { get; set; }
        public User User { get; set; }
    }
}
