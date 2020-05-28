using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Business
{
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(AbpPermissionManagementApplicationContractsModule),
        typeof(BusinessDomainModule)
    )]
    public class BusinessApplicationContractsModule : AbpModule
    {

    }
}
