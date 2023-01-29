using BaseService.Localization;
using Volo.Abp.Application;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace BaseService
{
    [DependsOn(
        typeof(AbpDddApplicationModule),
        typeof(AbpLocalizationModule),
        typeof(AbpPermissionManagementApplicationContractsModule)
    )]
    public class BaseServiceApplicationContractsModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<BaseServiceApplicationContractsModule>("BaseService");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<BaseServiceResource>("zh-Hans")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/BaseService");
            });
        }
    }
}
