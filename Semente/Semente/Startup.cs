using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Semente.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;

namespace Semente
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
            /*services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader());
            });*/

            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddMvc();
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowSpecificOrigin"));
            });

            services.AddDbContext<SementeContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("SementeContext")));

            services.AddIdentity<UserEntity, IdentityRole>()
                .AddEntityFrameworkStores<SementeContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(authenticationOptions =>
            {
                authenticationOptions.DefaultScheme = "Cookies";
                authenticationOptions.DefaultChallengeScheme = "Cookies";
            })

            .AddCookie(config =>
                config.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToLogin = ctx =>
                    {
                        if (ctx.Request.Path.StartsWithSegments("/api")
                            && ctx.Response.StatusCode == 200)
                        {
                            ctx.Response.StatusCode = 401;
                            return Task.FromResult<object>(null);
                        }
                        ctx.Response.Redirect(ctx.RedirectUri);
                        return Task.FromResult<object>(null);
                    }
                }
            )
            .AddJwtBearer(config =>
                {
                    config.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                                            Configuration.GetSection("AppConfiguration:Key").Value)),
                        ValidAudience = Configuration.GetSection("AppConfiguration:SiteUrl").Value,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ValidIssuer = Configuration.GetSection("AppConfiguration:SiteUrl").Value
                    };
                    config.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = ctx =>
                        {
                            ctx.Response.StatusCode = 401;
                            return Task.FromResult<object>(null);
                        }
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            /*app.UseCors(builder =>
       builder.WithOrigins("http://example.com"));
            app.UseCors(builder =>
       builder.WithOrigins("http://localhost:4200").AllowAnyHeader());*/
            app.UseCors("AllowSpecificOrigin");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
