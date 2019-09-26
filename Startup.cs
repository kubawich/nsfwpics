using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace NSFWpics
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Startup'
    public class Startup
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Startup'
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Startup.Startup(IConfiguration)'
        public Startup(IConfiguration configuration)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Startup.Startup(IConfiguration)'
        {
            Configuration = configuration;            
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Startup.Configuration'
        public IConfiguration Configuration { get; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Startup.Configuration'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Startup.ConfigureServices(IServiceCollection)'
        public void ConfigureServices(IServiceCollection services)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Startup.ConfigureServices(IServiceCollection)'
        {
            services.AddMvc();
			services.AddCors();
			services.AddMvc().AddRazorPagesOptions(o =>
			{
				o.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
			});

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API",
                    Version = "v1",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Email = "kubawich45@gmail.com",
                        Name = "Jakub Wichlinski",
                        Url = new Uri("https://kubawich.github.com/CV")
                    }                    
                });
            });
		}
        
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'Startup.Configure(IApplicationBuilder, IHostingEnvironment)'
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'Startup.Configure(IApplicationBuilder, IHostingEnvironment)'
        {
            /*app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });*/

			app.UseStaticFiles();

            app.UseRouting();

			app.UseCors(builder => builder.WithOrigins("https://nsfwpics.pw").AllowAnyHeader());

            app.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapRazorPages();
                    endpoints.MapDefaultControllerRoute();
                });

            app.UseSwagger();

            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });
        }
    }
}
