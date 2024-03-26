using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WPFMVVM.Infrastructure.Converters
{
    internal class LocationPointToStr : Converter
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Point point)) return null;

            return $"Lat:{point.X};Lon:{point.Y}";
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string str)) return null;

            var components = str.Split(';');

            return new Point(double.Parse(components[0].Split(':')[1]), double.Parse(components[1].Split(':')[1]));
        }
    }
}
