using Emergent.Challenge.Core.Definitions;
using Emergent.Challenge.Core.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Emergent.Challenge.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddChallengeServices(this IServiceCollection services)
        {
            // Register MediatR
            services.AddMediatR(typeof(ServiceCollectionExtensions));

            // Register services
            services.TryAddTransient<IVersionMatcher, VersionMatcher>();
            
            return services;
        }
    }
}
