using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace WPFMVVM.ViewModels
{
    internal static class Registrator
    {
        public static IServiceCollection RegisterViewModels(this IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<CountriesStatisticViewModel>();

            return services;
        }
    }
}
