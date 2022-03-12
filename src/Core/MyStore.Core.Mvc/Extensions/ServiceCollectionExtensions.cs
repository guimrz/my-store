using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using MyStore.Core.Mvc.Filters;
using System.Reflection;

namespace MyStore.Core.Mvc.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRequestValidation(this IServiceCollection services, params Assembly[] validatorAssemblies)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationFilter>();
            })
            .AddFluentValidation(fv =>
            {
                fv.RegisterValidatorsFromAssemblies(validatorAssemblies);
                fv.LocalizationEnabled = false;
            });

            return services;
        }
    }
}
