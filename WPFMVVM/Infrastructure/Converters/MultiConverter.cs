using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace WPFMVVM.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(MultiConverter))]
    internal abstract class MultiConverter : IMultiValueConverter
    {
        public abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);
        public virtual object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ОШИБКА: Обратное преобразование не поддерживается");
        }
    }
}
