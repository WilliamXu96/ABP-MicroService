using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace AuthServer.Host
{
    public class Program
    {
        public static int Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.WithProperty("Application", "AuthServer")
                .Enrich.FromLogContext()
                .WriteTo.File("Logs/logs.txt")
                //.WriteTo.Elasticsearch(
                //    new ElasticsearchSinkOptions(new Uri(configuration["ElasticSearch:Url"]))
                //    {
                //        AutoRegisterTemplate = true,
                //        AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                //        IndexFormat = "xdlms-log-{0:yyyy.MM}"
                //    })
                .WriteTo.Console()
                .CreateLogger();

            try
            {
                Log.Information("Starting AuthServer.Host.");
                CreateHostBuilder(args).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "AuthServer.Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        internal static IHostBuilder CreateHostBuilder(string[] args) =>
            Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseAutofac()
                .UseSerilog();
    }
}
