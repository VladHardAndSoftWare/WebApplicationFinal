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
using WebApplicationFinal.Areas.Identity.Data;



//using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Identity.UI.Services;

using Stripe;
//using WebApplicationFinal.Areas.Identity.Data;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
//using WebPWrecover.Services;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebApplicationFinal
{
    public class Startup
    {
        
        private IConfigurationRoot _confString;

        //конструктор для работы с DBSetting.json
        public Startup(Microsoft.AspNetCore.Hosting.IHostingEnvironment hostEnv, IConfiguration configuration) 
        {
            _confString = new ConfigurationBuilder().SetBasePath(hostEnv.ContentRootPath).AddJsonFile("DBSettings.json").Build();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //получение строки с указанием параметров базы данных из DBSetting.json
            //services.AddDbContext<AppDBContent>(options => options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<IdentityDbContext>(options => options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")));

            services.AddDbContext<AppDBContent>(options => options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")))
                    .AddDbContext <IdentityDbContext> (options => options.UseSqlServer(_confString.GetConnectionString("DefaultConnection")));

            
            services.AddIdentity<WebApplicationFinalUser, IdentityRole>().AddDefaultTokenProviders().AddDefaultUI().AddEntityFrameworkStores<AppDBContent>();

             services.AddMvc().AddRazorPagesOptions(options =>
        {
            //options.AllowAreas = true;
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
            //services.AddTransient<IEmailSender, EmailSender>();
            //services.Configure<AuthMessageSenderOptions>(Configuration);

            //добавление сервисов Idenity
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AppDBContent>();

            //передача в интерфейсы данных из БД
            services.AddTransient<IAllProduct, ProductRepository>();
            services.AddTransient<IProductCategory, CategoryRepository>();
            services.AddTransient<IAllOrder, OrdersRepository>();
            services.AddTransient<IAllUsers, UsersRepository>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //сервис позволяющий разделять корзины для разных пользователей
            services.AddScoped(sp => ShopCart.GetCart(sp));

            services.AddMvcCore().AddAuthorization(); // Note - this is on the IMvcBuilder, not the service collection

            
            //использования кэша
            services.AddMemoryCache();
            //использование сессий
            services.AddSession();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options => //CookieAuthenticationOptions
        {
            options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/Login");
        });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.Configure<StripeSettings>(_confString.GetSection("Stripe"));

            services.AddRazorPages().AddMvcOptions(options => options.EnableEndpointRouting = false);

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            StripeConfiguration.SetApiKey(_confString.GetSection("Stripe")["SecretKey"]);
            //app.UseDirectoryBrowser();//использования браузера директорий
            app.UseDeveloperExceptionPage();//использование страницы исключений
            app.UseStatusCodePages();//использования статуса страницы
            app.UseStaticFiles();
            app.UseSession();
            

            //app.UseMvcWithDefaultRoute();//маршрутизация
            

            //using (var scope = app.ApplicationServices.CreateScope())
            //{
            //    AppDBContent content = scope.ServiceProvider.GetRequiredService<AppDBContent>();
            //    //DBObjects.Initial(content);
            //}

            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            { //собственная маршрутизация
                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}"); //маршруты по умолчанию
                routes.MapRoute(name: "detailFilter", template: "Detail/action/{category?}", defaults: new { Controller = "Detail", action = "Index" });
                routes.MapRoute(name: "categoryFilter", template: "Product/action/{category?}", defaults: new { Controller = "Product", action = "Index" });
            });


            //app.UseRouting();
            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{area=Customer}/{controller=Home}/{action=Index}/{id?}");
            //    endpoints.MapRazorPages();
            //});
        }
    }
}
