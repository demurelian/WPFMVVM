using System.Windows;

namespace WPFMVVM.Components
{
    public partial class GaugeIndicator
    {
        #region ValueProperty
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(double),
                typeof(GaugeIndicator),
                new PropertyMetadata(default(double),
                    OnValuePropertyChanged,
                    OnCoerceValue),
                OnValidateValue);

        private static bool OnValidateValue(object value) => true;

        private static object OnCoerceValue(DependencyObject d, object baseValue)
        {
            var value = (double)baseValue;
            return Math.Max(0, Math.Min(100, value));
        }

        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            //var gauge_indicator = (GaugeIndicator)d;
            //gauge_indicator.ArrowRotator.Angle = (double)e.NewValue;
        }

        public double Value
        {
            get => (double) GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }
        #endregion
        public GaugeIndicator()
        {
            InitializeComponent();
        }
    }
}
