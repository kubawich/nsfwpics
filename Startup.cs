using System;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace NSFWpics
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;            
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
			services.AddCors();
			services.AddMvc().AddRazorPagesOptions(o =>
			{
				o.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
			});
		}
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
				app.UseStaticFiles();
                app.UseDeveloperExceptionPage();
			}
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            var cookiePolicyOptions = new CookiePolicyOptions();

            app.UseAuthentication();

			app.UseCors(builder => builder.WithOrigins("https://nsfwpics.pw").AllowAnyHeader());

			//app.UseStaticFiles();

            app.UseMvc();                     
        }
    }
}
