using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WpfCollectionDemo1.mvvm的使用.ViewModel;

namespace WpfCollectionDemo1
{
    /// <summary>
    /// EnlargementShrink.xaml 的交互逻辑
    /// </summary>
    public partial class EnlargementShrink : Window
    {

        private EnlargementShrinkVM enlargementShrinkVM = null;

        public EnlargementShrink()
        {
            InitializeComponent();

            inkCanvas.EraserShape = new RectangleStylusShape(50, 70);





            DataContext = enlargementShrinkVM = new EnlargementShrinkVM();

            this.Loaded += EnlargementShrink_Loaded;
        }

        private void EnlargementShrink_Loaded(object sender, RoutedEventArgs e)
        {
            inkCanvas.StrokeCollected += InkCanvas_StrokeCollected;

            eaPreWidthHeight.Text = VisualScalingExtensions.GetScalingRatioToDevice(grid).Width + " 高:" + VisualScalingExtensions.GetScalingRatioToDevice(grid).Height;
        }

        private void InkCanvas_StrokeCollected(object sender, System.Windows.Controls.InkCanvasStrokeCollectedEventArgs e)
        {
            var tempValue = enlargementShrinkVM.phoneImageShows.FirstOrDefault(p => p.BitMapImage == enlargementShrinkVM.BitMapImage);

            tempValue.StrokesVm = inkCanvas.Strokes;
        }


        private TransformGroup group = new TransformGroup();


        bool IsLeftMouseDown = false;

        bool EntryTouch = false;

        Thread th = null;
        private double ScaleNum = 0;
        Matrix scaleBig = Matrix.Identity;
        /// <summary>
        /// 放大
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Content.ToString() == "矩阵变换放大")
            {
                ScaleNum += 0.5;
                scaleBig.Scale(ScaleNum, ScaleNum);
                grid.RenderTransform = new MatrixTransform(scaleBig);
            }
            else
            {
                ScaleNum += 0.4;
                ScaleTransform scaleTransform = new ScaleTransform();
                SetEraserShape(ScaleNum);
                scaleTransform.ScaleX += 0.4;
                scaleTransform.ScaleY += 0.4;

                group.Children.Add(scaleTransform);
                grid.RenderTransform = group;
            }

