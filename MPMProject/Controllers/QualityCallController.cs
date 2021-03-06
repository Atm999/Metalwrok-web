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
    public class QualityCallController : BaseController
    {
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
            string myurl = url + "api/v1/configuration/public/machine";
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

        /// <summary>
        /// 根据machinename查找error_log数据，显示画面
        /// </summary>
        /// <returns></returns>
        public JsonResult GetIndex(string machinename) {
            string myurl = url + "api/v1/configuration/andon/error_log/1?status=1";
            string result = GetUrl(myurl);
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

        public JsonResult GetIndexqd(string machinename)
        {
            string myurl = url + "api/v1/configuration/andon/error_log/1?status=2";
            string result = GetUrl(myurl);
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
        public JsonResult Getcall(int type, int machine_id)
        {
            string myurl = url + "api/v1/client/error/onsite/" + type + "?machine_id=" + machine_id ;
            string postData = "{{" +
                               "\"type\":{0}," +
                               "\"machine_id\":{1}," +
                               "}}";
            postData = string.Format(postData, type, machine_id);
            string result = PostUrl(myurl, postData);
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
        public JsonResult GetPerson()
        {
            string myurl = url + "api/v1/configuration/public/person";
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
        /// <summary>
        /// 签到
        /// </summary>
        /// <param name="type"></param>
        /// <param name="machine_id"></param>
        /// <returns></returns>
        public JsonResult GetQd(int type, int machine_id,string number, int log_id)
        {
            string myurl = url + "api/v1/client/error/onsite/" + type + "?machine_id=" + machine_id+"&log_id="+log_id+ "&number=" + number;
            string postData = "{{" +
                               "\"type\":{0}," +
                               "\"machine_id\":{1}," +
                                "\"log_id\":{2}," +
                                 "\"number\":\"{3}\"," +
                               "}}";
            postData = string.Format(postData, type, machine_id, log_id, number);
            string result = PutUrl(myurl, postData);
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
        /// 解除
        /// </summary>
        /// <param name="type"></param>
        /// <param name="machine_id"></param>
        /// <param name="person_id"></param>
        /// <param name="log_id"></param>
        /// <returns></returns>
        public JsonResult GetJc(int type, int machine_id, int count, int log_id)
        {
            string myurl = url + "api/v1/client/error/onsite/" + type + "?machine_id=" + machine_id + "&log_id=" + log_id + "&count=" + count;
            string postData = "{{" +
                               "\"type\":{0}," +
                               "\"machine_id\":{1}," +
                                "\"log_id\":{2}," +
                                 "\"count\":{3}," +
                               "}}";
            postData = string.Format(postData, type, machine_id, log_id, count);
            string result = PostUrl(myurl, postData);
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