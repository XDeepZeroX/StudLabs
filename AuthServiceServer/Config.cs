using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthServiceServer
{
    internal class Config
    {

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
                //new IdentityResource("roles", "Roles", new List<string>(){ "role" })
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
               new Client
                {
                    ClientId = "StudLab",
                    ClientName = "StudLab",
                    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientUri = "https://localhost:5002",
                    RedirectUris           = { "https://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    },


                    AllowOfflineAccess = true,
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,

                    RequireConsent = false
                }
                // OpenID Connect hybrid flow client (MVC)
                //new Client
                //{
                //    ClientId = "BiLab",
                //    ClientName = "BiLab",
                //    AllowedGrantTypes = GrantTypes.HybridAndClientCredentials,
                //    ClientSecrets =
                //    {
                //        new Secret("secret".Sha256())
                //    },
                //    ClientUri = "https://localhost:5002",
                //    RedirectUris           = { "https://localhost:5002/signin-oidc" },
                //    PostLogoutRedirectUris = { "https://localhost:5002/signout-callback-oidc" },

                //    AllowedScopes =
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile
                //    },
                    

                //    AllowOfflineAccess = true,
                //    AlwaysSendClientClaims = true,
                //    AlwaysIncludeUserClaimsInIdToken = true,

                //    RequireConsent = false
                //}
            };
        }

        public static IEnumerable<ApiResource> GetAPIResources()
        {
            var temp = new List<ApiResource>()
            { 
                
            };



            return temp;
        }

    }
}