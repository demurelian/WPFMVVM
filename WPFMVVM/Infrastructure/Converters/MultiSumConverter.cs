using System.Globalization;
using System.Windows.Markup;

namespace WPFMVVM.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(MultiSumConverter))]
    internal class MultiSumConverter : MultiConverter
    {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double sum = 0;
            foreach (var item in values)
            {
                sum += System.Convert.ToDouble(item);
            }
            return sum;
        }
    }
}
