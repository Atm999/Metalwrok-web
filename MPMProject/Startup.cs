﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Model;
using MPMProject.Controllers;

namespace MPMProject
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            try
            {
                BaseController.url= Configuration.GetValue<string>("APIUrl");
            }
            catch (Exception ex)
            {
                Console.WriteLine("ex.message=" + ex.Message);
            }
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
           
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => false;//使用cookie
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin()//允许所有站点跨域请求
                   .AllowAnyMethod()//允许所有请求方法
                   .AllowAnyHeader()//允许所有请求头
                   .AllowCredentials();//允许Cookie信息
                });
            });

            //将session保存1个小时
            services.AddSession(options => {
                options.IdleTimeout = TimeSpan.FromHours(1);
            });

            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //注入Session工厂类接口
            //注册Cookie认证服务
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            #region CORS
            services.AddCors(options =>

            {

                options.AddPolicy("AllowSpecificOrigin",

                 builder => builder.WithOrigins("http://localhost:3997").AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

            });
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("AllowSpecificOrigin");

            app.UseStaticFiles(new StaticFileOptions()

            {

                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/Files")),
                RequestPath = new PathString("/src")

            });

            //必须加上这句才能用session
            app.UseSession();
            ServiceLocator.Instance = app.ApplicationServices;

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=LevelsConfig}/{action=Index}/{id?}");
            });
        }
    }
}
