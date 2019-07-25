using System.Windows;
using 依赖属性使用.Model;

namespace 依赖属性使用
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
            Person person = new Person() { Name = "张三" };

            textBolock.DataContext = person;
        }
    }
}
