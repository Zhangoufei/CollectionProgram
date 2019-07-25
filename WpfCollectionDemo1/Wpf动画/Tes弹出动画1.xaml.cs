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
using System.Windows.Threading;

namespace Wpf动画
{
    /// <summary>
    /// Tes弹出动画1.xaml 的交互逻辑
    /// </summary>
    public partial class Tes弹出动画1 : Window
    {
        public Tes弹出动画1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // this.Close();   
            /////timer定义  
            tm.Tick += new EventHandler(tm_Tick);
            tm.Interval = TimeSpan.FromSeconds(0.2);
            tm.Start();
        }
        DispatcherTimer tm = new DispatcherTimer();
        void tm_Tick(object sender, EventArgs e)
        { this.Close(); }
    }
}
