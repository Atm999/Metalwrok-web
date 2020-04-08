using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Http;
using Model;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using MPMProject.Models;
using Wise_Paas;
using Newtonsoft.Json.Linq;

namespace MPMProject.Controllers
{
    public class GeneralController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult LoginHandle(string userName, string passWord)
        {
            if(GlobalVar.IsCloud)
            {
                if (Integration.Login(HttpContext, userName, passWord, "Metalwork"))
                {
                    string myurl = url + "api/v1/configuration/public/user";
                    wise_paas_user wise_Paas_User = new wise_paas_user();
                    wise_Paas_User.name = userName;
                    wise_Paas_User.password = passWord;
                    wise_Paas_User.role = userName;
                    var postData = JsonConvert.SerializeObject(wise_Paas_User);
                    string result = PostUrl(myurl, postData);
                    
                    return Json("Success");
                }
                else
                {
                    return Json("Failed");
                }
            }
            else
            {
                string myurl = url + "api/v1/configuration/public/user?user={0}&password={1}";
                myurl = string.Format(myurl, userName, passWord);
                string result = PutUrl(myurl);
                JObject jo = (JObject)JsonConvert.DeserializeObject(result);
                switch (Convert.ToInt32(jo["code"]))
                {
                    case 200:
                        if(jo["data"] != null)
                        {
                            SetCookies( jo["data"][0]["name"].ToString(), jo["data"][0]["role"].ToString());
                            GlobalVar.role = jo["data"][0]["role"].ToString();
                            return Json("Success");
                        } 
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
                return Json("Failed");
            }
        }

        /// <summary>
        /// 注销用户
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            if (GlobalVar.IsCloud)
            {
                Integration.Logout(HttpContext);
                
            }
            else
            {
                DeleteCookies();
            }
            return RedirectToAction("index", "General");
        }

        /// <summary>
        /// 获取cookies
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>返回对应的值</returns>
        protected string GetCookies(string key)
        {
            HttpContext.Request.Cookies.TryGetValue(key, out string value);
            if (string.IsNullOrEmpty(value))
                value = string.Empty;
            return value;
        }

        public  void SetCookies(string userName, string role)
        {
            CookieOptions co = new CookieOptions();
            co.IsEssential = true;
            co.Expires = DateTime.Now.AddHours(1);
            HttpContext.Response.Cookies.Append("userName", userName, co);
            HttpContext.Response.Cookies.Append("role", role, co);
        }

        public void DeleteCookies()
        {
            HttpContext.Response.Cookies.Delete("userName");
            HttpContext.Response.Cookies.Delete("role");
        }
    }
}