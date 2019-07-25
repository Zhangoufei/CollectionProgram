using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;

namespace WpfCollectionDemo1.mvvm的使用.UserControl
{
    /// <summary>
    /// WindowInkCanvas.xaml 的交互逻辑
    /// </summary>
    public partial class WindowInkCanvas : Window
    {
        public WindowInkCanvas()
        {
            InitializeComponent();

            drawingAttributes = new DrawingAttributes();
            //InkCanvas 通过 DefaultDrawingAttributes 属性来获取墨迹的各种设置，该属性的类型为 DrawingAttributes 型
            //更改inkCanvas属性
            inkCanvas.DefaultDrawingAttributes = drawingAttributes;
            inkCanvas.ActiveEditingModeChanged += (s, e) =>
            {

            };

            inkCanvas.DragEnter += (s, e) =>
            {

            };

            inkCanvas.DragOver += (s, e) =>
            {

            };

            inkCanvas.Drop += (s, e) =>
            {

            };

            inkCanvas.MouseDown += (s, e) =>
            {

            };



        }


        //InkCanvas 提供的最基本的功能是自由笔，我们这里称显示出来的图形为墨迹，而绘制出来的墨迹实际上是 Stroke 类型，所以我们可以用 Stroke 类型的 DrawingAttributes 属性进行墨迹的一些设置，例如颜色，粗细等等。
        DrawingAttributes drawingAttributes = null;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            drawingAttributes.Color = Colors.Red;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            drawingAttributes.Width = drawingAttributes.Width + 3;

            drawingAttributes.Height = drawingAttributes.Height + 3;
        }

        //不明显
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //drawingAttributes.StylusTip = StylusTip.Rectangle;

            drawingAttributes.StylusTip = StylusTip.Ellipse;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //更改为更圆润
            drawingAttributes.FitToCurve = true;
        }

        //设置 IsHighlighter 属性为 true ，则墨迹显示的时候看上去像是荧光笔
        //官方文档上说这样做会使 Stroke 变的透明一些，可以显示覆盖住的墨迹，但是我感觉不太像透明，大家可以自己试试，也欢迎纠正
        //  drawingAttributes.IsHighlighter = false;

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //按照微软官方文档上的说法，设置 IgnorePressure 属性为 true墨迹粗细会随压力的增大而增大
            //但同时也说了 XAML 一般不使用此属性，我也实在找不到如何利用鼠标来进行压力的变化
            //所以这里我想大家做个了解应该就可以了
            drawingAttributes.IgnorePressure = true;
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as RadioButton).Content.ToString() == "绘制墨迹")
            {
                //绘制墨迹
                inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
            }

            else if ((sender as RadioButton).Content.ToString() == "按点擦除")
            {
                //使用橡皮擦按点擦除墨迹
                inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            }

            else if ((sender as RadioButton).Content.ToString() == "按线擦除")
            {
                //使用橡皮擦按绘制的墨迹将某一笔墨迹擦除掉，注意是某一笔，如果多次绘制但是有交叉也是不可以的
                inkCanvas.EditingMode = InkCanvasEditingMode.EraseByStroke;
            }

            else if ((sender as RadioButton).Content.ToString() == "选中墨迹")
            {
                //选中某一笔墨迹，进行拖动和缩放以及按 Delete 键删除，注意是某一笔
                inkCanvas.EditingMode = InkCanvasEditingMode.Select;
            }

            else if ((sender as RadioButton).Content.ToString() == "停止操作")
            {
                //不做任何
                inkCanvas.EditingMode = InkCanvasEditingMode.None;
            }

        }
    }
}
