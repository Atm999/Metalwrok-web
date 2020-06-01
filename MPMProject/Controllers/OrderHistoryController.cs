using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Model.wo_config;

namespace MPMProject.Controllers
{
    public class OrderHistoryController : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/work_order/produced_work_order";
            List<wo_config_excute> list = CommonHelper<wo_config_excute>.Get(myurl, HttpContext);
            list = list.OrderByDescending(x => x.id).ToList();
            return Json(list);
        }
    }
}