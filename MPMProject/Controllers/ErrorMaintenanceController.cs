﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MPMProject.Controllers
{
    public class ErrorMaintenanceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}