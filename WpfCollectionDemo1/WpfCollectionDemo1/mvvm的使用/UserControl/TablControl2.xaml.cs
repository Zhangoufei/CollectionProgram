using System.Windows;
using System.Windows.Controls;

namespace WpfCollectionDemo1.mvvm的使用.UserControl
{
    /// <summary>
    /// TablControl2.xaml 的交互逻辑
    /// </summary>
    public partial class TablControl2 : Page
    {
        public TablControl2()
        {
            InitializeComponent();
        }

        int sumIndex = 0;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            sumIndex++;
            if (sumIndex > 2)
            {
                sumIndex = 0;
            }
            tabContle1.SelectedIndex = sumIndex;

           // titleTabl.IsSelected = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            tabContle1.SelectedIndex = 0;
        }

        private void TabContle1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
