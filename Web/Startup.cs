using IServices;
using IServices.Infrastructure;
using IServices.ISysServices;
using IServices.ITaskServices;
using IServices.IUserServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models;
using Services;
using Services.Repository;
using Services.SysServices;
using Services.TaskServices;
using Services.UserServices;
using System;
using Web.Extensions;
using Web.SignalRHubs;
using Web.LocalizationResources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Hosting;
using EFCoreSecondLevelCacheInterceptor;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.OpenApi.Models;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Newtonsoft.Json.Serialization;
using Web.Areas.Api.Models;
using EasyCaching.Core;

namespace Web
{
    /// <summary>
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// </summary>

        private IConfiguration Configuration { get; }

        /// <summary>
        /// </summary>
        /// <param name="services"></param>
        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {
            //efcore cache
            services.AddEFSecondLevelCache(options =>
            {
                options.UseEasyCachingCoreProvider(EasyCachingConstValue.DefaultHybridName, true);//内存和redis混合
                //options.UseEasyCachingCoreProvider(EasyCachingConstValue.DefaultInMemoryName, false);//内存

                options.CacheAllQueries(CacheExpirationMode.Sliding, TimeSpan.FromDays(1)); //Sliding 滑动  Absolute 绝对
            });


            // More info: https://easycaching.readthedocs.io/en/latest/Redis/
            services.AddEasyCaching(options =>
            {
                // use memory cache with your own configuration
                options.UseInMemory();

                options.UseRedis(config =>
                {
                    config.DBConfig.AllowAdmin = true;
                    config.DBConfig.Endpoints.Add(new EasyCaching.Core.Configurations.ServerEndPoint(Configuration["Redis:Host"], int.Parse(Configuration["Redis:Port"])));
                    config.DBConfig.Password = Configuration["Redis:Password"];
                    config.DBConfig.IsSsl = bool.Parse(Configuration["Redis:IsSsl"]);
                });

                //  使用hybird
                options.UseHybrid(config =>
                {
                    config.LocalCacheProviderName = EasyCachingConstValue.DefaultInMemoryName;
                    config.DistributedCacheProviderName = EasyCachingConstValue.DefaultRedisName;
                    config.TopicName = "test_topic";
                });

                // 使用redis作为缓存总线
                options.WithRedisBus(config =>
                {
                    config.AllowAdmin = true;
                    config.Endpoints.Add(new EasyCaching.Core.Configurations.ServerEndPoint(Configuration["Redis:Host"], int.Parse(Configuration["Redis:Port"])));
                    config.Password = Configuration["Redis:Password"];
                    config.IsSsl = bool.Parse(Configuration["Redis:IsSsl"]);
                    config.Database = 6;
                });
            });

            //sql server

            services.AddDbContextPool<ApplicationDbContext>((serviceProvider, optionsBuilder) => optionsBuilder.UseLazyLoadingProxies().UseSqlServer(Configuration["ConnectionStrings:DbServerConnection"], c => c.EnableRetryOnFailure().MigrationsAssembly("Web")).AddInterceptors(serviceProvider.GetRequiredService<SecondLevelCacheInterceptor>()).EnableSensitiveDataLogging());

            //dapper
            services.AddScoped<IDbConnection>(e => new SqlConnection(Configuration["ConnectionStrings:DbServerConnection"]));

            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            // Configure Identity
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
            });

            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            services.AddScoped<DbContext, ApplicationDbContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Add application services.
            services.AddTransient<IKeyStoreService, InMemoryKeyStoreService>();

            services.AddTransient<IEmailSender, EmailSender>();

            //services.AddTransient<ISmsSender, SmsSender>();

            services.AddTransient<IUserInfo, UserInfo>();

            services.AddTransient<IUserAuthorize, UserAuthorize>();

            services.AddTransient<IDapperRepository, DapperRepository>();

            services.AddTransient<ISysLogService, SysLogService>();

            services.AddTransient<ISysAreaService, SysAreaService>();

            services.AddTransient<ISysControllerService, SysControllerService>();

            services.AddTransient<ISysActionService, SysActionService>();

            services.AddTransient<ISysHelpService, SysHelpService>();

            services.AddTransient<ISysHelpClassService, SysHelpClassService>();

            services.AddTransient<ISysRoleService, SysRoleService>();

            services.AddTransient<ISysUserRoleService, SysUserRoleService>();

            services.AddTransient<ISysUserLogService, SysUserLogService>();

            services.AddTransient<ISysRoleSysControllerSysActionService, SysRoleSysControllerSysActionService>();

            services.AddTransient<ISysControllerSysActionService, SysControllerSysActionService>();

            services.AddTransient<ISysDepartmentService, SysDepartmentService>();

            services.AddTransient<ITaskCenterService, TaskCenterService>();

            services.AddTransient<ISysMessageCenterService, SysMessageService>();

            services.AddTransient<ISysMessageReceivedService, SysMessageReceivedService>();

            services.AddTransient<IMessengerHub, MessengerHub>();

            services.AddTransient<IHolidayService, HolidayService>();

            services.AddTransient<IJsonDataService, JsonDataService>();

            services.AddTransient<IJsonDataHistoryService, JsonDataHistoryService>();

            services.AddTransient<ICityCodeService, CityCodeService>();

            services.AddTransient<IDomainLabelService, DomainLabelService>();

            services.AddTransient<ISysUserService, SysUserService>();

            services.AddTransient<ISysDepartmentSysUserService, SysDepartmentSysUserService>();

            services.AddTransient<IResInfo, ResInfo>();

            services.AddSingleton<IHostedService, Worker>();//计划任务

            services.AddCors(options =>
                        options.AddPolicy("全部允许", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
              );

            // 防止跨站请求
            services.AddAntiforgery();

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddAuthentication(

                 options =>
                 {
                 }

                )
               .AddCookie()
                .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = "evan",
                    ValidateAudience = true,
                    ValidAudience = "evan",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("AAAAAA0000000000000")),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
            services.AddMvc(a =>
                {
                    // 异常处理
                    a.Filters.Add(typeof(ExceptionFilter));

                    // 身份验证
                    a.Filters.Add(new UserAuthorizeFilter(new AuthorizationPolicyBuilder()
                        .RequireAuthenticatedUser()
                        .Build()));

                    // 记录action 日志
                    a.Filters.Add(typeof(UserLogFilter));

                    // a.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); //webapi 暂不知怎么处理 也会拦截
                })

            .AddDataAnnotationsLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(ExpressLocalizationResource));
            })
            .AddMvcLocalization(options =>
            {
                options.DataAnnotationLocalizerProvider = (type, factory) => factory.Create(typeof(ExpressLocalizationResource));
            })

            .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.Configure<EmailConfig>(Configuration.GetSection("EmailConfig"));

            services.Configure<AdministratorContact>(Configuration.GetSection("AdministratorContact"));

            services.AddSignalR();

            services.AddHealthChecks();

            services.AddControllersWithViews().AddDapr().AddRazorRuntimeCompilation().AddNewtonsoftJson(options =>
          {
              options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
              options.SerializerSettings.ContractResolver = new DefaultContractResolver();
          });

            services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API",
                    Version = "v1",
                    Contact = new OpenApiContact
                    {
                        Name = "樊书译",
                        Email = "fsy_008@163.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = Configuration["Copyright"]
                    }
                });

                //c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme() { In = ParameterLocation.Header, Type = SecuritySchemeType.ApiKey, Name = "X-API-KEY" });

                //c.AddSecurityRequirement(new OpenApiSecurityRequirement
                //{
                //    {
                //          new OpenApiSecurityScheme
                //            {
                //                Reference = new OpenApiReference
                //                {
                //                    Type = ReferenceType.SecurityScheme,
                //                    Id = "ApiKey"
                //                }
                //            },
                //            Array.Empty<string>()
                //    }
                //});

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Login 用户登录接口获取 Token，例如：“eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9*********”",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement(){ { new OpenApiSecurityScheme{
                                                                            Reference = new OpenApiReference
                                                                            {
                                                                                Type = ReferenceType.SecurityScheme,
                                                                                Id = "Bearer"
                                                                            },
                                                                        },
                                                                        new List<string>()
                                                                    }
                                                                });

                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "Web.xml"));
            });

            services.AddGrpc();

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddDatabaseDeveloperPageExceptionFilter();
        }

        /// <summary>
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();//开发者异常页面
            }
            else
            {
                // app.UseDeveloperExceptionPage();//开发者异常页面
            }
            app.UseCors(config => { config.AllowAnyOrigin(); config.AllowAnyHeader(); config.AllowAnyMethod(); });

            app.UseMigrationsEndPoint();

            app.UseExceptionHandler("/Home/Error");

            app.UseHsts();

            //app.UseStatusCodePages();//

            app.UseHttpsRedirection(); //跳转到https

            app.UseStaticFiles();//使用静态文件

            app.UseRouting();

            app.UseCors("全部允许");

            app.UseCloudEvents();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseRequestLocalization(a =>
            {
                a.SetDefaultCulture("zh-CN");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapSubscribeHandler();

                endpoints.MapControllerRoute(
                  name: "default-l",
                  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                 name: "default-2",
                 pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapHub<MessengerHub>("/messengerhub");
            });

            app.UseCookiePolicy();


            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V 1.0");
            });

            app.UseHealthChecks("/healthz");
        }
    }
}