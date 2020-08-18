using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement.Identity;

namespace BaseService
{
    [DependsOn(
        typeof(AbpPermissionManagementDomainIdentityModule)
    )]
    public class BaseServiceDomainModule : AbpModule
    {
    }
}
