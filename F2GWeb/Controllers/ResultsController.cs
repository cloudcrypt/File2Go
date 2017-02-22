﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using F2GWeb.Services;
using F2G.Models;
using F2G.ViewModels;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace F2GWeb.Controllers
{
    public class ResultsController : Controller
    {
        private readonly F2GContext _db;
        private readonly IAuthService _auth;

        public ResultsController(F2GContext db, IAuthService auth)
        {
            _db = db;
            _auth = auth;
        }



    }
}
