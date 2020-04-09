using MVVM绑定.ViewModel;
using System.Windows;

namespace MVVM绑定
{
    /// <summary>
    /// ProgressWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ProgressWindow : Window
    {

        ProgressWindowVm progressWindowVm;
        public ProgressWindow()
        {
            InitializeComponent();

            DataContext = progressWindowVm = new ProgressWindowVm();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            progressWindowVm.SetAddValue();
        }
    }
}
