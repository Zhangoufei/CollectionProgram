using MyStyle.pages;
using MyStyle.Styles;
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
using 倒计时;

namespace MyStyle
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
            Content = new OtherButtonPage();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Content = new ButtonPage();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Content = new TextBoxPage();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Content = new ScrollViewerPage();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Content = new ListBoxPage();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            ProcessWindow processWindow = new ProcessWindow();
            processWindow.grid.Children.Add(new UserControlTimer());
            processWindow.Show();
        }
    }
}
