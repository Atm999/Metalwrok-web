using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MPMProject.Controllers
{
   
    /// <summary>
    /// 公共配置——一级标签
    /// </summary>
    public class FirstLevelTagsController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/public/tag_type";
            return Json(CommonHelper<tag_type>.Get(myurl,HttpContext));
        }
    }
}