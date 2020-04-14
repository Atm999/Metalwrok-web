using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static Model.virtual_line;
using static Model.wo_config;

namespace MPMProject.Controllers
{
    public class OrderController : BaseController
    {
        public static int Count = 0;

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/work_order/wo_config";
            string result = GetUrl(myurl);
            JObject jo= (JObject)JsonConvert.DeserializeObject(result);
            var list = jo["data"].ToObject<IList<wo_config>>();
            var data = list.Where(p => p.status != 3);
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    Json(jo["data"]);
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
            return Json(data);
        }
        //虚拟线下设备查询
        public JsonResult GetmachineList(int group_id, int id)
        {
            var purl = url + "api/v1/configuration/work_order/virtual_line";
            var result1 = GetUrl(purl);
            JObject jos = (JObject)JsonConvert.DeserializeObject(result1);
            var lists = jos["data"].ToObject<IList<virtual_lineMachine>>();
            //查询当前虚拟线的设备list
            var data = lists.FirstOrDefault(p => p.id == group_id).Machines;

            List<machine> ma = new List<machine>();
            string myurl = url + "api/v1/configuration/work_order/wo_config";
            string result = GetUrl(myurl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            var list = jo["data"].ToObject<IList<wo_config>>();
            //查询该工单下设备的id
            string idlist = list.FirstOrDefault(p => p.id == id).lbr_formula;
            string [] Machine = idlist.Split(';');
            var machine_id = 0;
            machine machine = new machine();
            if (Machine.Length > 0 && Machine[0] != "")
            {
                for (int i = 0; i < Machine.Length; i++)
                {
                    machine_id = Convert.ToInt32(Machine[i]);
                    machine = data.FirstOrDefault(p => p.id == machine_id);
                    ma.Add(machine);
                }
                
            }
            return Json(ma);
        }
        ///当前这条线选中某一设备，其余线也可以选择这个设备
        public JsonResult Getmachine(int group_id)
        {
            List<machine> machines = new List<machine>();
            var purl = url + "api/v1/configuration/public/machine";
            var result1 = GetUrl(purl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result1);

            var list = jo["data"].ToObject<IList<machine>>();
            string myurl = url + "api/v1/configuration/work_order/virtual_line";
            string result = GetUrl(myurl);
            JObject myjo = (JObject)JsonConvert.DeserializeObject(result);
            var mylist = myjo["data"].ToObject<IList<virtual_lineMachine>>();
            var data = mylist.FirstOrDefault(p => p.id == group_id).Machines;
            //var otherPersons = list.Where(p => !data.Select(q => q.machine_id).Contains(p.id));
            return Json(data);
        }
        public IActionResult AddMachine(int id, int virtual_line_id, string work_order, string part_num, int standard_num, int shift, bool auto, int status ,string standard_time,int order_index,DateTime create_time)
        {//"https://api-mpm.wise-paas.cn/api/v1/configuration/work_order/virtual_line/444?machine_id=444"

            //设备绑定
            string[] Machine = Convert.ToString(HttpContext.Request.Form["Machine"]).Split(',');
            if (Machine.Length > 0 && Machine[0] != "")
            {
                bool flag = true;
                string str = "";
                for (int i = 0; i < Machine.Length; i++)
                {
                    str += Machine[i] + ";";
                   
                }
                //删除最后一位
                str = str.Remove(str.Length - 1, 1);
                wo_config ec = new wo_config();
                ec.lbr_formula = str;
                ec.id = id;
                ec.virtual_line_id = virtual_line_id;
                ec.work_order = work_order;
                ec.part_num = part_num;
                ec.standard_num = standard_num;
                ec.shift = shift;
                ec.auto = auto;
                ec.status = status;
                ec.standard_time = standard_time;
                ec.order_index = order_index;
                ec.create_time = create_time;
                string myurl = url + "api/v1/configuration/work_order/wo_config";
                var postData = JsonConvert.SerializeObject(ec);
                string machinePutResult = PutUrl(myurl, postData);

                JObject joMachinePut = (JObject)JsonConvert.DeserializeObject(machinePutResult);
                if (Convert.ToInt32(joMachinePut["code"]) != 200)
                {
                    flag = false;
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

        public IActionResult Update([FromBody]wo_config wo)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/work_order/wo_config";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = jo1["data"].ToObject<IList<Model.wo_config>>();
            var list = typeList.Where(p => p.id != wo.id);

            var lists = list.Any(p => p.work_order == wo.work_order);
            if (lists == false)
            {
                string myurl = url + "api/v1/configuration/work_order/wo_config";
                var postData = JsonConvert.SerializeObject(wo);
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
        /// <summary>
        /// 确保数据类型一致
        /// </summary>
        /// <param name="work"></param>
        /// <returns></returns>
        public IActionResult Add([FromBody]wo_config work)
        {
            var standard_time = work.standard_time;
            var list = standard_time.Split(";");
            string msg = "";
            if (list.Count() == Count)
            {
                work.create_time = DateTime.UtcNow;
                string myurl1 = url + "api/v1/configuration/work_order/wo_config";
                string result1 = GetUrl(myurl1);
                JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
                var typeList = jo1["data"].ToObject<IList<Model.wo_config>>();

                var lists = typeList.Any(p => p.work_order == work.work_order);
                if (lists == false)//没有重复的
                {
                    string myurl = url + "api/v1/configuration/work_order/wo_config";
                    var postData = JsonConvert.SerializeObject(work);
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
               
            }
            return Json(msg);
        }

        public IActionResult Delete([FromBody]wo_config wo)
        {
            string myurl = url + "api/v1/configuration/work_order/wo_config?id=" + wo.id.ToString();
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

        public JsonResult Getline()
        {
            var purl = url + "api/v1/configuration/work_order/virtual_line";
            var result1 = GetUrl(purl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result1);

            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    Json(jo["data"]);
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
            return Json(jo["data"]);
        }

        public JsonResult GetmachineCount(int lineid)
        {
            var purl = url + "api/v1/configuration/work_order/virtual_line";
            var result1 = GetUrl(purl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result1);
            var list = jo["data"].ToObject<IList<virtual_lineMachine>>();
            var data = list.FirstOrDefault(p => p.id == lineid).Machines;
            Count = data.Count();
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    Json(jo["data"]);
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
            return Json(data.Count());
        }

    }
}