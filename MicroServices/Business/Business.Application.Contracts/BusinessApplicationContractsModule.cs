using Volo.Abp.Application;
using Volo.Abp.Modularity;

namespace Business
{
    [DependsOn(
        typeof(AbpDddApplicationModule)
    )]
    public class BusinessApplicationContractsModule : AbpModule
    {

    }
}
