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

namespace MPMProject.Controllers
{
    public class GeneralController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult LoginHandle(string userName, string passWord, string redirectUri = "https://portal-sso.wise-paas.cn/web/portals.html", string bindingCode = "")
        {
            string authUrl = "https://portal-sso.wise-paas.cn/v2.0/auth";//Sign in. (for frontend service integration)
            List<Object> param = new List<object>() { new { username = userName, password = passWord } };

            //Sign in. (for backend service integration) Return the TokenPackage JSON model.
            HttpHelper.HttpResponseData authNative0 = HttpHelper.Post(authUrl, "native", param[0]);

            if (authUrl == "401")
            {
                return Json("Fail");
            }
            else
            {
                TokenPackage tokenPackage = JsonConvert.DeserializeObject<TokenPackage>(authNative0.Data);
                string accessToken = tokenPackage.accessToken;

                string userUrl = "https://portal-sso.wise-paas.cn/v2.0/users/me";//Sign in. (for frontend service integration)
                HttpHelper.HttpResponseData usersMe = HttpHelper.Get(userUrl, accessToken);
                User user = JsonConvert.DeserializeObject<User>(usersMe.Data);


                CookieOptions co = new CookieOptions();
                co.IsEssential = true;
                co.Domain = "wise-paas.cn";

                HttpContext.Response.Cookies.Append("EIToken", accessToken, co);
                HttpContext.Response.Cookies.Append("EIName", user.firstName, co);

                return Json("Success");
            }
        }

        /// <summary>
        /// 注销用户
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            string token = GetCookies("EIToken");

            string authUrl = "https://portal-sso.wise-paas.cn/v2.0/auth";
            string redirectUri = "https://portal-sso.wise-paas.cn/web/portals.html";
            List<Object> param = new List<object>() { new { redirectUri = redirectUri } };
            HttpHelper.HttpResponseData usersMe = HttpHelper.Delete(authUrl, param[0]);

            CookieOptions co = new CookieOptions();
            co.IsEssential = true;
            co.Domain = "wise-paas.cn";

            HttpContext.Response.Cookies.Delete("EIToken", co);

            ViewBag.Message = "退出登录成功！";
            return View("Views/User/logout.cshtml");
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
    }
}