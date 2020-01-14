using MystyleUserControl.控件;
using MystyleUserControl.样式;
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

namespace MystyleUserControl
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
            Button button = e.OriginalSource as Button;

            switch (button.Content)
            {
                case "自定义布局":
                    CustomPanelDemo1 customPanelDemo1 = new CustomPanelDemo1();
                    customPanelDemo1.Show();
                    break;
                case "控件":
                    UserContrl userContrl = new UserContrl();
                    userContrl.Show();
                    break;
                case "控件2":
                    UserControl2Window userContrl2 = new UserControl2Window();
                    userContrl2.Show();
                    break;
                case "样式1":
                    Style1Window style1Window = new Style1Window();
                    style1Window.Show();
                    break;
            }
        }
    }
}
