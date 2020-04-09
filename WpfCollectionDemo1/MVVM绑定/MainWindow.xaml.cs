using MVVM;
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

namespace MVVM绑定
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
            Content = new TextBoxBinDing();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //  Content = new EventBinding();
            EventBinding eventBinding = new EventBinding();
            eventBinding.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            ListBoxWindow listBoxWindow = new ListBoxWindow();
            listBoxWindow.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ProgressWindow progressWindow = new ProgressWindow();
            progressWindow.Show();
        }
    }
}
