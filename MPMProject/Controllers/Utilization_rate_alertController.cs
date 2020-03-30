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
    public class Utilization_rate_alertController : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/andon/utilization_rate_alert_detail";
            string result = GetUrl(myurl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            var machineList = jo["data"].ToObject<IList<Model.utilization_rate_alertDto>>();

            var purl = url + "api/v1/configuration/public/tag_extra";
            var result1 = GetUrl(purl);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var tag_info_extraList = jo1["data"].ToObject<IList<Model.tag_info_extra>>();

            List<object> list = new List<object>();
            foreach (var obj in machineList)
            {
                tag_info_extra tag_info = new tag_info_extra();
                if (obj.utilization_rate_type == 0)
                {
                    tag_info = tag_info_extraList
                        .Where(x => x.target_type == 0 && x.target_id == obj.machine_id && x.tag_type_sub_id == 15)
                        .FirstOrDefault();
                }
                else if (obj.utilization_rate_type == 1)
                {
                    tag_info = tag_info_extraList
                        .Where(x => x.target_type == 0 && x.target_id == obj.machine_id && x.tag_type_sub_id == 16)
                        .FirstOrDefault();
                }
                else if (obj.utilization_rate_type == 2)
                {
                    tag_info = tag_info_extraList
                        .Where(x => x.target_type == 0 && x.target_id == obj.machine_id && x.tag_type_sub_id == 17)
                        .FirstOrDefault();
                }
                object ob = new
                {
                    id = obj.id,
                    machine_id = obj.machine_id,
                    utilization_rate_type = obj.utilization_rate_type,
                    notice_group_id = obj.notice_group,
                    notice_type = obj.notice_type,
                    maximum = obj.maximum,
                    minimum = obj.minimum,
                    enable = obj.enable,
                    name_cn = obj.machine.name_cn,
                    nname = obj.notice_group.name_cn,
                    name = tag_info?.name,
                    description = tag_info?.description,
                    extraid = tag_info?.id,
                    tag_type_sub_id = tag_info?.tag_type_sub_id
                };
                list.Add(ob);
            }
            //    var dat =
            //        from p in
            //        machineList
            //        join
            //        y in tag_info_extraList.Where(n => n.tag_type_sub_id == 15 || n.tag_type_sub_id == 16 || n.tag_type_sub_id == 17)
            //        on p.machine_id equals y.target_id
            //        into g
            //        from o in g.DefaultIfEmpty()
            //        select new
            //        {
            //            p.id,
            //            p.machine_id,
            //            p.utilization_rate_type,
            //            p.notice_group_id,
            //            p.notice_type,
            //            p.maximum,
            //            p.minimum,
            //            p.enable,
            //            p.machine.name_cn,
            //            nname = p.notice_group.name_cn,
            //            o?.name,//o!=null?o.name:null
            //            o?.description,
            //            extraid = o?.id,
            //            o?.tag_type_sub_id
            //        };
            //    dat = dat.Where(tag_Info =>
            //    {
            //        if (tag_Info.tag_type_sub_id == null)
            //            return true; 
            //        if (tag_Info.utilization_rate_type == 0)
            //        {
            //            return tag_Info.tag_type_sub_id == 15;
            //        }
            //        else if (tag_Info.utilization_rate_type == 1)
            //        {
            //            return tag_Info.tag_type_sub_id == 16;
            //        }
            //        else if (tag_Info.utilization_rate_type == 2)
            //        {
            //            return tag_Info.tag_type_sub_id == 17;
            //        }
            //        else
            //            return true;
            //    });
            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    Json(jo["data"]);
                    break;
                case 400:
                    break;

            }
            return Json(list);
        }
        //Tag点修改/新增
        public IActionResult UpdateTagInfo(tag_info_extra tag_Info)
        {
            if (tag_Info.utilization_rate_types == 0)
            {
                tag_Info.tag_type_sub_id = 15;
            }
            else if (tag_Info.utilization_rate_types == 1)
            {
                tag_Info.tag_type_sub_id = 16;
            }
            else if (tag_Info.utilization_rate_types == 2)
            {
                tag_Info.tag_type_sub_id = 17;
            }
            tag_Info.target_type = 0;
            string tagInfoUrl = url + "api/v1/configuration/public/tag_extra";
            int id = tag_Info.id;
            //新增
            if (id == 0)
            {
                var tagInfoPostData = JsonConvert.SerializeObject(tag_Info);
                string tagInfoPostResult = PostUrl(tagInfoUrl, tagInfoPostData);
                JObject joMachinePost = (JObject)JsonConvert.DeserializeObject(tagInfoPostResult);
                if (Convert.ToInt32(joMachinePost["code"]) == 200)
                {
                    return Json("Success");
                }
                else
                {
                    return Json("Fail");
                }
            }
            else
            {//修改
                var tagInfoPostData = JsonConvert.SerializeObject(tag_Info);
                string tagInfoPutResult = PutUrl(tagInfoUrl, tagInfoPostData);
                JObject joMachinePut = (JObject)JsonConvert.DeserializeObject(tagInfoPutResult);
                if (Convert.ToInt32(joMachinePut["code"]) == 200)
                {
                    return Json("Success");
                }

                else
                {
                    return Json("Fail");
                }

            }
        }
        public IActionResult Update([FromBody]utilization_rate_alert ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/utilization_rate_alert";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = jo1["data"].ToObject<IList<Model.utilization_rate_alert>>();
            var list = typeList.Where(p => p.id != ec.id);

            var lists = list.Any(p => p.machine_id == ec.machine_id && p.utilization_rate_type == ec.utilization_rate_type);
            if (lists == false)
            {
                string myurl = url + "api/v1/configuration/andon/utilization_rate_alert";
                var postData = JsonConvert.SerializeObject(ec);
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
        public IActionResult Add([FromBody]utilization_rate_alert ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/utilization_rate_alert";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = jo1["data"].ToObject<IList<Model.utilization_rate_alert>>();

            var list = typeList.Any(p => p.machine_id == ec.machine_id && p.utilization_rate_type == ec.utilization_rate_type);
            if (list == false)//没有重复的
            {
                string myurl = url + "api/v1/configuration/andon/utilization_rate_alert";
                var postData = JsonConvert.SerializeObject(ec);
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

        public IActionResult Delete([FromBody]utilization_rate_alert ec)
        {
            string myurl = url + "api/v1/configuration/andon/utilization_rate_alert?id=" + ec.id.ToString();
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

        public JsonResult Getmachine()
        {
            var purl = url + "api/v1/configuration/public/machine";
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

        public JsonResult Getgroup()
        {
            string myurl = url + "api/v1/configuration/andon/notification_group";
            string result = GetUrl(myurl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
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
    }
}