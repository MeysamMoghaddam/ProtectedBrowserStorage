using Blazored.LocalStorage;
using Blazored.SessionStorage;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;


namespace ProtectedLocalStore
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddProtectedLocalStore(this IServiceCollection services, EncryptionService encryptionService)
        {
            services.AddBlazoredLocalStorage();
            services.AddBlazoredSessionStorage();
            services.AddTransient(ec => encryptionService);
            services.AddScoped<ProtectedLocalStorage>();
            services.AddScoped<ProtectedSessionStorage>();
            return services;
        }
    }
}
