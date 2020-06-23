using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MPMProject.Controllers
{
    public class UserController : BaseController
    {
        public IActionResult Index()
        {
            wise_paas_user user = GetLoginUser();
            ViewBag.name = user.name;
            ViewBag.role = user.role;
            return View();
        }
        public IActionResult GetData()
        {
            string myurl = url + "api/v1/configuration/public/user";
            List<wise_paas_user> wise_Paas_Users = CommonHelper<wise_paas_user>.Get(myurl, HttpContext);
            return Json(CommonHelper<wise_paas_user>.Get(myurl, HttpContext));
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Update(wise_paas_user user)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/public/user";

            var typeList = CommonHelper<wise_paas_user>.Get(myurl1, HttpContext);
            var list = typeList.Where(p => p.name == user.name).ToList();

            if (list.Count()>0)
            {
                string myurl = url + "api/v1/configuration/public/user";
                wise_paas_user wise_Paas_User = new wise_paas_user();
                wise_Paas_User.id = user.id;
                wise_Paas_User.name = list[0].name;
                wise_Paas_User.password = list[0].password;
                wise_Paas_User.role = user.role;

                string putString = JsonConvert.SerializeObject(wise_Paas_User);
                string result = PostUrl(myurl, putString);
                JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                if (Convert.ToInt32(jo["code"]) == 200)
                {
                    msg = "Success";
                }
                else
                {
                    msg = "fail";
                }
            }
            else
            {
                msg = "fail";
            }
            return Json(msg);
        }

        public IActionResult ChangePwd(string name,string old_pwd,string new_pwd)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/public/user";

            var typeList = CommonHelper<wise_paas_user>.Get(myurl1, HttpContext);
            var list = typeList.Where(p => p.password == old_pwd && p.name== name).ToList();

            if (list.Count() > 0)
            {
                string myurl = url + "api/v1/configuration/public/user";
                wise_paas_user wise_Paas_User = new wise_paas_user();
                wise_Paas_User.id = list[0].id;
                wise_Paas_User.name = list[0].name;
                wise_Paas_User.password = new_pwd;
                wise_Paas_User.role = list[0].role;

                string putString = JsonConvert.SerializeObject(wise_Paas_User);
                string result = PostUrl(myurl, putString);
                JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                if (Convert.ToInt32(jo["code"]) == 200)
                {
                    msg = "Success";
                }
                else
                {
                    msg = "fail";
                }
            }
            else
            {
                msg = "fail";
            }
            return Json(msg);
        }

        public IActionResult Add(wise_paas_user user)
        {
            string msg = "";
            string myurl1 = url + "api/v1/configuration/public/user";

            var typeList = CommonHelper<wise_paas_user>.Get(myurl1, HttpContext);
            var list = typeList.Where(p => p.name == user.name).ToList();
            if (list.Count()==0)
            {
                string myurl = url + "api/v1/configuration/public/user";
                var postData = JsonConvert.SerializeObject(user);
                string result = PostUrl(myurl, postData);
                JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                if (Convert.ToInt32(jo["code"]) == 200)
                {
                    msg = "Success";
                }
                else {
                    msg = "fail";
                }
            }
            else
            {
                msg = "fail";
            }

            return Json(msg);
        }

        public IActionResult Delete(string name)
        {
            string msg = "";
            string myurl = url + "api/v1/configuration/public/user?user=" + name;
            string result = DeleteUrl(myurl);
            JObject jo = (JObject)JsonConvert.DeserializeObject(result);
            if (Convert.ToInt32(jo["code"]) == 200)
            {
                msg = "Success";
            }
            else
            {
                msg = "fail";
            }
            return Json(msg);
        }
    }
}