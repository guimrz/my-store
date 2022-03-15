﻿using Duende.IdentityServer.Models;

namespace MyStore.Services.Identity.API
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                // Catalog specific scoppes
                new ApiScope("catalog", "Catalog API"),
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
            // m2m client credentials flow client
            new Client
            {
                ClientId = "client",
                ClientName = "Client Credentials Client",

                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedScopes = { "catalog", "openid", "profile" }
            },

            // interactive client using code flow + pkce
            new Client
            {
                ClientId = "interactive",
                ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,

                RedirectUris = { "https://localhost:44300/signin-oidc" },
                FrontChannelLogoutUri = "https://localhost:44300/signout-oidc",
                PostLogoutRedirectUris = { "https://localhost:44300/signout-callback-oidc" },

                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "catalog" }
            },
            };
    }
}