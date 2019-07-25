using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace WpfCollectionDemo1.mvvm的使用.ViewModel
{
    [ValueConversion(typeof(decimal),typeof(string))]
    public class ConvertTest : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            decimal price = (decimal)value;
            return price.ToString("C", culture);

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string price = culture.ToString();
            decimal result;

            if (Decimal.TryParse(price, NumberStyles.Any, culture, out result))
            {

                return result;
            }
            return value;
        }
    }
}
