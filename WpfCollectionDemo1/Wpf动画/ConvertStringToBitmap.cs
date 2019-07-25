using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Wpf动画
{
    public class ConvertStringToBitmap : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string path = value.ToString();

            if (!string.IsNullOrEmpty(path))
            {
                //return Application.Current.MainWindow.FindResource(path)  as BitmapImage;

                return MainWindow.MainWindowStatic.FindResource(path) as BitmapImage;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
