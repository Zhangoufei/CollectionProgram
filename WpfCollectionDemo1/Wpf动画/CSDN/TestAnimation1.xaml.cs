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

namespace Wpf动画.CSDN
{
    /// <summary>
    /// TestAnimation1.xaml 的交互逻辑
    /// </summary>
    public partial class TestAnimation1 : Window
    {
        public TestAnimation1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(OnTimedEvent);
            timer.Interval = TimeSpan.FromSeconds(1.0 / 20);
            timer.Start();
        }
        int index = 0;
        private void OnTimedEvent(object sender, EventArgs e)
        {
            index++;
            if (button1.Width == 1)
            {
                var timer = (System.Windows.Threading.DispatcherTimer)sender;
                timer.Stop();
                return;
            }
            button1.Width = 320 - (index++);
        }
        /// <summary>
        /// 将宽度变为320
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnTimedEvent2(object sender, EventArgs e)
        {
            index++;
            if (index > 40)
            {
                var timer = (System.Windows.Threading.DispatcherTimer)sender;
                timer.Stop();
            }
            button1.Width = 8 * (index++);
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(OnTimedEvent2);
            timer.Interval = TimeSpan.FromSeconds(1.0 / 20);
            timer.Start();
        }
    }
}
