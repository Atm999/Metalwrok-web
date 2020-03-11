using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MPMProject.Controllers
{
    public class OnsiteMachineStatusController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Json("连接成功");
        }

        [HttpPost]
        public IActionResult Search()
        {
            return Json("连接成功");
        }

        [HttpPost]
        public IActionResult Query()
        {
            return Json("连接成功");
        }


    }
}