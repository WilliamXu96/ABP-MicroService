using Volo.Abp.Localization;
using Volo.Abp.Modularity;

namespace FileStorage
{
    [DependsOn(
        typeof(AbpLocalizationModule)
    )]
    public class FileStorageDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}
