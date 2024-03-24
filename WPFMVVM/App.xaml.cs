using System.Windows;

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
}
