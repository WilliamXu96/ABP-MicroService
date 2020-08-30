using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace BaseService
{
    [DependsOn(
        typeof(BaseServiceDomainModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpPermissionManagementApplicationContractsModule)
    )]
    public class BaseServiceApplicationContractsModule : AbpModule
    {
    }
}
