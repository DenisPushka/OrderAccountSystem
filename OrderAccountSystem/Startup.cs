using DataAccess;
using DataAccess.Models;
using DataAccess.Models.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace OrderAccountSystem
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(c => { c.EnableEndpointRouting = false; });
            services.AddSingleton<ApplicationContext, ApplicationContext>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddSingleton<IClientRepository, ClientRepository>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            services.AddMvcCore();
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            
            app.UseStaticFiles();
            app.UseRouting();
            app.UseMvc(b =>
                {
                    b.MapRoute(
                        "api",
                        "api/{controller}/{action}/{id?}"
                    );

                    b.MapRoute(
                        "default",
                        "{controller}/{action}/{id?}",
                        new {controller = "Root", action = "Index"}
                    );
                }
            );
        }
    }
}