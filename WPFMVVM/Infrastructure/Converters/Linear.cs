using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace WPFMVVM.Infrastructure.Converters
{
    /// <summary>
    /// Реализация линейного преобразования f(x) = k*x + b
    /// </summary>
    [ValueConversion(typeof(double), typeof(double))]
    internal class Linear : Converter
    {
        [ConstructorArgument("K")]
        public double K { get; set; } = 1;
        [ConstructorArgument("B")]
        public double B { get; set; } = 0;
        public Linear(double k, double b)
        {
            K = k;
            B = b;
        }
        public Linear()
        {
            
        }
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;

            var x = System.Convert.ToDouble(value, culture);

            return K * x + B;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;

            var y = System.Convert.ToDouble(value, culture);

            return (y - B) / K;
        }
    }
}
