using System;
using System.Collections.Generic;
using System.Windows;
using Wpf动画.CSDN;
using Wpf动画.MyAnimation;

namespace Wpf动画
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public static MainWindow MainWindowStatic;

        public MainWindow()
        {
            InitializeComponent();

            MainWindowStatic = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Test1 test1 = new Test1();
            test1.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Tes弹出动画1 tes = new Tes弹出动画1();
            tes.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            TestAnimation1 testAnimation1 = new TestAnimation1();
            testAnimation1.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ButtonAnimations buttonAnimations = new ButtonAnimations();
            buttonAnimations.Show();

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            滑动图片动画 cc = new 滑动图片动画();
            cc.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            WPF制作GIF动画 wPF = new WPF制作GIF动画();
            wPF.Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            倒计时动画 click = new 倒计时动画();
            click.Show();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            封面相册动画 ccc = new 封面相册动画();

            List<Uri> list = new List<Uri>(new[]
             {
                 new Uri(@"pack://application:,,,/images/clock/clock00001.png"),
                 new Uri(@"pack://application:,,,/images/clock/clock00002.png"),
                 new Uri(@"pack://application:,,,/images/clock/clock00003.png"),
                 new Uri(@"pack://application:,,,/images/clock/clock00004.png"),
                 new Uri(@"pack://application:,,,/images/clock/clock00005.png"),
                 new Uri(@"pack://application:,,,/images/clock/clock00006.png"),
                 new Uri(@"pack://application:,,,/images/clock/clock00007.png"),
                 new Uri(@"pack://application:,,,/images/clock/clock00008.png"),
                 new Uri(@"pack://application:,,,/images/clock/clock00009.png"),
                 new Uri(@"pack://application:,,,/images/clock/clock00010.png"),
                 new Uri(@"pack://application:,,,/images/clock/clock00011.png"),
             });
            ccc.Init(list);
            ccc.Show();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            EasyAnimation easyAnimation = new EasyAnimation();
            easyAnimation.Show();
        }
    }
}
