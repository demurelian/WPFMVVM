using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using WPFMVVM.Components;

namespace WPFMVVM.Infrastructure.Converters
{
    internal class ParametricMultiplyValueConverter : Freezable, IValueConverter
    {
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register(
                nameof(Value),
                typeof(double),
                typeof(ParametricMultiplyValueConverter),
                new PropertyMetadata(1.0));
        [Description("Прибавляемое значение")]
        public double Value
        {
            get => (double) GetValue(ValueProperty);
            set => SetValue(ValueProperty, value);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = System.Convert.ToDouble(value, culture);
            return val * Value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = System.Convert.ToDouble(value, culture);
            return val / Value;
        }

        protected override Freezable CreateInstanceCore() => new ParametricMultiplyValueConverter { Value = Value };
    }
}
