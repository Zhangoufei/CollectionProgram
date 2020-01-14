using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace OpenWrite
{
    /// <summary>
    /// WindowEraserShap.xaml 的交互逻辑
    /// </summary>
    public partial class WindowEraserShap : Window
    {
        public WindowEraserShap()
        {
            InitializeComponent();

            rectangle.Height = 100;
            rectangle.Width = 100;
            rectangle.Fill = new SolidColorBrush(Colors.Aqua);

            testInkCanvase.Children.Add(rectangle);

          
        }

        Rectangle rectangle = new Rectangle();
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            testInkCanvase.pointStyle = PointStyle.None;
            Button button = sender as Button;
            switch (button.Content)
            {
                case "画笔":
                    testInkCanvase.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "橡皮檫":
                    testInkCanvase.EditingMode = InkCanvasEditingMode.EraseByPoint;
                    break;
                case "画直线":
                    testInkCanvase.pointStyle = PointStyle.StarghtLine;
                    break;
                case "画虚线":
                    testInkCanvase.pointStyle = PointStyle.ImaginaryLine;
                    break;
                case "画毛笔":
                    testInkCanvase.pointStyle = PointStyle.ImageLineMao;
                    break;

                default:
                    break;
            }
        }

        private void TestInkCanvase_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Point point = e.GetPosition(testInkCanvase);
            InkCanvas.SetLeft(rectangle, point.X);
            InkCanvas.SetTop(rectangle, point.Y);
        }
    }
}
