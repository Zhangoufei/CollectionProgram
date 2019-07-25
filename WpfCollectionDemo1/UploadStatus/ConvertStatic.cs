using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace UploadStatus
{
    public class ConvertStaticBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return Visibility.Hidden;
            bool temp = (bool)value;
            if (temp)
            {

                return Visibility.Visible;
            }
            else
            {
                return Visibility.Hidden;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


    public class ConvertStaticResource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string path = (string)value;
            if (!string.IsNullOrEmpty(path))
            {
                //return new BitmapImage(new Uri(path, UriKind.Absolute));
                //return FindResource(path) as BitmapImage
                if (path.Contains("http"))
                {
                    //优化大分辨率 占用内存过高问题
                    BitmapImage bitmapImage = new BitmapImage();

                    bitmapImage.BeginInit();

                    bitmapImage.UriSource = new System.Uri(path, UriKind.Absolute);

                    bitmapImage.DecodePixelWidth = 800;

                    bitmapImage.EndInit();
                    //bitmapImage.Freeze();

                    return bitmapImage;
                }
                else
                {
                    return ComonConvert.FindResource(path) as BitmapImage;
                }
            }
            else
            {
                return ComonConvert.FindResource(path) as BitmapImage;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
