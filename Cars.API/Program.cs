using Cars.API;
using Cars.API.Data.DbDataSource.Dapper;
using Microsoft.AspNetCore;

internal class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        var configManager = host.Services.GetService<IConfiguration>();
        new DatabaseInitializer(configManager).Initialize();
        new DatabaseSeed(configManager).Seed();
        host.Run();
    }

    public static IWebHostBuilder CreateHostBuilder(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
    }
}