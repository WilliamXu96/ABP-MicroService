using Volo.Abp.AuditLogging;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Identity;

namespace BaseService
{
    [DependsOn(
         typeof(AbpAuditLoggingDomainModule),
        typeof(AbpPermissionManagementDomainIdentityModule)
    )]
    public class BaseServiceDomainModule : AbpModule
    {
        //public override void ConfigureServices(ServiceConfigurationContext context)
        //{
        //    Configure<AbpVirtualFileSystemOptions>(options =>
        //    {
        //        options.FileSets.AddEmbedded<BaseServiceDomainModule>("BaseService");
        //    });

        //    Configure<AbpLocalizationOptions>(options =>
        //    {
        //        options.Resources
        //            .Add<BaseServiceResource>("zh-Hans")
        //            .AddBaseTypes(typeof(AbpValidationResource))
        //            .AddVirtualJson("/Localization/BaseService");
        //    });
        //}
    }
}
