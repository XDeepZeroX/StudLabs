using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StudLab.Data;
using StudLab.Filters;
using StudLab.Model;
using StudLab.Model.Abstract;
using StudLab.Models.Abstract;
using StudLab.Models.Repositories;
using StudLab.Models.TablesEntities;

namespace StudLab
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
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();


            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; // cookie middle setup above
                //options.SignInScheme = "Cookies"; // CookieAuthenticationDefaults.AuthenticationScheme; // cookie middle setup above
                options.Authority = "http://localhost:5000"; // Auth Server

                options.RequireHttpsMetadata = false; // only for development 

                options.ClientId = "StudLab"; // client setup in Auth Server
                options.ClientSecret = "secret";
                options.ResponseType = "code id_token"; // means Hybrid flow (id + access token)
                options.Scope.Add("offline_access");


                options.GetClaimsFromUserInfoEndpoint = true; //-
                options.SaveTokens = true;

                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    NameClaimType = "name",
                    RoleClaimType = "role"
                };

            });

            string connectionString = Configuration.GetConnectionString("DefaultConnection");

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(connectionString));


            services.AddMvc(opt=>
            {
                opt.Filters.Add(new CustomActionFilter());
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            //----------AutoMapper----------------
            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddTransient(typeof(AbstractTableRepository<MultiCriteriaTask>), typeof(MultiCriteriaRepository));
            services.AddTransient(typeof(AbstractTableRepository<TableTransportTask>), typeof(TransportRepository));
            services.AddTransient(typeof(AbstractTableRepository<MatrixOperationTask>), typeof(MatrixOperationsRepository));
            services.AddTransient(typeof(AbstractRepository<User>), typeof(UserRepository));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");


            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            
            app.UseMvcWithDefaultRoute();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
