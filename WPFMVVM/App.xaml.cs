using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using WPFMVVM.Services;
using WPFMVVM.ViewModels;

namespace WPFMVVM
{
    public partial class App
    {
        public static bool IsDesignMode { get; private set; } = true;

        private static IHost __Host;
        
        public static IHost Host => __Host ??= Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
        
        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            base.OnStartup(e);

            var host = Host;
            await host.StartAsync().ConfigureAwait(false);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);

            var host = Host;
            await host.StopAsync().ConfigureAwait(false);
            host.Dispose();
            __Host = null;
        }
        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton<DataService>();
            services.AddSingleton<CountriesStatisticViewModel>();
        }

        public static string CurrentDirectory => IsDesignMode 
            ? Path.GetDirectoryName(GetSourceCodePath()) 
            : Environment.CurrentDirectory;

        private static string GetSourceCodePath([CallerFilePath]string Path = null) => Path;
    }
}
