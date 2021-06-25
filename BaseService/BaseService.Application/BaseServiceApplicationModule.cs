using Volo.Abp.AutoMapper;
using Volo.Abp.Identity;
using Volo.Abp.Json;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.TenantManagement;

namespace BaseService
{
    [DependsOn(
        typeof(BaseServiceApplicationContractsModule),
        typeof(AbpPermissionManagementApplicationModule),
        typeof(AbpTenantManagementApplicationModule),
        typeof(AbpIdentityApplicationModule),
        typeof(AbpAutoMapperModule)
    )]
    public class BaseServiceApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<BaseServiceApplicationAutoMapperProfile>();
            });
        }
    }
}
