﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MPMProject.Controllers
{
    public class MaterialCallController : BaseController
    {
        public string url = "http://api-mpm.wise-paas.cn/";
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 获取下拉选虚拟线的数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetMachine()
        {
            url = url + "api/v1/configuration/public/machine";
            string result = GetUrl(url);
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

        /// <summary>
        /// 根据machinename查找error_log数据，显示画面
        /// </summary>
        /// <returns></returns>
        public JsonResult GetIndex(string machinename) {
            url = url + "api/v1/configuration/andon/error_log/2?status=1";
            string result = GetUrl(url);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            var list = jo["data"].ToObject<IList<error_log>>();
            var data = list.Where(p => p.machine_name == machinename);
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
        /// <summary>
        /// 呼叫
        /// </summary>
        /// <param name="type"></param>
        /// <param name="machine_id"></param>
        /// <returns></returns>
        public JsonResult Getcall(int type, int machine_id,int count,string material_name)
        {
            url = url + "api/v1/client/error/onsite/" + type + "?machine_id=" + machine_id +"&count="+count+ "&material_name="+ material_name;
            string postData = "{{" +
                               "\"type\":{0}," +
                               "\"machine_id\":{1}," +
                                "\"count\":{2}," +
                                 "\"material_name\":\"{3}\"," +
                               "}}";
            postData = string.Format(postData, type, machine_id,count, material_name);
            string result = PostUrl(url, postData);
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
        public JsonResult GetIndexqd(string machinename)
        {
            url = url + "api/v1/configuration/andon/error_log/2?status=2";
            string result = GetUrl(url);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            var list = jo["data"].ToObject<IList<error_log>>();
            var data = list.Where(p => p.machine_name == machinename);
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
        /// <summary>
        /// 解除
        /// </summary>
        /// <param name="type"></param>
        /// <param name="machine_id"></param>
        /// <param name="person_id"></param>
        /// <param name="log_id"></param>
        /// <returns></returns>
        public JsonResult GetJc(int type, int machine_id,  int log_id)
        {
            url = url + "api/v1/client/error/onsite/" + type + "?machine_id=" + machine_id + "&log_id=" + log_id ;
            string postData = "{{" +
                               "\"type\":{0}," +
                               "\"machine_id\":{1}," +
                                "\"log_id\":{2}," +
                                
                               "}}";
            postData = string.Format(postData, type, machine_id, log_id);
            string result = PostUrl(url, postData);
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