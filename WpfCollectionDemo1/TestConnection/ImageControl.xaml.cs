using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace TestConnection
{
    /// <summary>
    /// ImageControl.xaml 的交互逻辑
    /// </summary>
    public partial class ImageControl : UserControl
    {
        public ImageControl(string uri)
        {
            InitializeComponent();

            imageUrl.Height = 200;
            imageUrl.Width = 300;
            imageUrl.Source = new BitmapImage(new Uri(uri));
        }

        private void ImageUrl_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }
    }
}
