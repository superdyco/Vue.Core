using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Vue.Core.Data;
using Vue.Core.Data.Entities;
using Vue.Core.Common.Config;
using Vue.Core.Extensions;
using Vue.Core.helps;
using Vue.Core.middleware;
using Vue.Core.Model;
using Vue.Core.Service;
using Vue.Core.Service.Interface;

namespace Vue.Core
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
            //db
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")
                    , assembly => assembly.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                ));

            //redis
            services.AddDistributedRedisCache(options =>
            {
                // Redis Server 的 IP 跟 Port
                options.Configuration = Configuration["redis:Connection"];
                options.InstanceName = Configuration["redis:InstanceName"];
            });
            services.AddSession();
            //jwt
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
            ).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                            context.Response.StatusCode = 401;
                        }

                        return Task.CompletedTask;
                    },
                    OnTokenValidated = context =>
                    {
                        //get jwt claims data
                        //var claimIdentity = (ClaimsIdentity)context.Principal.Identity;

                        return Task.CompletedTask;
                    }
                };
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration["JwtSetting:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = Configuration["JwtSetting:Audience"],
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSetting:Key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Add custom authorization handlers
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiRead",
                    policy => policy.Requirements.Add(new ApiRequirement(Enums.ApiPolicy.Read)));
                options.AddPolicy("ApiWrite",
                    policy => policy.Requirements.Add(new ApiRequirement(Enums.ApiPolicy.Write)));
                options.AddPolicy("ApiDelete",
                    policy => policy.Requirements.Add(new ApiRequirement(Enums.ApiPolicy.Delete)));
                options.AddPolicy("ApiPrint",
                    policy => policy.Requirements.Add(new ApiRequirement(Enums.ApiPolicy.Print)));
            });

            services.AddScoped<IAuthorizationHandler, ApiAuthorizationHandler>();

            //automapper
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfile()); });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //swagger
            services.AddSwaggerDocumentation();

            //DI
            services.AddScoped<IUsersService<Users>, UsersService>();
            services.AddScoped<IUsersTokenService, UsersTokenService>();
            services.AddScoped<IRolesService<Roles>, RolesService>();
            services.AddScoped<INewsService<News>, NewsService>();

            #region options

            services.Configure<JwtSetting>(o => Configuration.GetSection("JwtSetting").Bind(o));

            #endregion

            //可以存取Identity資料,在各Controller內
            //public Class(IHttpContextAccessor httpContextAccessor)
            services.AddHttpContextAccessor();

            //關閉回傳Json格式時Camel case names by default (第一字母變小寫)
            services.AddControllers()
                .AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext db)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    HotModuleReplacement = true,
                    ConfigFile = Path.Combine(env.ContentRootPath,
                        @"ClientApp/node_modules/@vue/cli-service/webpack.config.js"),
                    ProjectPath = Path.Combine(env.ContentRootPath, @"ClientApp")
                });

                app.UseSwaggerDocumentation();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //middleware
            app.UseApiHitsMiddleware(app.ApplicationServices);

            app.UseDefaultFiles();
            app.UseSession();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapFallbackToController("Index", "Home");
            });

            //if database not found,will auto created and seed data
           
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                context.Database.EnsureCreated();
            }
            
            var seeder = new Seeder(app.ApplicationServices);
            seeder.Init();
        }
    }
}