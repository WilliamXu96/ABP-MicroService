using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Volo.Abp;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Identity;
using Volo.Abp.Modularity;

namespace InternalGateway
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(AbpIdentityHttpApiModule),
        typeof(AbpAspNetCoreSerilogModule)
    )]
    public class InternalGatewayHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            ConfigureSwaggerServices(context);
            context.Services.AddOcelot(context.Services.GetConfiguration());
        }

        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();

            app.UseCorrelationId();
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "InternalGateway Service API");
            });
            app.MapWhen(
                ctx => ctx.Request.Path.ToString().StartsWith("/api/abp/") ||
                       ctx.Request.Path.ToString().StartsWith("/Abp/"),
                app2 =>
                {
                    app2.UseRouting();
                    app2.UseConfiguredEndpoints();
                }
            );

            app.UseOcelot().Wait();
            app.UseAbpSerilogEnrichers();
        }

        private static void ConfigureSwaggerServices(ServiceConfigurationContext context)
        {
            context.Services.AddSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "InternalGateway Service API", Version = "v1" });
                    options.DocInclusionPredicate((docName, description) => true);
                });
        }
    }
}
