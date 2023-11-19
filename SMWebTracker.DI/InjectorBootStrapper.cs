using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SMWebTracker.Data.Repositories;
using SMWebTracker.Domain.Interfaces;
using SMWebTracker.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.DI
{
    public class InjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();

            // Services
            services.AddScoped<IUserService, UserService>();
        }
    }
}
