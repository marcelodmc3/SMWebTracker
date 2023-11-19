using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SMWebTracker.DI
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDIConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            InjectorBootStrapper.RegisterServices(services, configuration);
        }
    }
}