using IdentityServer.Configuration;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using IdentityServer4.Models;

namespace IdentityServer.Data;

public class ConfigurationDbContextSeed
{
    public async Task SeedAsync(ConfigurationDbContext context, IConfiguration configuration)
    {
        Dictionary<string, string> clientLinks = new Dictionary<string, string>
        {
            {
                "MvcClient", configuration.GetValue<string>("MvcClient")
            }
        };

        if (!context.Clients.Any())
        {
            IEnumerable<Client> clients = Config.GetClients(clientLinks);

            foreach (Client client in clients)
            {
                context.Clients.Add(client.ToEntity());
            }

            await context.SaveChangesAsync();
        }

        if (!context.ApiScopes.Any())
        {
            IEnumerable<ApiScope> apiScopes = Config.GetScopes();

            foreach (ApiScope scope in apiScopes)
            {
                context.ApiScopes.Add(scope.ToEntity());
            }

            await context.SaveChangesAsync();
        }

        if (!context.IdentityResources.Any())
        {
            foreach (IdentityResource resource in Config.GetIdentityResources())
            {
                context.IdentityResources.Add(resource.ToEntity());
            }

            await context.SaveChangesAsync();
        }

        if (!context.ApiResources.Any())
        {
            foreach (ApiResource api in Config.GetResources())
            {
                context.ApiResources.Add(api.ToEntity());
            }

            await context.SaveChangesAsync();
        }
    }
}