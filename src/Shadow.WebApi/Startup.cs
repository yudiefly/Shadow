using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using ZZH.Metrics.ServiceCollection.Extensions;
//using Pivotal.Discovery.Client;
using Shadow.MongoDbRepository.DependencyInjection;
using Shadow.MySqlRepository.DependencyInjection;
using Shadow.PostgreSqlRepository.DependencyInjection;
using Shadow.Service.DependencyInjection;
using Shadow.SqlServerRepository.DependencyInjection;
using Shadow.Tool.ApplicationBuilder;
using Shadow.Tool.DependencyInjection;
using Shadow.Tool.Diagnostics;
using Shadow.Tool.Http;
using Shadow.Tool.Http.Filters;
using Shadow.Tool.Logger;
//using Steeltoe.Common.Http.Discovery;
using System.Diagnostics;
using ZZH.DapperExpression.Service.DependencyInjection;
using ZZH.MongoDB.StandardService.DependencyInjection;
using ZZH.AutoMapper.Service.DependencyInjection;
using NLog.Web;

namespace Shadow.WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // add metrics
            services.AddZzhMetrics(Configuration);

            services.AddApiVersioning(o => {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
            });
            services.AddMvc().AddMvcOptions(option => 
            {
                option.Filters.Add<LogAttribute>();
            })
            .AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblyContaining<Startup>();  // 当前程序集 https://fluentvalidation.net/aspnet#asp-net-core
            })
            .AddZzhMertrics();

            services.AddHttpContextAccessor();

            #region Eureka的服务发现和注册(不再使用了）
            //// register service discovery
            //services.AddDiscoveryClient(Configuration)
            //        .AddTransient<DiscoveryHttpMessageHandler>();

            //// eg:
            //services.AddHttpClient();
            //services.AddHttpClient("discovery-sample")
            //        .AddHttpMessageHandler<DiscoveryHttpMessageHandler>()
            //        .AddTypedClient<Controllers.DiscoverySampleController>();

            // add redis
            //services.AddRedisCache(Configuration);
            #endregion

            services.AddRedisSentinelCache(Configuration);  // 哨兵模式

            // only for test
            // 实际开发中只能使用 SqlServer 或 MySQL、PostgreSQL其中一个
            if (string.Equals(Configuration["dbmodel"], "MySql", System.StringComparison.OrdinalIgnoreCase))
            {
                // add dapper MySql
                services.AddDapperMySql<MySqlRepository.Context.ShadowActiveDbContext>("shadow_mysql");
                services.AddMySqlRepository();
            }
            else
            {
                if(string.Equals(Configuration["dbmodel"], "PostgreSQL", System.StringComparison.OrdinalIgnoreCase))
                {
                    //add dapper PostgreSQL
                    services.AddDapperPostgreSql<PostgreSqlRepository.Context.ShadowActiveDbContext>("shadow_postgresql");
                    services.AddPostgreSqlRepository();
                }
                else
                {
                    // add dapper SqlServer
                    services.AddDapperSqlServer<SqlServerRepository.Context.ShadowActiveDbContext>("shadow");
                    services.AddSqlServerRepository();
                }               
            }

            // add MongoDB
            services.AddMongoDbRepository();
            services.AddMongoDB();

            // add automapper
            services.AddAutoMapper();
            services.AddApplicationService();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, DiagnosticListener diagnosticListener)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(ExceptionHandler.Default);  // handle exception
            }

            diagnosticListener.AddToolkitDiagnositcs();  // 添加诊断

            env.ConfigureNLog("nlog.config");
            loggerFactory.AddDefaultNLog(options =>
            {
                options.HasRequestHeaders = true;
                options.HasResponseHeaders = true;
            });

            // 保证在 Mvc 之前调用
            app.UseHttpContextGlobal()
               .UseToolTrace();

            app.UseMvc();
            
            // add service discovery
            //app.UseDiscoveryClient();
        }
    }
}
