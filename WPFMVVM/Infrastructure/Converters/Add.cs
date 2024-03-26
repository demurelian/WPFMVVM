using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace WPFMVVM.Infrastructure.Converters
{
    [MarkupExtensionReturnType(typeof(Add))]
    internal class Add : Converter
    {
        [ConstructorArgument("B")]
        public double B { get; set; } = 0;
        public Add() { }
        public Add(double B) => this.B = B;
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;

            var x = System.Convert.ToDouble(value, culture);

            return x + B;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return null;

            var x = System.Convert.ToDouble(value, culture);

            return x - B;
        }
    }
}
