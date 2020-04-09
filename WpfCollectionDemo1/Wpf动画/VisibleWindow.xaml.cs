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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wpf动画
{
    /// <summary>
    /// VisibleWindow.xaml 的交互逻辑
    /// </summary>
    public partial class VisibleWindow : Window
    {
        public VisibleWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Storyboard storyboard = FindResource("StoryboardClose") as Storyboard;
            storyboard.Begin();
        }
    }
}
