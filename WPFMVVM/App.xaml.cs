using System.Windows;
using WPFMVVM.Services;

namespace WPFMVVM
{
    public partial class App
    {
        public static bool IsDesignMode { get; private set; } = true;
        
        protected override void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            base.OnStartup(e);

            //var service_test = new DataService();
            //var countries = service_test.GetData().ToArray();
        }
    }
}
