using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace FileStorage
{
    [DependsOn(
        typeof(FileStorageApplicationContractsModule),
        typeof(AbpAutoMapperModule)
    )]
    public class FileStorageApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<FileStorageApplicationModule>();
            });
        }
    }
}
