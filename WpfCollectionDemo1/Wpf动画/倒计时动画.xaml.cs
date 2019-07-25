using System;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using Wpf动画.ViewModel;

namespace Wpf动画
{
    /// <summary>
    /// 倒计时动画.xaml 的交互逻辑
    /// </summary>
    public partial class 倒计时动画 : Window
    {

        private TimerClick timerClick = null;

        public static 倒计时动画 MMM;

        public 倒计时动画()
        {
            InitializeComponent();
            MMM = this;

            Loaded += 倒计时动画_Loaded;


            DataContext = timerClick = new TimerClick();

        }
        DispatcherTimer dispatcherTimer = null;
        private void 倒计时动画_Loaded(object sender, RoutedEventArgs e)
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick; ;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1);
            dispatcherTimer.Start();
        }

        int sumNum = 10;
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
           // Storyboard sbd = Application.Current.FindResource("Storyboard1") as Storyboard;
            Storyboard sbd = (Storyboard)MMM.FindResource("Storyboard1");
            sbd.Begin();

            if (sumNum == 0) {
                dispatcherTimer.Stop();
            }
            timerClick.TextBlockBing = sumNum+ "";
            sumNum--;
        }

        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
