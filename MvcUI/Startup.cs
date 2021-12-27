using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Services;
using Business.Services.Bases;
using Core.DataAccess.Configs;
using Core.Utils;
using Core.Utils.Bases;
using DataAccess.EntityFramework.Contexts;
using DataAccess.Repositories.Bases;
using DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using MvcUI.Settings;

namespace MvcUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(config =>
            {
                config.LoginPath = "/Accounts/Login";
                config.AccessDeniedPath = "/Accounts/AccessDenied";
                config.ExpireTimeSpan=TimeSpan.FromMinutes(30);
                config.SlidingExpiration = true;
            });

            services.AddSession();
           

            ConnectionConfig.ConnectionString = Configuration.GetConnectionString("CarRentalContext");
            services.AddScoped<DbContext, CarRentalContext>();
            services.AddScoped<BrandRepositoryBase, BrandRepository>();
            services.AddScoped<CarRepositoryBase, CarRepository>();
            services.AddScoped<ColorRepositoryBase, ColorRepository>();
            services.AddScoped<RentalRepositoryBase, RentalRepository>();
            services.AddScoped<UserRepositoryBase, UserRepository>();
            services.AddScoped<RoleRepositoryBase, RoleRepository>();



            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IRentalService, RentalService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();

            AppSettingsUtilBase appSettingsUtil = new AppSettingsUtil(Configuration);
            appSettingsUtil.BindAppSettings<AppSettings>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();
            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
