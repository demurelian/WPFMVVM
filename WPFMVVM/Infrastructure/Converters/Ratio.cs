using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Data;

namespace WPFMVVM.Infrastructure.Converters
{
    internal class Ratio : Converter
    {
        public double K { get; set; } = 1;
        public Ratio() { }
        public Ratio(double K) => this.K = K;
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;

            var x = System.Convert.ToDouble(value, culture);

            return x * K;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;

            var x = System.Convert.ToDouble(value, culture);

            return x / K;
        }
    }
}
