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

namespace MystyleUserControl.控件
{
    /// <summary>
    /// UserControl2Window.xaml 的交互逻辑
    /// </summary>
    public partial class UserControl2Window : Window
    {
        public UserControl2Window()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Descre_click(object sender, RoutedEventArgs e)
        {

        }

        private void Bold_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void Bold_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Italic_cheked(object sender, RoutedEventArgs e)
        {

        }

        private void Italic_unCheked(object sender, RoutedEventArgs e)
        {

        }

        private void BtnBtton0_Click(object sender, RoutedEventArgs e)
        {
            Duration duration = new Duration(TimeSpan.FromSeconds(10));
            DoubleAnimation doubleAnimation = new DoubleAnimation(100,duration);
            progessbar2.BeginAnimation(ProgressBar.ValueProperty, doubleAnimation);
        }

        private void Rslider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
