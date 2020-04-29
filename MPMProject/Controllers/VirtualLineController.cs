using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Model.virtual_line;

namespace MPMProject.Controllers
{
    public class VirtualLineController : BaseController
    {
        //List<dept> dept = new List<dept>();

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/work_order/virtual_line";
            return Json(CommonHelper<virtual_lineMachine>.Get(myurl, HttpContext));
        }

        public IActionResult Update([FromBody]virtual_line line)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/work_order/virtual_line";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = jo1["data"].ToObject<IList<Model.virtual_line>>();
            var list = typeList.Where(p => p.id != line.id);

            var lists = list.Any(p => p.name_cn == line.name_cn || p.name_en == line.name_en || p.name_tw == line.name_tw);
            if (lists == false)
            {
                string myurl = url + "api/v1/configuration/work_order/virtual_line";
                var postData = JsonConvert.SerializeObject(line);
                string result = PutUrl(myurl, postData);
                JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                switch (Convert.ToInt32(jo["code"]))
                {
                    case 200:
                        msg = "Success";
                        break;
                    case 400:
                        msg = "fail";
                        break;

                }
            }
            else
            {
                msg = "fail";
            }
            return Json(msg);
        }
        public IActionResult Add([FromBody]virtual_line line)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/work_order/virtual_line";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = jo1["data"].ToObject<IList<Model.virtual_line>>();

            var list = typeList.Any(p => p.name_cn == line.name_cn || p.name_en == line.name_en || p.name_tw == line.name_tw);
            if (list == false)//没有重复的
            {
                string myurl = url + "api/v1/configuration/work_order/virtual_line";
                var postData = JsonConvert.SerializeObject(line);
                string result = PostUrl(myurl, postData);
                JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                switch (Convert.ToInt32(jo["code"]))
                {
                    case 200:
                        msg = "Success";
                        break;
                    case 400:
                        msg = "fail";
                        break;

                }
            }
            else
            {
                msg = "fail";
            }
            return Json(msg);
        }
        public IActionResult Delete([FromBody]virtual_line line)
        {
            string myurl = url + "api/v1/configuration/work_order/virtual_line?id=" + line.id.ToString();
            string result = DeleteUrl(myurl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    Json("Success");
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
            return Json("Success");
        }
        /// <summary>
        /// 设备一旦被某一条线选中，则其余线不可选这个设备
        /// </summary>
        /// <param name="group_id"></param>
        /// <returns></returns>
        //public JsonResult Getmachine()
        //{
        //    List<machine> machines = new List<machine>();
        //    var purl = url + "api/v1/configuration/public/machine";
        //    var result1 = GetUrl(purl);
        //    JObject jo = (JObject)JsonConvert.DeserializeObject(result1);
        //    var list = jo["data"].ToObject<IList<machine>>();

        //    string myurl = url + "api/v1/configuration/work_order/virtual_line";
        //    string result = GetUrl(myurl);
        //    JObject myjo = (JObject)JsonConvert.DeserializeObject(result);
        //    var mylist = myjo["data"].ToObject<IList<virtual_lineMachine>>();

        //    if (mylist.Count>0) {
        //        for (int i=0;i<mylist.Count;i++) {

        //            var index = mylist[i].Machines.ToList();
        //            for (int j=0;j<index.Count;j++) {
        //                machine machineRes = new machine();
        //                machineRes.id = index[j].machine_id;
        //                machineRes.name_cn = index[j].name_cn;
        //                machineRes.name_en = index[j].name_en;
        //                machineRes.name_tw = index[j].name_tw;
        //                machineRes.description = index[j].description;
        //                machineRes.area_node_id = index[j].area_node_id;
        //                machines.Add(machineRes);
        //            }
                    
        //        }
            
        //    }
        //    var otherPersons = list.Where(p => !machines.Select(q => q.id).Contains(p.id));
        //    return Json(otherPersons);

        //    //var otherPersons = list.Where(p => !data.Select(q => q.person_id).Contains(p.id));
        //    //mylist.Count[0].Machines[0].machine_id
        //    //return Json(jo["data"]);

        //}
        ////
        ///当前这条线选中某一设备，其余线也可以选择这个设备
        public JsonResult Getmachine(int group_id)
        {
            List<machine> machines = new List<machine>();
            var purl = url + "api/v1/configuration/public/machine";
            var list = CommonHelper<machine>.Get(purl, HttpContext);
            string myurl = url + "api/v1/configuration/work_order/virtual_line";
            var mylist = CommonHelper<virtual_lineMachine>.Get(myurl, HttpContext);
            var data = mylist.FirstOrDefault(p => p.id == group_id).Machines;
            var otherPersons = list.Where(p => !data.Select(q => q.machine_id).Contains(p.id));
            return Json(otherPersons);
        }

        //虚拟线下设备查询
        public JsonResult GetmachineList(int group_id)
        {
            string myurl = url + "api/v1/configuration/work_order/virtual_line";
            var list = CommonHelper<virtual_lineMachine>.Get(myurl, HttpContext);
            var data = list.FirstOrDefault(p => p.id == group_id).Machines;
            return Json(data);
        }

        public IActionResult AddMachine(int id, int virtualLine_id)
        {//"https://api-mpm.wise-paas.cn/api/v1/configuration/work_order/virtual_line/444?machine_id=444"
            
          //设备绑定
                string[] Machine = Convert.ToString(HttpContext.Request.Form["Machine"]).Split(',');
                if (Machine.Length > 0 && Machine[0] != "")
                {
                    bool flag = true;
                    for (int i = 0; i < Machine.Length; i++)
                    {
                        string machinePutUrl = url + "api/v1/configuration/work_order/virtual_line/" + virtualLine_id + "?machine_id=" + Convert.ToInt32(Machine[i]);
                        string machinePutData = "{{" +
                                "\"virtualLine_id\":{0}," +
                                "\"machine_id\":{1}" +
                                "}}";
                        machinePutData = string.Format(machinePutData, virtualLine_id, Convert.ToInt32(Machine[i]) );
                        string machinePutResult = PostUrl(machinePutUrl, machinePutData);
                        JObject joMachinePut = (JObject)JsonConvert.DeserializeObject(machinePutResult);
                        if (Convert.ToInt32(joMachinePut["code"]) != 200)
                        {
                            flag = false;
                        }
                    }
                    if (flag)
                    {
                        return Json("Success");
                    }
                    else
                    {
                        return Json("Fail");
                    }
                }
                else
                {
                    return Json("Success");
                }
        }

        public IActionResult DeleteMachine(int id, int group_id)
        {
            string myurl = url + "api/v1/configuration/work_order/virtual_line/" + group_id + "?machine_id=" + id;
            string result = DeleteUrl(myurl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    Json("Success");
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
            return Json("Success");
        }
    }
}