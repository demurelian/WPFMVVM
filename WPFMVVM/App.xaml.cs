using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;
using WPFMVVM.Services;
using WPFMVVM.ViewModels;

namespace WPFMVVM
{
    public partial class App
    {
        public static bool IsDesignMode { get; private set; } = true;
        
        protected override void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            base.OnStartup(e);
        }
    }

    public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
    {
        services.AddSingleton<DataService>();
        services.AddSingleton<CountriesStatisticViewModel>();
    }
}
