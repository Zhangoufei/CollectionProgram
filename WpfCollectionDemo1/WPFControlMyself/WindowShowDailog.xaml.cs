using CommonCtrls;
using System.Windows;

namespace WPFControlMyself
{
    /// <summary>
    /// WindowShowDailog.xaml 的交互逻辑
    /// </summary>
    public partial class WindowShowDailog : Window
    {
        public WindowShowDailog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var result = ICMessageBox.Show("你确定要删除当前画板吗 ?", "提示", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {

            }
        }
    }
}
