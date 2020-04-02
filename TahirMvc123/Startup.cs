using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using TahirMvc123.Models;

namespace TahirMvc123
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
            // MSSQL server
            services.AddDbContext<MvcDBContext>(options =>
               options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            //// #Injection
            //services.AddTransient<IBookService, BookService>();
            //var sigInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var sigInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my-name-is-khan-and-i-am-not-"));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = "labs-i",
                ValidAudience = "labs-a",
                IssuerSigningKey = sigInKey,
                RequireExpirationTime = false,
                RequireSignedTokens = true,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };


            ////services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //// .AddCookie(options =>
            //// {
            ////     options.LoginPath = "/user/login";
            ////     options.AccessDeniedPath = "/user/accessdenied";
            //// });

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
          .AddJwtBearer(opt =>
          {
              opt.RequireHttpsMetadata = false;
              opt.SaveToken = true;
              opt.TokenValidationParameters = tokenValidationParameters;
          });

            services.AddAuthorization(auth =>
            {
                // Users 
                auth.AddPolicy("grade1", policy => policy.RequireClaim("permission", "grade-1"));
                auth.AddPolicy("create", policy => policy.RequireClaim("permission", "create"));
                auth.AddPolicy("update", policy => policy.RequireClaim("permission", "update"));
                auth.AddPolicy("delete", policy => policy.RequireClaim("permission", "delete"));
            });

            services.AddControllersWithViews();
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
            }

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=User}/{action=Login}/{id?}");
            });
        }
    }
}
