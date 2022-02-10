using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using LuckySpin.Services;
using LuckySpin.Models;

namespace LuckySpin
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddTransient<TextTransformService>();
            //services.AddSingleton<Repository>();
           

            services.AddDbContext<LuckySpinContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("LuckySpinDb")));
            //TODO: (Mac) Adjust the DbContext options to
            //  options.UseSqlite(...)
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseDeveloperExceptionPage();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "default",
                     pattern: "{controller}/{action}/{id:long?}",
                     defaults: new { controller = "Spinner", action = "Index" }
                );
            });
            app.UseStaticFiles();
        }
    }
}
