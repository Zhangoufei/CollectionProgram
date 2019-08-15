using CommonCtrls;
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

namespace WpfCollectionDemo1
{
    /// <summary>
    /// ShowDialogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ShowDialogWindow : Window
    {
        public ShowDialogWindow()
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