            eaWidth.Text = VisualScalingExtensions.GetScalingRatioToDevice(inkCanvas).Width + "";
            eaHeight.Text = VisualScalingExtensions.GetScalingRatioToDevice(inkCanvas).Height + "";
        }

        //缩小
        Matrix scaleSmall = Matrix.Identity;
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (button.Content.ToString() == "矩阵变换缩小")
            {
                ScaleNum -= 1.5;
                SetEraserShape(ScaleNum);
                scaleSmall.Scale(ScaleNum, ScaleNum);
                grid.RenderTransform = new MatrixTransform(scaleSmall * scaleBig);
                //testImage.RenderTransform = new MatrixTransform(scale);
            }
            else
            {
                //if (ScaleNum <= 0)
                //{
                //    return;
                //}
                ScaleTransform scaleTransform = new ScaleTransform();
                ScaleNum -= 0.2;
                SetEraserShape(ScaleNum);
                scaleTransform.ScaleX -= 0.4;
                scaleTransform.ScaleY -= 0.4;

                group.Children.Add(scaleTransform);
                inkCanvas.RenderTransform = group;
                testImage.RenderTransform = group;

            }

        }
        /// <summary>
        /// 根据放大倍数缩小橡皮檫
        /// </summary>
        /// <param name="ScaleNum"></param>
        /// <returns></returns>
        /// <summary>
        /// 根据放大倍数缩小橡皮檫
        /// </summary>
        /// <param name="ScaleNum"></param>
        /// <returns></returns>
        public bool SetEraserShape(double ScaleNum)
        {
            double x = Math.Round(70 / 100d, 2);

            double sumStep = 1;
            double step = 0.4;

            if (ScaleNum >= step && ScaleNum < sumStep + step)
            {
                inkCanvas.EraserShape = new RectangleStylusShape(60, Math.Round(60 / x, 2));
            }
            else if (sumStep + step >= 1.2 && ScaleNum < sumStep + step + 1)
            {
                inkCanvas.EraserShape = new RectangleStylusShape(40, Math.Round(40 / x, 2));
            }
            else if (ScaleNum >= sumStep + step + 1 && ScaleNum < sumStep + step + 2)
            {
                inkCanvas.EraserShape = new RectangleStylusShape(30, Math.Round(30 / x, 2));
            }
            else if (ScaleNum >= sumStep + step + 2 && ScaleNum < sumStep + step + 3)
            {
                inkCanvas.EraserShape = new RectangleStylusShape(10, Math.Round(10 / x, 2));
            }
            else if (ScaleNum >= sumStep + step + 4)
            {
                inkCanvas.EraserShape = new RectangleStylusShape(10, Math.Round(10 / x, 2));
            }
            return true;
        }

        private void Button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            IsLeftMouseDown = true;

            if (th != null)
            {
                if (th.ThreadState == ThreadState.Running || th.ThreadState == ThreadState.WaitSleepJoin)
                    th.Abort();
            }
            th = new Thread(new ThreadStart(() =>
            {
                Thread.Sleep(1500);

                if (IsLeftMouseDown)
                {
                    EntryTouch = true;

                    MessageBox.Show("长按了1.5秒");
                    IsLeftMouseDown = false;
                }
                else
                {
                    MessageBox.Show("1.5秒内释放了点击");
                }

                EntryTouch = false;

            }));
            th.Start();
        }

        private void Button_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (EntryTouch)
            {
                MessageBox.Show("已进入长按事件");
            }

            IsLeftMouseDown = false;
        }



        private int EnlargmentNum = 0;
        /// <summary>
        /// 翻转对象1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Storyboard sbd = (Storyboard)this.FindResource("Storyboard90");
            //var temp = (EasingDoubleKeyFrame)Resources["myEasingKey"];
            //angle += 90;
            //temp.Value = angle;
            //sbd.Begin();

            //ScaleNum -= 0.01;
            //scaleTransform.ScaleX = ScaleNum;
            //scaleTransform.ScaleY = ScaleNum;
            //group.Children.Add(scaleTransform);
            //inkCanvas.RenderTransform = group;

            RotateTransform rotateTransform = new RotateTransform(90);
            group.Children.Add(rotateTransform);


            inkCanvas.RenderTransform = group;
            testImage.RenderTransform = group;

        }
        //翻转对象
        private RotateTransform rotateTransform = new RotateTransform();
        /// <summary>
        /// 翻转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //rotateTransform.Angle += 90;

            //group.Children.Add(rotateTransform);

            //testImage.RenderTransform = group;
        }

        private int ScaleX = 1, ScaleY = 1;

        private Point m_PreviousMousePoint;


        Dictionary<string, ModelDic> keyValuePairs = new Dictionary<string, ModelDic>();

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var temp = new BitmapImage(new Uri("/resource/img_home_background.png", UriKind.Relative));
            testImage.Source = temp;

        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var temp = new BitmapImage(new Uri("/resource/微信图片_20191205083957.jpg", UriKind.Relative));
            testImage.Source = temp;

        }

        private void ListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            PhoneImageShow imageShow = (sender as ListBox).SelectedValue as PhoneImageShow;

            enlargementShrinkVM.BitMapImage = imageShow.BitMapImage;
            if (imageShow.StrokesVm == null)
            {
                inkCanvas.Strokes = new StrokeCollection();
                return;
            }

            inkCanvas.Strokes = imageShow.StrokesVm;


        }

        private void InkCanvas_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {


        }
        ScaleTransform scaleTransform = new ScaleTransform();
        private bool move;
        /// <summary>
        /// 移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            move = true;
            inkCanvas.EditingMode = InkCanvasEditingMode.Select;
        }

        private void InkCanvas_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            //var element = e.Source as FrameworkElement;
            //if (e.IsInertial)
            //{
            //    Rect containingRect = new Rect(((FrameworkElement)e.ManipulationContainer).RenderSize);

            //    Rect shapeBounds = element.RenderTransform.TransformBounds(new Rect(element.RenderSize));
            //    if (e.IsInertial && !containingRect.Contains(shapeBounds))
            //    {
            //        e.ReportBoundaryFeedback(e.DeltaManipulation);
            //        e.Complete();
            //    }
            //}

            if (move)
            {
                //group.Children.Add(new TranslateTransform() { X = e.DeltaManipulation.Translation.X, Y = e.DeltaManipulation.Translation.Y });
                //grid.RenderTransform = group;

                //Matrix matrix = new Matrix();
                //matrix.Translate(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y);
                //foreach (Stroke stroke in inkCanvas.Strokes)
                //{
                //    stroke.Transform(matrix, false);
                //}
                //testImage.RenderTransform = new MatrixTransform(matrix);

                //MatrixAnimationUsingKeyFrames anim = new MatrixAnimationUsingKeyFrames();
                //int duration = 1;
                //anim.KeyFrames = Interpolate(new Point(0.5, 0.5), new Point(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y), 1, 1, 100, duration);
                //grid.BeginAnimation(MatrixTransform.MatrixProperty, anim, HandoffBehavior.Compose);

                double x = e.DeltaManipulation.Translation.X;
                double y = e.DeltaManipulation.Translation.Y;
                //if (x < 0 || x > 0) return;
                scrops.Children.Add(new TranslateTransform() { X = x, Y = y });
                grid.RenderTransform = scrops;

            }
        }
        public TransformGroup scrops = new TransformGroup();
        public MatrixKeyFrameCollection Interpolate(Point startPoint, Point endPoint, double startScale, double endScale, double framerate, double duration)
        {
            MatrixKeyFrameCollection keyframes = new MatrixKeyFrameCollection();

            double steps = duration * framerate;
            double milliSeconds = 1000 / framerate;
            double timeCounter = 0;



            double diffX = Math.Abs(startPoint.X - endPoint.X);
            double xStep = diffX / steps;

            double diffY = Math.Abs(startPoint.Y - endPoint.Y);
            double yStep = diffY / steps;

            double diffScale = Math.Abs(startScale - endScale);
            double scaleStep = diffScale / steps;


            if (endPoint.Y < startPoint.Y)
            {
                yStep = -yStep;
            }

            if (endPoint.X < startPoint.X)
            {
                xStep = -xStep;
            }


            if (endScale < startScale)
            {
                scaleStep = -scaleStep;
            }


            Point currentPoint = new Point();
            double currentScale = startScale;

            for (int i = 0; i < steps; i++)
            {
                keyframes.Add(new DiscreteMatrixKeyFrame(new Matrix(currentScale, 0, 0, currentScale, currentPoint.X, currentPoint.Y), KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(timeCounter))));
                currentPoint.X += xStep;
                currentPoint.Y += yStep;
                currentScale += scaleStep;
                timeCounter += milliSeconds;

            }

            keyframes.Add(new DiscreteMatrixKeyFrame(new Matrix(endScale, 0, 0, endScale, endPoint.X, endPoint.Y), KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))));

            return keyframes;

        }


        private void InkCanvas_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {

        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            move = false;
            inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void InkCanvas_ManipulationInertiaStarting(object sender, System.Windows.Input.ManipulationInertiaStartingEventArgs e)
        {
            // 移动惯性
            //e.TranslationBehavior = new InertiaTranslationBehavior()
            //{
            //    InitialVelocity = e.InitialVelocities.LinearVelocity,
            //    DesiredDeceleration = 1 / (1000.0 * 1000.0)   // 单位：一个WPF单位 / ms
            //};
        }



        private int sum = 0;
        private void Image_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            sum++;
            textTochLeft.Text = "左键触发：" + sum;
        }
        private int sumRight = 0;
        /// <summary>
        /// 长按事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            sumRight++;
            textTochRight.Text = "右键触发" + sumRight + "";
        }

        private DispatcherTimer dispatcherTimer;
        private void Image_TouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {

            if (dispatcherTimer == null)
            {
                dispatcherTimer = new DispatcherTimer();
                dispatcherTimer.Tick += DispatcherTimer_Tick;
                dispatcherTimer.Interval = TimeSpan.FromMilliseconds(500);
            }
            dispatcherTimer.Start();
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX += 0.01;
            scaleTransform.ScaleY += 0.01;

            group.Children.Add(scaleTransform);
            inkCanvas.RenderTransform = group;
            testImage.RenderTransform = group;
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
        }

        private void TestImage_MouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void Grid_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Point point = e.MouseDevice.GetPosition(this);

            //.Translation.X;
            //double y = e.DeltaManipulation.Translation.Y;
            ////if (x < 0 || x > 0) return;
            scrops.Children.Add(new TranslateTransform() { X = 0, Y = point.Y });
            testImage.RenderTransform = scrops;
        }

        private void InkCanvas_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }




    }

    public class ModelDic
    {
        public BitmapImage bitmapImage { set; get; }

        public StrokeCollection stroke { set; get; }


    }

    public class LinearMatrixAnimation : AnimationTimeline
    {

        public Matrix? From
        {
            set { SetValue(FromProperty, value); }
            get { return (Matrix)GetValue(FromProperty); }
        }
        public static DependencyProperty FromProperty = DependencyProperty.Register("From", typeof(Matrix?), typeof(LinearMatrixAnimation), new PropertyMetadata(null));

        public Matrix? To
        {
            set { SetValue(ToProperty, value); }
            get { return (Matrix)GetValue(ToProperty); }
        }
        public static DependencyProperty ToProperty = DependencyProperty.Register("To", typeof(Matrix?), typeof(LinearMatrixAnimation), new PropertyMetadata(null));

        public LinearMatrixAnimation()
        {
        }

        public LinearMatrixAnimation(Matrix from, Matrix to, Duration duration)
        {
            Duration = duration;
            From = from;
            To = to;
        }

        public override object GetCurrentValue(object defaultOriginValue, object defaultDestinationValue, AnimationClock animationClock)
        {
            if (animationClock.CurrentProgress == null)
            {
                return null;
            }

            double progress = animationClock.CurrentProgress.Value;
            Matrix from = From ?? (Matrix)defaultOriginValue;

            if (To.HasValue)
            {
                Matrix to = To.Value;
                Matrix newMatrix = new Matrix(((to.M11 - from.M11) * progress) + from.M11, 0, 0, ((to.M22 - from.M22) * progress) + from.M22,
                        ((to.OffsetX - from.OffsetX) * progress) + from.OffsetX, ((to.OffsetY - from.OffsetY) * progress) + from.OffsetY);
                return newMatrix;
            }

            return Matrix.Identity;
        }

        protected override System.Windows.Freezable CreateInstanceCore()
        {
            return new LinearMatrixAnimation();
        }

        public override System.Type TargetPropertyType
        {
            get { return typeof(Matrix); }
        }
    }


    public static class VisualScalingExtensions
    {
        /// <summary>
        /// 获取一个 <paramref name="visual"/> 在显示设备上的尺寸相对于自身尺寸的缩放比。
        /// </summary>
        public static Size GetScalingRatioToDevice(this Visual visual)
        {
            return visual.GetTransformInfoToDevice().Item1;
        }

        /// <summary>
        /// 获取一个 <paramref name="visual"/> 在显示设备上的尺寸相对于自身尺寸的缩放比和旋转角度（顺时针为正角度）。
        /// </summary>
        public static Tuple<Size, double> GetTransformInfoToDevice(this Visual visual)
        {
            if (visual == null) throw new ArgumentNullException(nameof(visual));

            // 计算此 Visual 在 WPF 窗口内部的缩放（含 ScaleTransform 等）。
            var root = VisualRoot(visual);
            var transform = ((MatrixTransform)visual.TransformToAncestor(root)).Value;
            // 计算此 WPF 窗口相比于设备的外部缩放（含 DPI 缩放等）。
            var ct = PresentationSource.FromVisual(visual)?.CompositionTarget;
            if (ct != null)
            {
                transform.Append(ct.TransformToDevice);
            }
            // 如果元素有旋转，则计算旋转分量。
            var unitVector = new Vector(1, 0);
            var vector = transform.Transform(unitVector);
            var angle = Vector.AngleBetween(unitVector, vector);
            transform.Rotate(-angle);
            // 计算考虑了旋转的综合缩放比。
            var rect = new Rect(new Size(1, 1));
            rect.Transform(transform);

            return Tuple.Create<Size, double>(rect.Size, angle);
        }

        /// <summary>
        /// 寻找一个 <see cref="Visual"/> 连接着的视觉树的根。
        /// 通常，如果这个 <see cref="Visual"/> 显示在窗口中，则根为 <see cref="Window"/>；
        /// 不过，如果此 <see cref="Visual"/> 没有显示出来，则根为某一个包含它的 <see cref="Visual"/>。
        /// 如果此 <see cref="Visual"/> 未连接到任何其它 <see cref="Visual"/>，则根为它自身。
        /// </summary>
        private static Visual VisualRoot(Visual visual)
        {
            if (visual == null) throw new ArgumentNullException(nameof(visual));

            var root = visual;
            var parent = VisualTreeHelper.GetParent(visual);
            while (parent != null)
            {
                if (parent is Visual r)
                {
                    root = r;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }
            return root;
        }
    }

}
