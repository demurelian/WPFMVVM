using System.Globalization;
using System.Windows.Data;

namespace WPFMVVM.Infrastructure.Converters
{
    internal abstract class MultiConverter : IMultiValueConverter
    {
        public abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);
        public virtual object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ОШИБКА: Обратное преобразование не поддерживается");
        }
    }
}
