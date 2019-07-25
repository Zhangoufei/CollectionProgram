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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf放大镜
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Init(); //初始化控件
           myGridContent.PreviewMouseMove += new MouseEventHandler(myGridContent_PreviewMouseMove);

            //为最外层的Grid添加鼠标移动事件
            VisualBrush vb = (VisualBrush)pathTwo.Fill;
            vb.Visual = myGd;   //指定加载时显示的放大区域
        }
        Grid myGrid = new Grid();
        Grid myGd = new Grid();
        Canvas canvasOne = new Canvas();
        Canvas canvasTwo = new Canvas() { Name = "myCanvas" };
        Path pathTwo = null;


        //初始化控件
        void Init()
        {
            #region 放大镜区域-Grid

            //线
            Line line = new Line()
            {
                X1 = 150,
                Y1 = 140,
                X2 = 300,
                Y2 = 250,
                StrokeThickness = 30,
                Stroke = new LinearGradientBrush()  //使用线性渐变绘制区域 
                {
                    StartPoint = new Point(0, 0),   //开始位置
                    EndPoint = new Point(0, 1),     //结束位置
                    GradientStops = new GradientStopCollection()    //渐变的颜色
                        {
                            new GradientStop(Colors.White,1),
                            new GradientStop(Colors.Black,0)
                        }
                }
            };



            //路径-1
            Path pathOne = new Path()
            {
                Fill = new SolidColorBrush(Colors.White),
                Width = 200,
                Height = 200,
                Data = new GeometryGroup()
                {
                    Children = new GeometryCollection()
                        {
                            new EllipseGeometry(new Point(100,100),100,100),     //从中心开始，画一个圆，半径是100
                            new EllipseGeometry(new Point(100,100),1,1)          //从中心开始，画一个小圆，半径是1
                        }
                }
            };



            //路径-2
            pathTwo = new Path()
            {
                Name = "myPath",
                Width = 200,
                Height = 200,
                Fill = new VisualBrush()
                {
                    Viewbox = new Rect(0, 0, 30, 30),
                    ViewboxUnits = BrushMappingMode.Absolute,
                    Viewport = new Rect(0, 0, 1, 1),
                    ViewportUnits = BrushMappingMode.RelativeToBoundingBox
                },
                Data = new GeometryGroup()
                {
                    Children = new GeometryCollection()
                        {
                            new EllipseGeometry(new Point(100,100),100,100),    //从中心开始，画一个圆，半径是100
                            new EllipseGeometry(new Point(100,100),1,1)         //从中心开始，画一个小圆，半径是1
                        }
                }
            };



            //圆-1
            Ellipse ellipseOne = new Ellipse()
            {
                Width = 200,
                Height = 200,
                StrokeThickness = 10,
                Stroke = new LinearGradientBrush()
                {
                    StartPoint = new Point(0, 0),
                    EndPoint = new Point(0, 1),
                    GradientStops = new GradientStopCollection()
                    {
                        new GradientStop(Colors.Black,0),
                        new GradientStop(Colors.White,1)
                    }
                }
            };



            //圆-2
            Ellipse ellipseTwo = new Ellipse()
            {
                Width = 200,
                Height = 200,
                StrokeThickness = 10,
                Stroke = new LinearGradientBrush()
                {
                    StartPoint = new Point(0, 0),
                    EndPoint = new Point(0, 1),
                    GradientStops = new GradientStopCollection()
                    {
                        new GradientStop(Colors.Black,1),
                        new GradientStop(Colors.White,0)
                    }
                }
            };



            myGrid.Children.Add(canvasOne);

            canvasOne.Children.Add(canvasTwo);

            canvasTwo.Children.Add(line);
            canvasTwo.Children.Add(pathOne);
            canvasTwo.Children.Add(pathTwo);
            canvasTwo.Children.Add(ellipseOne);
            canvasTwo.Children.Add(ellipseTwo);
            #endregion



            #region 需要放大的内容区域-myGd
            Button btn = new Button()
            {
                Margin = new Thickness(70, 73, 134, 0),
                Height = 28,
                Content = "Administrator",
                VerticalAlignment = VerticalAlignment.Top
            };


            Label lbl = new Label()
            {
                Margin = new Thickness(70, 39, 88, 0),
                Height = 28,
                Content = "Administrator",
                VerticalAlignment = VerticalAlignment.Top
            };



            myGd.Children.Add(btn);
            myGd.Children.Add(lbl);
            #endregion



            myGridContent.Children.Add(myGd);
            myGridContent.Children.Add(myGrid);

        }



        void myGridContent_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            VisualBrush vb = (VisualBrush)pathTwo.Fill;
            Point point = e.MouseDevice.GetPosition(myGd);
            Rect rc = vb.Viewbox;
            rc.X = point.X - rc.Width / 2;
            rc.Y = point.Y - rc.Height / 2;
            vb.Viewbox = rc;
            Canvas.SetLeft(canvasTwo, point.X - pathTwo.Width / 2);
            Canvas.SetTop(canvasTwo, point.Y - pathTwo.Height / 2);
        }
    }
}
