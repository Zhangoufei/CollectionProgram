using Com.Tiye.Log;
using CommonCtrls;
using System;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using WpfCollectionDemo1.Common;
using WpfCollectionDemo1.mvvm的使用.baseControl;
using WpfCollectionDemo1.mvvm的使用.UserControl;
using WPFControlMyself;
using System.Runtime.InteropServices;  //调用WINDOWS API函数时要用到
using Microsoft.Win32;  //写入注册表时要用到
using Com.Zhang.Common;

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

            Closing += MainWindow_Closing;

        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
         //   Environment.Exit(0);
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

        private void Button_Click_16(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_17(object sender, RoutedEventArgs e)
        {


        }

        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            var result = ICMessageBox.Show("你确定要删除当前画板吗 ?", "提示", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                ShowDialogWindow showDialogWindow = new ShowDialogWindow();
                showDialogWindow.Owner = this;
                showDialogWindow.Show();

            }
        }

        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            WindowShowDailog windowShowDailog = new WindowShowDailog();
            windowShowDailog.Owner = this;
            windowShowDailog.Show();
        }

        private void Button_Click_20(object sender, RoutedEventArgs e)
        {
            QrocodeWindow qrocodeWindow = new QrocodeWindow();
            qrocodeWindow.Show();
        }

        private void Button_Click_21(object sender, RoutedEventArgs e)
        {
            byte[] mac = new byte[6];

            //MAC地址分配到mac数组中
            //mac[0] = 0x00;
            //mac[1] = 0x01;
            //mac[2] = 0x80;
            //mac[3] = 0x79;
            //mac[4] = 0x08;
            //mac[5] = 0xD8;

            mac[0] = 0xEC;
            mac[1] = 0xD6;
            mac[2] = 0x8A;
            mac[3] = 0x28;
            mac[4] = 0x4F;
            mac[5] = 0xDD;

            WakeUp(mac);
        }
        /// <summary>
        /// 远程开机，网卡需要具备远程唤醒功能
        /// </summary>
        /// <param name="mac">网卡物理地址字符数组</param>
        public static void WakeUp(byte[] mac)
        {
            UdpClient client = new UdpClient();
            client.Connect(IPAddress.Broadcast, 9090);

            byte[] packet = new byte[17 * 6];

            for (int i = 0; i < 6; i++)
                packet[i] = 0xFF;

            for (int i = 1; i <= 16; i++)
                for (int j = 0; j < 6; j++)
                    packet[i * 6 + j] = mac[j];

            int result = client.Send(packet, packet.Length);
        }

        private void Button_Click_22(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.Application.Restart();
            Environment.Exit(0);
            //Application.Current.Shutdown();
        }

        private void Button_Click_23(object sender, RoutedEventArgs e)
        {
            string temp1 = @"D:\picture\tiye大屏背景\02.jpg";

            SetWallpaper.SetWall(temp1);
        }

        private void Button_Click_24(object sender, RoutedEventArgs e)
        {
            EnlargementShrink enlargementShrink = new EnlargementShrink();
            enlargementShrink.Owner = this;
            enlargementShrink.Show();
        }

        private void Button_Click_25(object sender, RoutedEventArgs e)
        {
            WindowBigEnlarg windowBigEnlarg = new WindowBigEnlarg();
            windowBigEnlarg.Owner = this;
            windowBigEnlarg.Show();
        }
    }
}
