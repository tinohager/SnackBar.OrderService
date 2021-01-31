using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SnackBar.OrderService.WebApi.Hubs;

namespace SnackBar.OrderService.WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddCors(options =>
            //    options.AddDefaultPolicy(builder =>
            //        builder.WithOrigins("https://localhost:5001")
            //        .AllowAnyHeader()
            //        .AllowAnyMethod()
            //        .SetIsOriginAllowed((isOriginAllowed) => true)
            //        .AllowCredentials()));

            //services.AddCors(options =>
            //    options.AddDefaultPolicy(builder => 
            //        builder.AllowAnyHeader()
            //        .AllowAnyMethod()
            //        .AllowCredentials()
            //        .SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost"))
            //);

            services.AddCors(options => options.AddPolicy("AllowAllPolicy",
                builder =>
                {
                    builder.AllowAnyHeader()
                           .AllowAnyMethod()
                           .SetIsOriginAllowed((host) => true)
                           .AllowCredentials();
                }));

            services.AddControllers();

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors("AllowAllPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<SnackBarHub>("/snackbar");
            });
        }
    }
}
