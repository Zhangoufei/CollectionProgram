using AutoUpdaterDotNET;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 磁盘操作
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            CommonTest.MainWindow = this;

            //AutoUpdater.AppTitle = "升级文件";
            //AutoUpdater.AppCastURL = "http://192.168.18.106:9050/Info.xml";

            //AutoUpdater.ApplicationExitEvent += AutoUpdater_ApplicationExitEvent;

            //AutoUpdater.Start();

        }

        private void AutoUpdater_ApplicationExitEvent()
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Content = new 读取磁盘信息();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Content = new 图片放大缩小();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Content = new Office打开();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
        }
    }
}
