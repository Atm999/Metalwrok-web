using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MPMProject.Controllers
{
    /// <summary>
    /// 公共配置——二级标签
    /// </summary>
    public class SecondLevelTagsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}