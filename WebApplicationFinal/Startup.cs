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
            
            //передача в интерфейсы данных из БД
            services.AddTransient<IAllCars, CarRepository>();
            services.AddTransient<ICarsCategory, CategoryRepository>();
            services.AddTransient<IAllOrder, OrdersRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //сервис позволяющий разделять корзины для разных пользователей
            services.AddScoped(sp => ShopCart.GetCart(sp));

            services.AddMvc();
            //использования кэша
            services.AddMemoryCache();
            //использование сессий
            services.AddSession();
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
                routes.MapRoute(name: "categoryFilter", template: "Car/action/{category?}", defaults: new { Controller = "Car", action = "Index" });  
            });

            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppDBContent content = scope.ServiceProvider.GetRequiredService<AppDBContent>();
                DBObjects.Initial(content);
            }   
        }
    }
}
