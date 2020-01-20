using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MPMProject.Controllers
{
    /// <summary>
    /// 公共配置——层级配置
    /// </summary>
    public class LevelsConfigController : BaseController
    {
        public IActionResult Index()
        {
            if (!string.IsNullOrEmpty(Request.Query["tab"]))
            {
                ViewBag.tab = 1;
            }
            return View();
        }
    }
}