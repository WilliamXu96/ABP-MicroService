using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace FileSystem
{
    [DependsOn(
        typeof(AbpLocalizationModule)
    )]
    public class FileSystemDomainModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<FileSystemDomainModule>("FileSystem");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<FileSystemDomainModule>("zh-Hans")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization");
            });
        }
    }
}
