using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Http.Client.IdentityModel.Web;
using Volo.Abp.Modularity;

namespace BaseService.HttpApi.Client
{
    [DependsOn(
        typeof(BaseServiceApplicationContractsModule),
        typeof(AbpHttpClientIdentityModelModule),
        typeof(AbpHttpClientIdentityModelWebModule)
       
)]
    public class BaseServiceHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "BaseService";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(BaseServiceApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
