using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IdentityServer.Factories;

public class PersistedGrantDbContextFactory : IDesignTimeDbContextFactory<PersistedGrantDbContext>
{
    public PersistedGrantDbContext CreateDbContext(string[] args)
    {
        IConfigurationRoot config = new ConfigurationBuilder()
                                    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
                                    .AddJsonFile("appsettings.json")
                                    .AddEnvironmentVariables()
                                    .Build();

        DbContextOptionsBuilder<PersistedGrantDbContext> optionsBuilder =
            new DbContextOptionsBuilder<PersistedGrantDbContext>();

        OperationalStoreOptions operationOptions = new OperationalStoreOptions();

        optionsBuilder.UseSqlServer(config["ConnectionString"], o => o.MigrationsAssembly("IdentityServer"));

        return new PersistedGrantDbContext(optionsBuilder.Options, operationOptions);
    }
}