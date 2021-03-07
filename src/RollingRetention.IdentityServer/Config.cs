using System.Collections.Generic;
using IdentityServer4.Models;

namespace RollingRetention.IdentityServer
{
    public static class Config
    {
        public static IList<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };
        }

        public static IList<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource()
                {
                    Name = "app.api",
                    DisplayName = "My API",
                    Scopes = new List<string>()
                    {
                        "app.api.client"
                    }
                }
            };
        }

        public static IList<Client> GetClients()
        {

            return new List<Client>()
            {
                new Client()
                {
                    ClientId = "spa_client",
                    ClientName = "SPA Apps",
                    ClientSecrets = {
                        new Secret("super_secret_string".Sha256())
                    },
                    AllowOfflineAccess = true,
                    RequirePkce = true,
                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    AllowedScopes = {
                        "app.api.client",
                        "openid",
                        "profile",
                        "offline_access",
                    },
                    RedirectUris = {
                        "http://localhost:3000/sign-oidc",
                        "http://localhost:3000"
                    },
                    PostLogoutRedirectUris = {
                        "http://localhost:3000/signout-oidc",
                        "http://localhost:3000"
                    }
                }
            };
        }
    }
}