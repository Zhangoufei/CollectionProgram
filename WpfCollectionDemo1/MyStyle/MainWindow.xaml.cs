using MyStyle.pages;
using MyStyle.Styles;
using System;
using System.Diagnostics;
using System.Windows;
using 倒计时;

namespace MyStyle
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Content = new ButtonPage();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Content = new TextBoxPage();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Content = new ScrollViewerPage();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Content = new ListBoxPage();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            ProcessWindow processWindow = new ProcessWindow();
            processWindow.grid.Children.Add(new UserControlTimer());
            processWindow.Show();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            ScrollTextBlockWindow scrollTextBlockWindow = new ScrollTextBlockWindow();
            scrollTextBlockWindow.Show();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            OpenUri("http://www.baidu.com");
        }


        /// <summary>
        /// 直接打开Uri类型的链接（经过转码后的string再经过tostring()会出现乱码，用另一个方法）
        /// </summary>
        /// <param name="uri"></param>
        public static void OpenUri(Uri uri)
        {
            if (uri != null)
            {
                try
                {
                    System.Diagnostics.Process.Start(uri.ToString());
                }
                catch
                {
                    // RGBMessageBox.Show("网址是：" + uri.ToString(), "网络浏览器调用失败，请确定你的电脑中安装了网络浏览器！", RGBMesType.Warn);
                }
            }
        }

        /// <summary>
        /// 直接打开string类型的链接（可以是经过转码后的链接）
        /// </summary>
        /// <param name="uri"></param>
        public static void OpenUri(string uri)
        {
            if (!string.IsNullOrEmpty(uri))
            {
                try
                {
                    System.Diagnostics.Process.Start(uri);
                }
                catch
                {
                    //  RGBMessageBox.Show("网址是：" + uri.ToString(), "网络浏览器调用失败，请确定你的电脑中安装了网络浏览器！", RGBMesType.Warn);
                }
            }
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = @"E:\资源\希沃白板5.enbx";
            //process.StartInfo.Arguments = @"E:\资源\希沃白板5.enbx";
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
            process.Start();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            WindowStyle windowStyle = new WindowStyle();

        }
    }
}
