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
        public IActionResult LoginHandle(string userName, string passWord)
        {
            if (Integration.Login(HttpContext, userName, passWord, "Metalwork"))
            {
                return Json("Success");
            }
            else
            {
                return Json("Failed");
            }
        }

        /// <summary>
        /// 注销用户
        /// </summary>
        /// <returns></returns>
        public IActionResult Logout()
        {
            Integration.Logout(HttpContext);
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