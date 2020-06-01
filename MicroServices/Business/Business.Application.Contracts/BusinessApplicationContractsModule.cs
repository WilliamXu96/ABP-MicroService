using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace Business
{
    [DependsOn(
        typeof(BusinessDomainModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpPermissionManagementApplicationContractsModule)
    )]
    public class BusinessApplicationContractsModule : AbpModule
    {

    }
}
