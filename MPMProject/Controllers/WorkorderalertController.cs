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
    public class WorkorderalertController : BaseController
    {

        public IActionResult Index()
        {
            return View();
        }
        public JsonResult GetData()
        {
            string myurl = url + "api/v1/configuration/andon/work_order_alert_detail";
            string result = GetUrl(myurl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            var machineList = jo["data"].ToObject<IList<Model.work_order_alertDto>>();

            var purl = url + "api/v1/configuration/public/tag_extra";
            var result1 = GetUrl(purl);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var tag_info_extraList = jo1["data"].ToObject<IList<Model.tag_info_extra>>();

            List<object> list = new List<object>();
            foreach (var obj in machineList)
            {
                tag_info_extra tag_info = new tag_info_extra();
                if (obj.alert_type == 0)
                {
                    tag_info = tag_info_extraList
                        .Where(x => x.target_type == 2 && x.target_id == obj.virtual_line_id && x.tag_type_sub_id == 19)
                        .FirstOrDefault();
                }
                else if (obj.alert_type == 1)
                {
                    tag_info = tag_info_extraList
                        .Where(x => x.target_type == 2 && x.target_id == obj.virtual_line_id && x.tag_type_sub_id == 20)
                        .FirstOrDefault();
                }
                else if (obj.alert_type == 2)
                {
                    tag_info = tag_info_extraList
                        .Where(x => x.target_type == 2 && x.target_id == obj.virtual_line_id && x.tag_type_sub_id == 21)
                        .FirstOrDefault();
                }
                else if (obj.alert_type == 3)
                {
                    tag_info = tag_info_extraList
                        .Where(x => x.target_type == 2 && x.target_id == obj.virtual_line_id && x.tag_type_sub_id == 22)
                        .FirstOrDefault();
                }
                object ob = new
                {
                    obj.id,
                    obj.virtual_line_id,
                    obj.alert_type,
                    obj.minimum,
                    obj.maximum,
                    obj.notice_group_id,
                    obj.notice_type,
                    obj.enable,
                    obj.virtual_line.name_cn,
                    nname = obj.notice_group.name_cn,
                    tag_info?.name,
                    tag_info?.description,
                    extraid = tag_info?.id,
                    tag_info?.tag_type_sub_id
                };
                list.Add(ob);
            }

            switch (Convert.ToInt32(jo["code"]))
            {
                case 200:
                    Json(jo["data"]);
                    break;
                case 400:
                    break;
                case 410:
                    break;
              
            }
            return Json(list);
        }
      
        //Tag点修改/新增
        public IActionResult UpdateTagInfo(tag_info_extra tag_Info)
        {
            if (tag_Info.utilization_rate_types == 0)
            {
                tag_Info.tag_type_sub_id = 19;
            }
            else if (tag_Info.utilization_rate_types == 1)
            {
                tag_Info.tag_type_sub_id = 20;
            }
            else if (tag_Info.utilization_rate_types == 2)
            {
                tag_Info.tag_type_sub_id =21;
            }
            else if (tag_Info.utilization_rate_types == 3)
            {
                tag_Info.tag_type_sub_id = 22;
            }
            tag_Info.target_type = 2;
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
        public IActionResult Update([FromBody]work_order_alert ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/work_order_alert";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = jo1["data"].ToObject<IList<Model.work_order_alert>>();
            var list = typeList.Where(p => p.id != ec.id);

            var lists = list.Any(p => p.virtual_line_id == ec.virtual_line_id && p.alert_type == ec.alert_type);
            if (lists == false) {
                string myurl = url + "api/v1/configuration/andon/work_order_alert";
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
        public IActionResult Add([FromBody]work_order_alert ec)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/andon/work_order_alert";
            string result1 = GetUrl(myurl1);
            JObject jo1 = (JObject)JsonConvert.DeserializeObject(result1);
            var typeList = jo1["data"].ToObject<IList<Model.work_order_alert>>();

            var list = typeList.Any(p => p.virtual_line_id == ec.virtual_line_id && p.alert_type == ec.alert_type);
            if (list == false)//没有重复的
            {
                string myurl = url + "api/v1/configuration/andon/work_order_alert";
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

        public IActionResult Delete([FromBody]work_order_alert ec)
        {
            string myurl = url + "api/v1/configuration/andon/work_order_alert?id=" + ec.id.ToString();
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

        public JsonResult GetVline()
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