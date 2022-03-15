using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Consul.AspNetCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Consul;
using Microsoft.Extensions.Options;

namespace MyStore.Core.ServiceDiscovery.Consul.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddConsulDiscovery(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConsulClientConfiguration>(options => configuration.GetSection("ServiceDiscovery:Consul").Bind(options));
            services.TryAddSingleton<IConsulClientFactory, ConsulClientFactory>();
            services.TryAddSingleton((IServiceProvider sp) => sp.GetRequiredService<IConsulClientFactory>().CreateClient(Options.DefaultName));

            // Service registration
            services.AddHostedService<AgentServiceRegistrationHostedService>();
            services.TryAddTransient<AgentServiceRegistration>(serviceProvider =>
            {
                var serviceDiscoveryOptions = serviceProvider.GetRequiredService<IOptions<ServiceDiscoveryConfiguration>>()?.Value;

                return new AgentServiceRegistration
                {
                    Address = serviceDiscoveryOptions?.Address,
                    Name = serviceDiscoveryOptions?.ServiceName,
                    ID = Guid.NewGuid().ToString(),
                    Port = serviceDiscoveryOptions?.Port ?? 0,
                    Checks = new[]
                    {
                        new AgentCheckRegistration
                        {
                            HTTP = $"{serviceDiscoveryOptions?.Address}/api/health/status",
                            Notes = "Checks /health/status",
                            Timeout = TimeSpan.FromSeconds(3),
                            Interval = TimeSpan.FromSeconds(10)
                        }
                    }
                };
            });

            return services;
        }
    }
}
