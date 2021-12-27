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
using DataAccess.EntityFramework.Contexts;
using DataAccess.Repositories.Bases;
using DataAccess.Repositories.Concrete;
using DataAccess.Repositories.Concretes;
using Microsoft.EntityFrameworkCore;

namespace WebApp
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
            ConnectionConfig.ConnectionString = Configuration.GetConnectionString("CarRentalContext");
            services.AddScoped<DbContext, CarRentalContext>();
            services.AddScoped<BrandRepositoryBase, BrandRepository>();
            services.AddScoped<CarRepositoryBase, CarRepository>();
            services.AddScoped<ColorRepositoryBase, ColorRepository>();
            services.AddScoped<CustomerRepositoryBase, CustomerRepository>();
            services.AddScoped<RentalRepositoryBase, RentalRepository>();
            services.AddScoped<ICarService, CarService>();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
