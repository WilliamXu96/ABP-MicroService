using Business.Localization;
using Business.MultiTenancy;
using Volo.Abp.AuditLogging;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.SettingManagement;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace Business
{
    [DependsOn(
        typeof(AbpLocalizationModule),
        typeof(AbpAuditLoggingDomainModule),
        typeof(AbpSettingManagementDomainModule)
    )]
    public class BusinessDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<BusinessDomainModule>("Business");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<BusinessResource>("zh-Hans")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/Business");
            });
        }
    }
}
