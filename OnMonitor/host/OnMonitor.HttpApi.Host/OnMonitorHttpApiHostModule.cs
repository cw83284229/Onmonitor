using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IdentityModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OnMonitor.EntityFrameworkCore;
using OnMonitor.MultiTenancy;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.Security.Claims;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.Swashbuckle;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.AspNetCore.Mvc;

namespace OnMonitor
{
    [DependsOn(
        typeof(OnMonitorApplicationModule),
        typeof(OnMonitorEntityFrameworkCoreModule),
        typeof(OnMonitorHttpApiModule),
        typeof(AbpAspNetCoreMvcUiMultiTenancyModule),
        typeof(AbpAutofacModule),
        typeof(AbpCachingStackExchangeRedisModule),
        typeof(AbpEntityFrameworkCoreSqlServerModule),
        typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpSettingManagementEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreSerilogModule),
        typeof(AbpSwashbuckleModule),
        typeof(AbpAspNetCoreSignalRModule)
       
        )]
    public class OnMonitorHttpApiHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();
            var configuration = context.Services.GetConfiguration();

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });

            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = MultiTenancyConsts.IsEnabled;
            });

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<OnMonitorDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}OnMonitor.Domain.Shared", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<OnMonitorDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}OnMonitor.Domain", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<OnMonitorApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}OnMonitor.Application.Contracts", Path.DirectorySeparatorChar)));
                    options.FileSets.ReplaceEmbeddedByPhysical<OnMonitorApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}OnMonitor.Application", Path.DirectorySeparatorChar)));
                });
            }

            context.Services.AddAbpSwaggerGenWithOAuth(
                configuration["AuthServer:Authority"],
                new Dictionary<string, string>
                {
                    {"OnMonitor", "OnMonitor API"}
                },
                options =>
                {
                    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "OnMonitor.Application.xml"));
                    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "OnMonitor.Application.Contracts.xml"));
                    //options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "OnMonitor.HttpApi.xml"));
                    options.SwaggerDoc("v1", new OpenApiInfo {Title = "OnMonitor API", Version = "v1"});
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                });
             //配置动态api
            Configure<AbpAspNetCoreMvcOptions>(options =>

            {
                options.ConventionalControllers
                  .Create(typeof(OnMonitorApplicationModule).Assembly);

            });
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            });

            context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = Convert.ToBoolean(configuration["AuthServer:RequireHttpsMetadata"]);
                    options.Audience = "OnMonitor";
                });

            Configure<AbpDistributedCacheOptions>(options =>
            {
                options.KeyPrefix = "OnMonitor:";
            });

            if (!hostingEnvironment.IsDevelopment())
            {
                var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
                context.Services
                    .AddDataProtection()
                    .PersistKeysToStackExchangeRedis(redis, "OnMonitor-Protection-Keys");
            }

            //context.Services.AddCors(options =>
            //{
            //    options.AddPolicy(DefaultCorsPolicyName, builder =>
            //    {
            //        builder
            //            .WithOrigins(
            //                configuration["App:CorsOrigins"]
            //                    .Split(",", StringSplitOptions.RemoveEmptyEntries)
            //                    .Select(o => o.RemovePostFix("/"))
            //                    .ToArray()
            //            )
            //            .WithAbpExposedHeaders()
            //            .SetIsOriginAllowedToAllowWildcardSubdomains()
            //            .AllowAnyHeader()
            //            .AllowAnyMethod()
            //            .AllowCredentials();
            //    });
            //});
            context.Services.AddSameSiteCookiePolicy();
            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .SetIsOriginAllowed(origin => true)
                        // .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()

                        .WithMethods("GET", "POST", "HEAD", "PUT", "DELETE", "OPTIONS");

                });


            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseErrorPage();
                app.UseHsts();
            }
            app.UseCookiePolicy();
            app.UseHttpsRedirection();
            app.UseCorrelationId();
            app.UseVirtualFiles();
            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);
            app.UseAuthentication();
            if (MultiTenancyConsts.IsEnabled)
            {
                app.UseMultiTenancy();
            }
            app.UseAbpRequestLocalization();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseAbpSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Support APP API");

                var configuration = context.GetConfiguration();
                options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
                options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();
        }
    }
}
