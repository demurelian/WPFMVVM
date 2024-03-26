using System.Globalization;

namespace WPFMVVM.Infrastructure.Converters
{
    internal class ToArray : MultiConverter
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) => values;
    }
}
