using EmployeeManagement.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApplication1
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(_config.GetConnectionString("EmployeeDBConnection")));//Instead of creating new instance asp.net core will give contexxt from existing pool
            // services.AddMvcCore();
            services.AddMvc(options => options.EnableEndpointRouting = false);
            //services.AddScoped<IEmployeeRepository, MockEmployeeRepository>();//One hhtp request the service will be valid, count remain 4
            //services.AddTransient<IEmployeeRepository, MockEmployeeRepository>();//A new instance is provided every time request is made, count remain 3
            //services.AddSingleton<IEmployeeRepository, MockEmployeeRepository>(); //throughout the App, used in InMemory case
            services.AddScoped<IEmployeeRepository, SqlEmployeeRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute(); // Enables Default routing to our controller methods which seems like below config
            /*app.UseMvc(configureRoutes =>
            {
                configureRoutes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");// for controller and action we use default value, {id?} is optional parameter due to ?
            });*/

            //Below is setting we do in case using attribute routing
            //app.UseMvc();

            /*app.Run(async (context) =>
                {
                    await context.Response.WriteAsync("Hosting Env : " + env.EnvironmentName); // View Environment setting
                }
            );*/
        }
    }
}