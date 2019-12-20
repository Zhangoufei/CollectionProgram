using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfCollectionDemo1
{
    /// <summary>
    /// WindowBigEnlarg.xaml 的交互逻辑
    /// </summary>
    public partial class WindowBigEnlarg : Window
    {
        public WindowBigEnlarg()
        {
            InitializeComponent();

            this.Loaded += WindowBigEnlarg_Loaded;
        }

        private void WindowBigEnlarg_Loaded(object sender, RoutedEventArgs e)
        {
            inkcanvas.EditingMode = InkCanvasEditingMode.InkAndGesture;

            inkcanvas.Gesture += Inkcanvas_Gesture;

            inkcanvas.SetEnabledGestures(new ApplicationGesture[]
                    {ApplicationGesture.Down,
                     ApplicationGesture.ArrowDown,
                      ApplicationGesture.ScratchOut,
                     ApplicationGesture.Circle});

        }

        int numtemp = 0;
        private void Inkcanvas_Gesture(object sender, InkCanvasGestureEventArgs e)
        {
            testVIew.Text = "开始识别" + numtemp;
            ReadOnlyCollection<GestureRecognitionResult> gestureResults =
       e.GetGestureRecognitionResults();
            testVIew.Text = "识别" + gestureResults;
            // Check the first recognition result for a gesture.
            if (gestureResults[0].RecognitionConfidence ==
                RecognitionConfidence.Strong)
            {
                testVIew.Text = "识别" + gestureResults[0].RecognitionConfidence;
                switch (gestureResults[0].ApplicationGesture)
                {
                    case ApplicationGesture.Down:
                        // Do something.
                        testVIew.Text = " ApplicationGesture.Down" ;
                        break;
                    case ApplicationGesture.ArrowDown:
                        testVIew.Text = " ApplicationGesture.ArrowDown";
                        // Do something.
                        break;
                    case ApplicationGesture.Circle:
                        testVIew.Text = " ApplicationGesture.Circle";
                        // Do something.
                        break;
                    case ApplicationGesture.ScratchOut:
                        //inkcanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
                        // Do something.
                        numtemp++;
                        testVIew.Text = "识别到了" + numtemp;
                        break;
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ZoomIn(canvas);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ZoomOut(canvas);
        }

        /// <summary>
        /// 图片放大
        /// </summary>
        /// <param name="img">被操作的前台Image控件</param>
        public void ZoomIn(Grid img)
        {
            TransformGroup tg = img.RenderTransform as TransformGroup;
            var tgnew = tg.CloneCurrentValue();
            if (tgnew != null)
            {
                ScaleTransform st = tgnew.Children[1] as ScaleTransform;
                img.RenderTransformOrigin = new Point(0.5, 0.5);
                if (st.ScaleX > 0 && st.ScaleX <= 2.0)
                {
                    st.ScaleX += 0.5;
                    st.ScaleY += 0.5;
                }
                //else if (st.ScaleX < 0 && st.ScaleX >= -2.0)
                //{
                //    st.ScaleX -= 0.5;
                //    st.ScaleY += 0.5;
                //}
            }

            // 重新给图像赋值Transform变换属性
            img.RenderTransform = tgnew;
        }

        /// <summary>
        /// 图片缩小
        /// </summary>
        /// <param name="img">被操作的前台Image控件</param>
        public void ZoomOut(Grid img)
        {
            TransformGroup tg = img.RenderTransform as TransformGroup;
            var tgnew = tg.CloneCurrentValue();
            if (tgnew != null)
            {
                ScaleTransform st = tgnew.Children[1] as ScaleTransform;
                img.RenderTransformOrigin = new Point(0.5, 0.5);
                if (st.ScaleX >= 1)
                {
                    st.ScaleX -= 0.5;
                    st.ScaleY -= 0.5;
                }
                else if (st.ScaleX <= 0.5)
                {
                    st.ScaleX = 0.5;
                    st.ScaleY = 0.5;
                }
            }
            // 重新给图像赋值Transform变换属性
            img.RenderTransform = tgnew;
        }

        /// <summary>
        /// 图片左转
        /// </summary>
        /// <param name="img">被操作的前台Image控件</param>
        public void RotateLeft(Grid img)
        {
            TransformGroup tg = img.RenderTransform as TransformGroup;
            var tgnew = tg.CloneCurrentValue();
            if (tgnew != null)
            {
                RotateTransform rt = tgnew.Children[2] as RotateTransform;
                img.RenderTransformOrigin = new Point(0.5, 0.5);
                rt.Angle -= 5;
            }

            // 重新给图像赋值Transform变换属性
            img.RenderTransform = tgnew;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            inkcanvas.EditingMode = InkCanvasEditingMode.None;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && sender == movingObject)
            {
                EndPosition = e.GetPosition(canvas);

                TransformGroup tg = canvas.RenderTransform as TransformGroup;
                var tgnew = tg.CloneCurrentValue();
                if (tgnew != null)
                {
                    TranslateTransform tt = tgnew.Children[0] as TranslateTransform;

                    var X = EndPosition.X - StartPosition.X;
                    var Y = EndPosition.Y - StartPosition.Y;
                    tt.X += X;
                    tt.Y += Y;
                }

                // 重新给图像赋值Transform变换属性
                canvas.RenderTransform = tgnew;
            }

        }


        private Grid movingObject;  // 记录当前被拖拽移动的图片

        private Point StartPosition; // 本次移动开始时的坐标点位置

        private Point EndPosition;   // 本次移动结束时的坐标点位置

        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StartPosition = e.GetPosition(canvas);

            movingObject = canvas;
        }

        private void Canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            movingObject = null;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            inkcanvas.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            inkcanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            inkcanvas.EraserShape = new RectangleStylusShape(70, 100);
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            ZoomIn(canvas);
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            RotateLeft(canvas);
        }

        private void Inkcanvas_StrokeErasing(object sender, InkCanvasStrokeErasingEventArgs e)
        {

        }
    }
}
