using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using SchoolGallery.DAL;
using SchoolGallery.Models;
using SchoolGallery.Utils;
namespace SchoolGallery
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<SchoolContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DB")));
            services.AddSession(option => option.IdleTimeout = TimeSpan.FromMinutes(120));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,SchoolContext schoolContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
           
            app.UseStaticFiles();
            //app.UseCookiePolicy();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Login}/{id?}");
            });

            if (schoolContext.Account.Where(x=>x.AccountID=="Admin").FirstOrDefault()==null)
            {
                AccountModel admin = new AccountModel();
                admin.AccountID = "Admin";
                admin.UserName = "Admin";
                admin.PassWord = Tools.DoubleMD5("123456");
                admin.UserType = UserType.Admin;
                schoolContext.Add(admin);
                schoolContext.SaveChanges();
            }
        }
    }
}
