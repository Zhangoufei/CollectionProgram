using Com.Tiye.Log;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfCollectionDemo1.Common;
using WpfCollectionDemo1.mvvm的使用.baseControl;
using WpfCollectionDemo1.mvvm的使用.UserControl;

namespace WpfCollectionDemo1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Content = new ListBox滚动条样式();

            //Content = new 控件模板学习();

            //Content = new ItemsControlDemo();

            // Content = new UseSliderTest();

            Logger.DebugMode = true;

            Logger.Debug("324");


            Closed += MainWindow_Closed;



        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            //只能进程树关闭
            //System.Windows.Forms.Application.Restart();
            //Application.Current.Shutdown();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            /*WPF中处理消息首先要获取窗口句柄，创建HwndSource对象 通过HwndSource对象添加
             * 消息处理回调函数.HwndSource类: 实现其自己的窗口过程。 创建窗口之后使用 AddHook 
             * 和 RemoveHook 来添加和移除挂钩，接收所有窗口消息。*/
            ConvertCommon.MainWindow = this;

            Logger.Debug("444");

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SliderControl sliderControl = new SliderControl();
            sliderControl.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            图片切换 tu = new 图片切换();
            tu.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Popup使用 popup = new Popup使用();
            popup.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            单选框使用 danxuan = new 单选框使用();
            danxuan.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            BaseLayout baseLayout = new BaseLayout();
            baseLayout.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            Content = new ListBox滚动条样式();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            Content = new Page1();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            Content = new 手势操作();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            Content = new ItemsControlDemo();
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            Content = new 安装字体();
        }

        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            Content = new ListBox横向滚动条();
        }

        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            Content = new Notify();
        }

        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            Content = new TablControl2();
        }

        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            tableActivity.Focus();
        }

        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            动画 dong = new 动画();
            dong.Show();
        }

        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            WindowInkCanvas windowInkCanvas = new WindowInkCanvas();
            windowInkCanvas.Show();
        }
    }
}
