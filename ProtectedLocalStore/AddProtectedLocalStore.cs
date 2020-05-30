using System;
using System.Collections.Generic;
using System.Text;
using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;

namespace ProtectedLocalStore
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddProtectedLocalStore(this IServiceCollection services, EncryptionService encryptionService)
        {
            services.AddBlazoredLocalStorage();
            services.AddTransient(ec => encryptionService);
            services.AddScoped<ProtectedLocalStore>();
            return services;
        }
    }
}
