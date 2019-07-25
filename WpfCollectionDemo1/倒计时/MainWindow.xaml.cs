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
using Walterlv.Demo.Interop;

namespace 倒计时
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

           WindowBlur.SetIsEnabled(this, true);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProcessWindow processWindow = new ProcessWindow();
            processWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            processWindow.grid.Children.Add(new UserTimeStyle1());
            processWindow.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ProcessWindow processWindow = new ProcessWindow();
            processWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        
            processWindow.grid.Children.Add(new MyUserControl());
            processWindow.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            Content = new MyTimerDemo3();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            ProcessWindow processWindow = new ProcessWindow();
            processWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            processWindow.grid.Children.Add(new UserControlTimer());
            processWindow.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            WindowBlurTest windowBlurTest = new WindowBlurTest();
            windowBlurTest.Show();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            WindowBlureTest2 windowBlureTest2 = new WindowBlureTest2();
            windowBlureTest2.Show();
        }
    }
}
