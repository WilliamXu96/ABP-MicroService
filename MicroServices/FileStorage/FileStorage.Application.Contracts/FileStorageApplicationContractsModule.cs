using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace FileStorage
{
    [DependsOn(
        typeof(FileStorageDomainModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpPermissionManagementApplicationContractsModule)
    )]
    public class FileStorageApplicationContractsModule : AbpModule
    {
    }
}
