using System.Globalization;
using System.Windows.Markup;

namespace WPFMVVM.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(ToArray))]
    internal class ToArray : MultiConverter
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) => values;
    }
}
