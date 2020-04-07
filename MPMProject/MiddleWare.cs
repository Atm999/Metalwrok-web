using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wise_Paas;

namespace MPMProject
{
    public class MiddleWare
    {
        private readonly RequestDelegate next;
        public MiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                if (context.Request.Path.Value != "/General/LoginHandle")
                {
                    //云端登录处理
                    if(GlobalVar.IsCloud)
                    {
                        bool re = Integration.CheckRole(context, "Metalwork");
                        //如果没有权限
                        if (!re)
                        {
                            context.Request.Path = "/General/Login";
                        }
                    }
                    //docker登录处理
                    else
                    {
                        context.Request.Cookies.TryGetValue("userName", out string value);
                        if (string.IsNullOrEmpty(value))
                        {
                            context.Request.Path = "/General/Login";
                        }
                    }
                }
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception == null)
            {
                return;
            }
            await WriteExceptionAsync(context, exception).ConfigureAwait(false);
        }

        private static async Task WriteExceptionAsync(HttpContext context, Exception exception)
        {

            //返回友好的提示
            HttpResponse response = context.Response;
            response.ContentType = context.Request.Headers["Accept"];

            response.ContentType = "application/json";
            await response.WriteAsync(JsonConvert.SerializeObject("")).ConfigureAwait(false);
        }

    }
}
