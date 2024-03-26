using System.Globalization;

namespace WPFMVVM.Infrastructure.Converters
{
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
