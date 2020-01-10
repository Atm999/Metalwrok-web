using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MPMProject.Controllers
{
    /// <summary>
    /// 公共配置——层级概览
    /// </summary>
    public class LevelsOverviewController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}