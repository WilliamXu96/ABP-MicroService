using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace FileSystem
{
    [DependsOn(
        typeof(FileSystemApplicationContractsModule),
        typeof(AbpAutoMapperModule)
    )]
    public class FileSystemApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<FileSystemApplicationModule>();
            });
        }
    }
}
