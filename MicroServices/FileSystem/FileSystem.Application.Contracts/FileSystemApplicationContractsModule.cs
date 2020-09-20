using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;

namespace FileSystem
{
    [DependsOn(
        typeof(FileSystemDomainModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpPermissionManagementApplicationContractsModule)
    )]
    public class FileSystemApplicationContractsModule : AbpModule
    {
    }
}
