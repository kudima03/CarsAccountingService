﻿using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Extensions;

public static class IWebHostExtensions
{
    public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost, Action<TContext, IServiceProvider> seeder)
        where TContext : DbContext
    {
        using var scope = webHost.Services.CreateScope();
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<TContext>>();
        var context = services.GetService<TContext>();

        try
        {
            logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name}");
            InvokeSeeder(seeder, context, services);
            logger.LogInformation($"Migrated database associated with context {typeof(TContext).Name}");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"An error occurred while migrating the databases on context {typeof(TContext).Name}");
        }

        return webHost;
    }

    private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context,
        IServiceProvider services)
        where TContext : DbContext
    {
        context.Database.Migrate();
        seeder(context, services);
    }
}