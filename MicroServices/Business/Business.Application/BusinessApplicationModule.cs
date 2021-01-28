using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs.Hangfire;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Identity;
using Volo.Abp.Json;
using Volo.Abp.Modularity;
using XCZ;

namespace Business
{
    [DependsOn(
        typeof(BusinessDomainModule),
        typeof(BusinessApplicationContractsModule),
        typeof(AbpHttpClientIdentityModelModule),
        typeof(AbpIdentityHttpApiClientModule),
        typeof(FormApplicationModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpBackgroundJobsHangfireModule)
    )]
    public class BusinessApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<BusinessApplicationAutoMapperProfile>();
            });
        }

        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<AbpJsonOptions>(option =>
            {
                option.UseHybridSerializer = false;
            });
        }
    }
}
