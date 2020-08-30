using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace BaseService
{
    [DependsOn(
        typeof(BaseServiceApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule)
    )]
    public class BaseServiceHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(BaseServiceHttpApiModule).Assembly);
            });
        }
    }
}
