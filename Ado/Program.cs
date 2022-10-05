using static AdoFrameWork.Core.RegisterService;
using AdoFrameWork.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

internal class Program
{
    private static void Main(string[] args)
    {
        var build = new ConfigurationBuilder();
        buildConfig(build);
        var host = Host.CreateDefaultBuilder()
        .ConfigureServices((config, service) =>
        {
            service.RegisterServices();
        }).Build();

        var startup = new Startup(host.Services);
        startup.BuildScreen();
        host.RunAsync();
    }
    private static void buildConfig(IConfigurationBuilder builder)
    {
        builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", false, true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json")
        .AddEnvironmentVariables();
    }
}