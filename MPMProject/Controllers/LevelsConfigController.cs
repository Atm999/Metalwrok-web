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
            return View();
        }
    }
}