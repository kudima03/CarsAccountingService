using IdentityServer;
using IdentityServer.Data;
using IdentityServer.Extensions;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore;

internal class Program
{
    private static void Main(string[] args)
    {
        IWebHost host = CreateHostBuilder(args).Build();

        host.MigrateDbContext<PersistedGrantDbContext>((_, __) => { })
            .MigrateDbContext<AuthorizationDbContext>((context, services) =>
            {
                IWebHostEnvironment? env = services.GetService<IWebHostEnvironment>();

                ILogger<AuthorizationDbContextSeed>?
                    logger = services.GetService<ILogger<AuthorizationDbContextSeed>>();

                new AuthorizationDbContextSeed()
                    .SeedAsync(context, env, logger, 10)
                    .Wait();
            })
            .MigrateDbContext<ConfigurationDbContext>((context, services) =>
            {
                IConfiguration? cfg = services.GetService<IConfiguration>();

                new ConfigurationDbContextSeed()
                    .SeedAsync(context, cfg)
                    .Wait();
            });

        host.Run();
    }

    public static IWebHostBuilder CreateHostBuilder(string[] args)
    {
        return WebHost.CreateDefaultBuilder(args)
                      .UseStartup<Startup>();
    }
}