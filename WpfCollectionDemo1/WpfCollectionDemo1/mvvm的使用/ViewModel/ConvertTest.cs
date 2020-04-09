using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace WpfCollectionDemo1.mvvm的使用.ViewModel
{
    [ValueConversion(typeof(decimal), typeof(string))]
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

    public class UrlConvertBitmapImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null)
                return null;

            string tempresult = value.ToString();


            BitmapImage bitmapImage = new BitmapImage();

            bitmapImage.BeginInit();

            bitmapImage.UriSource = new System.Uri(tempresult, UriKind.Absolute);

            bitmapImage.DecodePixelWidth = 800;

            bitmapImage.EndInit();

            return bitmapImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
