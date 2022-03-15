using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyStore.Services.Identity.Domain;
using MyStore.Services.Identity.Repository;
using Serilog;
using System.Security.Claims;

namespace MyStore.Services.Identity.API
{
    public class SeedData
    {
        public static async Task EnsureSeedDataAsync(WebApplication app)
        {
            using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

            await SeedConfiguration(scope);
            await SeedUsers(scope);
        }

        public static async Task SeedConfiguration(IServiceScope scope)
        {
            using var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();

            if (!await context.IdentityResources.AnyAsync())
            {
                foreach (var identityResource in Config.IdentityResources)
                {
                    await context.IdentityResources.AddAsync(identityResource.ToEntity());
                }
            }

            if (!await context.Clients.AnyAsync())
            {
                foreach (var client in Config.Clients)
                {
                    await context.Clients.AddAsync(client.ToEntity());
                }
            }

            if (!await context.ApiScopes.AnyAsync())
            {
                foreach (var apiScope in Config.ApiScopes)
                {
                    await context.ApiScopes.AddAsync(apiScope.ToEntity());
                }
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedUsers(IServiceScope scope)
        {
            var context = scope.ServiceProvider.GetService<IdentityDbContext>();
            await context.Database.MigrateAsync();

            var roleMgr = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            await roleMgr.CreateAsync(new IdentityRole("Admin"));

            var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var alice = userMgr.FindByNameAsync("alice").Result;
            if (alice == null)
            {
                alice = new ApplicationUser
                {
                    UserName = "alice",
                    Email = "AliceSmith@email.com",
                    EmailConfirmed = true,
                };
                var result = userMgr.CreateAsync(alice, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(alice, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Alice"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("alice created");
            }
            else
            {
                Log.Debug("alice already exists");
            }

            var bob = userMgr.FindByNameAsync("bob").Result;
            if (bob == null)
            {
                bob = new ApplicationUser
                {
                    UserName = "bob",
                    Email = "BobSmith@email.com",
                    EmailConfirmed = true
                };
                var result = userMgr.CreateAsync(bob, "Pass123$").Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }

                result = userMgr.AddClaimsAsync(bob, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                            new Claim(JwtClaimTypes.GivenName, "Bob"),
                            new Claim(JwtClaimTypes.FamilyName, "Smith"),
                            new Claim(JwtClaimTypes.WebSite, "http://bob.com"),
                            new Claim("location", "somewhere")
                        }).Result;
                if (!result.Succeeded)
                {
                    throw new Exception(result.Errors.First().Description);
                }
                Log.Debug("bob created");

                await userMgr.AddToRoleAsync(bob, "Admin");
            }
            else
            {
                Log.Debug("bob already exists");
            }
        }
    }
}