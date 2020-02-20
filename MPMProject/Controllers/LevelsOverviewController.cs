using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MPMProject.Controllers
{
    /// <summary>
    /// 公共配置——层级概览
    /// </summary>
    public class LevelsOverviewController : BaseController
    {
        public string url = "http://api-mpm.wise-paas.cn/";

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            //由于设备为末节点，将其id全部设为-1
            int id = -1;
            //根节点
            List<object> final = new List<object>() { new { name = "根节点", id = 0, upper_id = "-" } };

            //配置的节点
            var areaNodeUrl = url + "api/v1/configuration/public/area_node";
            var resAreaNode = GetUrl(areaNodeUrl);
            JObject joAreaNode = (JObject)JsonConvert.DeserializeObject(resAreaNode);

            //绑定的设备
            var machineUrl = url + "api/v1/configuration/public/machine";
            var resMachine = GetUrl(machineUrl);
            JObject joMachine = (JObject)JsonConvert.DeserializeObject(resMachine);

            if (Convert.ToInt32(joAreaNode["code"]) == 200 && Convert.ToInt32(joMachine["code"]) == 200)
            {
                var areaNodeList = joAreaNode["data"].ToObject<IList<Model.area_node>>();
                var machineList = joMachine["data"].ToObject<IList<Model.machine>>();
                //层级
                var datAreaNode = (from p in areaNodeList
                                   select new { name = p.name_cn, p.id, p.upper_id }).ToList();
                //设备
                var datMachine = (from q in machineList
                                  where q.area_node_id !=0
                                  select new { name = q.name_cn, id, upper_id = q.area_node_id }).ToList();
                var dat = final.Union(datAreaNode).Union(datMachine).ToList();
                return Json(dat);
            }
            else
            {
                return Json(final);
            }
            
        }

    }
}