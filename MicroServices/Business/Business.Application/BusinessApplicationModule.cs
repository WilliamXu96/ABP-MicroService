using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace Business
{
    [DependsOn(
        typeof(BusinessApplicationContractsModule),
        typeof(AbpAutoMapperModule)
    )]
    public class BusinessApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<BusinessApplicationModule>();
            });
        }
    }
}
