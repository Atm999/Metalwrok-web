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
        //全局变量用于保存数据
        public List<NavTree> treeNodes = new List<NavTree>();

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetData()
        {
            //配置的节点
            var areaNodeUrl = url + "api/v1/configuration/public/area_node";
            var resAreaNode = GetUrl(areaNodeUrl);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(resAreaNode);
            var areaNodeList = jo1["data"].ToObject<IList<Model.area_node>>();

            //绑定的设备
            var machineUrl = url + "api/v1/configuration/public/machine";
            var resMachine = GetUrl(machineUrl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(resMachine);
            var machineList = jo["data"].ToObject<IList<Model.machine>>();
  
            int id = -1;
            var datAreaNode = (from p in areaNodeList
                       select new { name = p.name_cn, p.id,  p.upper_id }).ToList();
            var datMachine = (from q in machineList
                        select new { name = q.name_cn, id,  upper_id = q.area_node_id }).ToList();
            List<object> final=new List<object>() {
                new { name="根节点",id=0,upper_id="-"}
                };
            var dat = final.Union(datAreaNode).Union(datMachine).ToList();
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    Json(dat);
                    break;
                case 400:
                    break;
                case 410:
                    break;
                case 411:
                    break;
                default:
                    break;
            }

            return Json(dat);
        }

    }
}