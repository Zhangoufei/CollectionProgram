using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MystyleUserControl.样式
{
    /// <summary>
    /// Style1Window.xaml 的交互逻辑
    /// </summary>
    public partial class Style1Window : Window
    {
        public Style1Window()
        {
            InitializeComponent();

            Loaded += Style1Window_Loaded;
        }

        private void Style1Window_Loaded(object sender, RoutedEventArgs e)
        {
            int count = wrappnel.Children.Count;

            for (int i = 0; i < count; i++)
            {
                Button btn = wrappnel.Children[i] as Button;

                RotateTransform rotateTransform = new RotateTransform();
                rotateTransform.Angle = 45;

                btn.RenderTransform = rotateTransform;
                btn.Background = new SolidColorBrush(Colors.Bisque);
                btn.Height = 50;
                btn.Margin = new Thickness(10);
            }
        }
    }
}
