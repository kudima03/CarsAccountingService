﻿using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace IdentityServer.Factories;

public class ConfigurationDbContextFactory : IDesignTimeDbContextFactory<ConfigurationDbContext>
{
    public ConfigurationDbContext CreateDbContext(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory()))
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ConfigurationDbContext>();
        var storeOptions = new ConfigurationStoreOptions();

        optionsBuilder.UseSqlServer(config["ConnectionString"], o => o.MigrationsAssembly("IdentityServer"));

        return new ConfigurationDbContext(optionsBuilder.Options, storeOptions);
    }
}