using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blazor.App.Web
{
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("app");

            //builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri("http://localhost:55389") });

            builder.Services.AddHttpClient("companiesAPI", cl =>
            {
                cl.BaseAddress = new Uri("http://localhost:55389");
            })
            .AddHttpMessageHandler(sp =>
            {
                var handler = sp.GetService<AuthorizationMessageHandler>()
                .ConfigureHandler(
                    authorizedUrls: new[] { "http://localhost:55389" },
                    scopes: new[] { "BaseService" }
                 );
                return handler;
            });
            builder.Services.AddScoped(
                sp => sp.GetService<IHttpClientFactory>().CreateClient("companiesAPI"));

            // 增加 BootstrapBlazor 组件
            builder.Services.AddBootstrapBlazor();

            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("AuthServer", options.ProviderOptions);
                options.ProviderOptions.DefaultScopes.Add("role");
                options.ProviderOptions.DefaultScopes.Add("email");
                options.ProviderOptions.DefaultScopes.Add("phone");
            });

            var host = builder.Build();
            await host.RunAsync();
        }
    }
}
