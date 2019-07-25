using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Walterlv.Demo.Interop;

namespace 倒计时
{
    /// <summary>
    /// MyUserControl.xaml 的交互逻辑
    /// </summary>
    public partial class MyUserControl : UserControl
    {
        public MyUserControl()
        {
            InitializeComponent();

            WindowBlur.SetIsEnabled(this, true);
        }
        /// <summary>
        /// 处理倒计时的委托
        /// </summary>
        /// <returns></returns>
        public delegate bool CountDownHandler();

        private DispatcherTimer timer;
        private ProcessCount processCount;

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (startTime.Content.ToString() == "开始")
            {
                startTime.Content = "暂停";

                //设置定时器
                if (timer == null || restart)
                {
                    restart = false;

                    timer = new DispatcherTimer();
                    timer.Interval = new TimeSpan(0, 0, 1);   //时间间隔为一秒
                    timer.Tick += new EventHandler(timer_Tick);

                    //转换成秒数
                    Int32 hour = Convert.ToInt32(HourArea.Text);
                    Int32 minute = Convert.ToInt32(MinuteArea.Text);
                    Int32 second = Convert.ToInt32(SecondArea.Text);

                    //处理倒计时的类
                    processCount = new ProcessCount(hour * 3600 + minute * 60 + second);
                    CountDown += new CountDownHandler(processCount.ProcessCountDown);

                    //开启定时器
                    timer.Start();
                }
                else
                {
                    timer.Start();
                }

            }
            else  //暂停处理
            {
                startTime.Content = "开始";
                timer.Stop();
            }



        }

        /// <summary>
        /// 处理事件
        /// </summary>
        public event CountDownHandler CountDown;
        public bool OnCountDown()
        {
            if (CountDown != null)
                return CountDown();

            return false;
        }

        /// <summary>
        /// Timer触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (OnCountDown())
            {
                HourArea.Text = processCount.GetHour();
                MinuteArea.Text = processCount.GetMinute();
                SecondArea.Text = processCount.GetSecond();
            }
            else
                timer.Stop();
        }

        private bool restart = false;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            HourArea.Text = "00";
            MinuteArea.Text = "00";
            SecondArea.Text = "00";

            processCount.TotalSecond = 0;

            restart = true;

        }

        private void MinuteArea_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void MinuteArea_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            //if (string.IsNullOrWhiteSpace(textBox.SelectedText))
            //{
                textBox.SelectAll();
            //}

        }

        private void StartTime2_Click(object sender, RoutedEventArgs e)
        {
            var darkwindow = new Window()
            {
                Background = Brushes.Black,
                Opacity = 0.4,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                WindowState = WindowState.Maximized,
                Topmost = true
            };
            darkwindow.Show();
        }
    }
}
