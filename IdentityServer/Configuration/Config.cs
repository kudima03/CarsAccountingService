using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServer.Configuration;

public static class Config
{
    internal static IEnumerable<ApiResource> GetResources()
    {
        return new List<ApiResource>
        {
            new("Cars.API")
            {
                Scopes = new[]
                {
                    "Cars.API"
                }
            }
        };
    }

    internal static IEnumerable<ApiScope> GetScopes()
    {
        return new List<ApiScope>
        {
            new("Cars.API")
        };
    }

    internal static IEnumerable<Client> GetClients(Dictionary<string, string> clientsUrl)
    {
        return new List<Client>
        {
            new()
            {
                ClientId = "mvc",
                ClientName = "CarsMvcClient",
                ClientSecrets = new List<Secret>
                {
                    new("secret".Sha256())
                },
                AllowedGrantTypes = GrantTypes.Code,
                AllowAccessTokensViaBrowser = false,
                RequireConsent = false,
                AllowOfflineAccess = true,
                AlwaysIncludeUserClaimsInIdToken = true,
                ClientUri = $"{clientsUrl["MvcClient"]}",
                RedirectUris = new List<string>
                {
                    $"{clientsUrl["MvcClient"]}/signin-oidc"
                },
                PostLogoutRedirectUris = new List<string>
                {
                    $"{clientsUrl["MvcClient"]}/signout-callback-oidc"
                },
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "Cars.API"
                },
                RequirePkce = false,
                AccessTokenLifetime = 60 * 60 * 2,  // 2 hours
                IdentityTokenLifetime = 60 * 60 * 2 // 2 hours
            }
        };
    }

    internal static IEnumerable<IdentityResource> GetIdentityResources()
    {
        return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
    }
}