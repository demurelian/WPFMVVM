using Microsoft.Extensions.DependencyInjection;
using WPFMVVM.Services.Interfaces;

namespace WPFMVVM.Services
{
    internal static class Registrator
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IDataService, DataService>();

            services.AddTransient<IAsyncDataService, AsyncDataService>();

            return services;
        }
    }
}
