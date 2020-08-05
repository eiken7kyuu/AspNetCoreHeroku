using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using HerokuDeploySample.Data;
using System.IO;

namespace HerokuDeploySample
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            if (Env.IsDevelopment())
            {
                services.AddDbContext<SchoolContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("SchoolContext")));
            }
            else
            {
                var uri = new Uri(Configuration["DATABASE_URL"]);
                var userInfo = uri.UserInfo.Split(":");
                (var user, var password) = (userInfo[0], userInfo[1]);
                var db = Path.GetFileName(uri.AbsolutePath);

                var connStr = $"Host={uri.Host};Port={uri.Port};Database={db};Username={user};Password={password};Enlist=true";
                services.AddDbContext<SchoolContext>(options => options.UseNpgsql(connStr));
            }
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

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
