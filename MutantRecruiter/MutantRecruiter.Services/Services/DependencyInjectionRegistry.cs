using Microsoft.Extensions.DependencyInjection;
using MutantRecruiter.Services.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace MutantRecruiter.Services.Services
{
    public static class DependencyInjectionRegistry
    {
        public static IServiceCollection AddFunctionService(this IServiceCollection services, string endPoint, string key, string database)
        {
            services.AddSingleton<ICosmosDBService<Human>>(new CosmosDBService<Human>(endPoint, key, database));
            return services;
        }
    }
}
