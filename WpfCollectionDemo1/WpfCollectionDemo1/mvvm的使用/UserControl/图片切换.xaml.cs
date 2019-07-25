using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfCollectionDemo1.mvvm的使用.UserControl
{
    /// <summary>
    /// 图片切换.xaml 的交互逻辑
    /// </summary>
    public partial class 图片切换 : Window
    {
        public 图片切换()
        {
            InitializeComponent();

            gridImage2.Children.Remove(image);
            gridImage2.Children.Remove(image2);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Image iamge = new Image();

            //BitmapImage bitmapImage = new BitmapImage();
            //bitmapImage.BeginInit();
            //bitmapImage.UriSource = new Uri(AppDomain.CurrentDomain.BaseDirectory + @"resource\38c58PICtuS.jpg", UriKind.Absolute);
            //bitmapImage.EndInit();
            //iamge.Source = bitmapImage;
            //gridImage.Children.Add(image2);

            if (gridImage.Children.Count > 0)
            {
                gridImage.Children.RemoveAt(0);
            }
            gridImage.Children.Add(image);

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (gridImage.Children.Count > 0)
            {
                gridImage.Children.RemoveAt(0);
            }
            gridImage.Children.Add(image2);
        }
    }
}
