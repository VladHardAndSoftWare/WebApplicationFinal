using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApplicationFinal.Data;
using WebApplicationFinal.Data.Interfaces;
using WebApplicationFinal.Data.mocks;
using WebApplicationFinal.Data.Models;
using WebApplicationFinal.Data.Repository;


using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebApplicationFinal
{
    public class Startup
    {
        
        private IConfigurationRoot _confString;

        //конструктор для работы с DBSetting.json
        public Startup(IHostingEnvironment hostEnv) 
        {
            _confString = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("DBSettings.json").Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //получение строки с указанием параметров базы данных из DBSetting.json
            services.AddDbContext<AppDBContent>(options => options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")));

            // добавление сервисов Idenity
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //            .AddEntityFrameworkStores<AppDBContent>();

            //передача в интерфейсы данных из БД
            services.AddTransient<IAllProduct, ProductRepository>();
            services.AddTransient<IProductCategory, CategoryRepository>();
            services.AddTransient<IAllOrder, OrdersRepository>();
            services.AddTransient<IAllUsers, UsersRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //сервис позволяющий разделять корзины для разных пользователей
            services.AddScoped(sp => ShopCart.GetCart(sp));

            services.AddMvc();
            //использования кэша
            services.AddMemoryCache();
            //использование сессий
            services.AddSession();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options => //CookieAuthenticationOptions
        {
            options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        });

        //    services.AddIdentity<IdentityUser, IdentityRole>()
        ////// services.AddDefaultIdentity<IdentityUser>()
        //.AddEntityFrameworkStores<AppDBContent>()
        //.AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
        .AddRazorPagesOptions(options =>
        {
            options.AllowAreas = true;
            options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
            options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
        });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            // using Microsoft.AspNetCore.Identity.UI.Services;
            //services.AddSingleton<IEmailSender, EmailSender>();



        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //app.UseDirectoryBrowser();//использования браузера директорий
            app.UseDeveloperExceptionPage();//использование страницы исключений
            app.UseStatusCodePages();//использования статуса страницы
            app.UseStaticFiles();
            app.UseSession();


            //app.UseMvcWithDefaultRoute();//маршрутизация
            app.UseMvc(routes =>
            { //собственная маршрутизация
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}"); //маршруты по умолчанию
                routes.MapRoute(name: "detailFilter", template: "Detail/action/{category?}", defaults: new { Controller = "Detail", action = "Index" });
                routes.MapRoute(name: "categoryFilter", template: "Product/action/{category?}", defaults: new { Controller = "Product", action = "Index" });  
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppDBContent content = scope.ServiceProvider.GetRequiredService<AppDBContent>();
                //DBObjects.Initial(content);
            }   
        }
    }
}
