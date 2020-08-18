using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.AspNetCore.MultiTenancy;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using System;
using Volo.Abp.TenantManagement;
using Volo.Abp.Threading;
using Volo.Abp.Data;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.PermissionManagement.HttpApi;
using Volo.Abp.PermissionManagement;
using Volo.Abp.PermissionManagement.Identity;
using Business;
using Microsoft.AspNetCore.Cors;
using Volo.Abp.MultiTenancy;
using BaseService.EntityFrameworkCore;

namespace BaseService
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(BaseServiceApplicationModule),
        typeof(BaseServiceEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreMultiTenancyModule),
        //typeof(AbpEntityFrameworkCoreSqlServerModule),
        //typeof(AbpAuditLoggingEntityFrameworkCoreModule),
        //typeof(AbpPermissionManagementEntityFrameworkCoreModule),
        typeof(AbpPermissionManagementHttpApiModule),
        //typeof(AbpPermissionManagementDomainIdentityModule),
        //typeof(AbpPermissionManagementApplicationModule),
        //typeof(AbpSettingManagementEntityFrameworkCoreModule),
        //typeof(AbpTenantManagementEntityFrameworkCoreModule),
        typeof(AbpTenantManagementHttpApiModule),
        //typeof(AbpTenantManagementApplicationModule),
        typeof(AbpIdentityHttpApiModule),
        typeof(BusinessHttpApiModule),
        //typeof(AbpIdentityEntityFrameworkCoreModule),
        //typeof(AbpIdentityApplicationModule),
        typeof(AbpAspNetCoreSerilogModule)
    )]
    public class BaseServiceHostModule : AbpModule
    {
        private const string DefaultCorsPolicyName = "Default";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();

            Configure<AbpMultiTenancyOptions>(options =>
            {
                options.IsEnabled = true;
            });

            context.Services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.ApiName = configuration["AuthServer:ApiName"];
                    options.RequireHttpsMetadata = false;
                });

            context.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity Service API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });

            Configure<AbpDbContextOptions>(options =>
            {
                options.UseSqlServer();
            });

            context.Services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration["Redis:Configuration"];
            });

            Configure<AbpAuditingOptions>(options =>
            {
                options.IsEnabledForGetRequests = true;
                options.ApplicationName = "IdentityService";
            });

            context.Services.AddCors(options =>
            {
                options.AddPolicy(DefaultCorsPolicyName, builder =>
                {
                    builder
                        .WithOrigins(
                            configuration["App:CorsOrigins"]
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            var redis = ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
            context.Services.AddDataProtection()
                .PersistKeysToStackExchangeRedis(redis, "DataProtection-Keys");

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
                options.Languages.Add(new LanguageInfo("en", "en", "English"));
                options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
                options.Languages.Add(new LanguageInfo("ru", "ru", "Русский"));
                options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
                options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
                options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            });
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseCorrelationId();
            app.UseVirtualFiles();
            app.UseRouting();
            app.UseCors(DefaultCorsPolicyName);
            app.UseAuthentication();
            app.UseAbpClaimsMap();
            app.UseMultiTenancy();

            app.UseAbpRequestLocalization(); 
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity Service API");
            });
            app.UseAuditing();
            app.UseAbpSerilogEnrichers();
            app.UseConfiguredEndpoints();

            AsyncHelper.RunSync(async () =>
            {
                using (var scope = context.ServiceProvider.CreateScope())
                {
                    await scope.ServiceProvider
                        .GetRequiredService<IDataSeeder>()
                        .SeedAsync();
                }
            });
        }
    }
}
