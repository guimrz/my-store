using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MyStore.Core.ServiceDiscovery.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ServiceDiscoveryConfiguration>(options => configuration.GetSection("ServiceDiscovery").Bind(options));

            return services;        
        }
    }
}
