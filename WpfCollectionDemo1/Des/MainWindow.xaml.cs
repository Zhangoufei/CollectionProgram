using Des.design;
using System.Windows;

namespace Des
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
            Design design = new Design();

            DesignPro desig = new DesignPro(design);
            desig.Send("这是代理模式-静态代理模式");

        }
    }
}
