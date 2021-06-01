using BaseService.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Identity;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace BaseService
{
    [DependsOn(
        typeof(AbpPermissionManagementDomainIdentityModule)
    )]
    public class BaseServiceDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<BaseServiceDomainModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<BaseServiceResource>("zh-Hans")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/BaseService");

                options.DefaultResourceType = typeof(BaseServiceResource);
            });
        }
    }
}
