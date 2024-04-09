using System.Threading;
using System.Windows;

namespace WMWpfTest
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Thread(ComputeValue).Start();
            
        }

        private void ComputeValue()
        {
            var value = LongProcess(DateTime.Now);
            if (ResultBlock.Dispatcher.CheckAccess())
                ResultBlock.Text = value;
            else
                ResultBlock.Dispatcher.Invoke(() => ResultBlock.Text = value);
        }

        private static string LongProcess(DateTime Time)
        {
            Thread.Sleep(2000);

            return $"Value: {Time}";
        }
    }
}