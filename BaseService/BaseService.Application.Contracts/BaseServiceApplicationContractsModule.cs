using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace BaseService
{
    [DependsOn(
        typeof(BaseServiceDomainModule),
        typeof(AbpDddApplicationModule)
    )]
    public class BaseServiceApplicationContractsModule : AbpModule
    {
    }
}
