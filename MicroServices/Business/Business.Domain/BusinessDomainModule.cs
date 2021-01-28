using Volo.Abp.AuditLogging;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;

namespace Business
{
    [DependsOn(
        typeof(AbpAuditLoggingDomainModule),
        typeof(AbpSettingManagementDomainModule)
    )]
    public class BusinessDomainModule : AbpModule
    {

    }
}
