using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace OpenWrite
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        DrawingAttributes drawingAttributes;
        public MainWindow()
        {
            InitializeComponent();

            drawingAttributes = new DrawingAttributes();

            inkCanvas.DefaultDrawingAttributes = drawingAttributes;
            //平滑处理
            drawingAttributes.FitToCurve = false;
            //随着压力增大而增大
            drawingAttributes.IgnorePressure = false;



            inkCanvas.EraserShape = new RectangleStylusShape(50, 70);
            //画笔大小
            drawingAttributes.Height = 5;
            drawingAttributes.Width = 5;

            inkCanvas.Gesture += InkCanvas_Gesture;
            disappearAnimation.Completed += DisappearAnimation_Completed;

            inkCanvas.ManipulationInertiaStarting += InkCanvas_ManipulationInertiaStarting;

            inkCanvas.StrokeCollected += InkCanvas_StrokeCollected1;

            inkCanvas.IsHitTestVisibleChanged += InkCanvas_IsHitTestVisibleChanged;

        }

        private void InkCanvas_IsHitTestVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParams)
        {

            return null;
        }

        private void InkCanvas_Gesture(object sender, InkCanvasGestureEventArgs e)
        {
            //需要设置识别手势

        }

        private void DisappearAnimation_Completed(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 选择 移动操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.Select;
        }

        //选择颜色
        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            drawingAttributes.Color = Colors.Red;
        }

        /// <summary>
        /// 选择画笔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            pen_popup.IsOpen = true;
            inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
        }

        private void Btn4_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
        }


        private Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();





        public TransformGroup scrops = new TransformGroup();

        private double _ScaleXSum = 0;
        private double _ScaleYSum = 0;
        private double _TranslationXSum = 0;
        private double _TranslationYSum = 0;

        private int sumIndex;



        private int OneSingleNum = 0;

        private int EasNum = 0;

        private int FiveNum = 0;

        private void InkCanvas_ManipulationInertiaStarting(object sender, System.Windows.Input.ManipulationInertiaStartingEventArgs e)
        {
            //e.TranslationBehavior = new InertiaTranslationBehavior()
            //{
            //    InitialVelocity = e.InitialVelocities.LinearVelocity,
            //    DesiredDeceleration = 1 / (1000.0 * 1000.0)   // 单位：一个WPF单位 / ms
            //};
        }

        int TounchIndexNum = 0;
        private Dictionary<int, int> DecTouchsDic = new Dictionary<int, int>();
        private void InkCanvas_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            //  tochtextbox.AppendText("InkCanvas_ManipulationStarted" + "\r\t");


        }
        private void InkCanvas_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
             {
                //   tochtextbox.AppendText("InkCanvas_ManipulationDelta" + "\r\t");
                TounchIndexNum++;

                 toch_Num.Text = DecTouchsDic.Count + "";

                //  TounchIndex.Text = TounchIndexNum + "";

                try
                 {

                     sumIndex++;

                    //if (_ScaleXSum > 200) return;

                    //if (_ScaleYSum > 200) return;

                    //if (_TranslationXSum > 500 || _TranslationXSum < -500) return;
                    //if (_TranslationYSum > 500 || _TranslationYSum < -500) return;

                    //if (e.DeltaManipulation.Scale.X == 1 || e.DeltaManipulation.Scale.X <= 0 || e.DeltaManipulation.Scale.Y == 1 || e.DeltaManipulation.Scale.Y <= 0) return;
                    //if (e.DeltaManipulation.Translation.X <= -100 || e.DeltaManipulation.Translation.Y >= 100) return;

                    //  tochtextbox.AppendText("e.DeltaManipulation.Scale.X =" + e.DeltaManipulation.Scale.X + "e.DeltaManipulation.Scale.Y =" + e.DeltaManipulation.Scale.Y + "\r\t");
                    //ScaleX.Text = e.DeltaManipulation.Scale.X + "";
                    //ScaleY.Text = e.DeltaManipulation.Scale.Y + "";


                    //_ScaleXSum = _ScaleXSum + e.DeltaManipulation.Scale.X;
                    //_ScaleYSum = _ScaleYSum + e.DeltaManipulation.Scale.Y;

                    //ScaleXSum.Text = _ScaleXSum + "";
                    //ScaleYSum.Text = _ScaleYSum + "";
                    //_ScaleXSum = e.CumulativeManipulation.Scale.X;
                    //_ScaleYSum = e.CumulativeManipulation.Scale.Y;
                    //ScaleXSum.Text = e.CumulativeManipulation.Scale.X + "";
                    //ScaleYSum.Text = e.CumulativeManipulation.Scale.Y + "";

                    //tochtextbox.AppendText("e.DeltaManipulation.Translation.X =" + e.DeltaManipulation.Translation.X + "e.Translation.Y =" + e.DeltaManipulation.Translation.Y + "\r\t");
                    //TranslationX.Text = e.DeltaManipulation.Translation.X + "";
                    //TranslationY.Text = e.DeltaManipulation.Translation.Y + "";

                    //_TranslationXSum = _TranslationXSum + e.DeltaManipulation.Translation.X;
                    //_TranslationYSum = _TranslationYSum + e.DeltaManipulation.Translation.Y;
                    //TranslationXSum.Text = _TranslationXSum + "";
                    //TranslationYSum.Text = _TranslationYSum + "";

                    //TranslationXSum.Text = e.CumulativeManipulation.Translation.X + "";
                    //TranslationYSum.Text = e.CumulativeManipulation.Translation.Y + "";

                    //_TranslationXSum = e.CumulativeManipulation.Translation.X;
                    //_TranslationYSum = e.CumulativeManipulation.Translation.Y;

                    //tochtextbox.AppendText(e.Device.ToString());

                    //if (sumIndex > 20)
                    //{
                    //    scrops.Children.Add(new ScaleTransform() { ScaleX = _ScaleXSum, ScaleY = _ScaleYSum });
                    //    scrops.Children.Add(new TranslateTransform() { X = _TranslationXSum, Y = _TranslationYSum });
                    //    inkCanvas.RenderTransform = scrops;
                    //}

                }
                 catch (Exception ee)
                 {
                     tochtextbox.AppendText("异常:" + ee.StackTrace + "\r\t");
                 }
             }));


            //缩放  拖动
            //foreach (Stroke stroke in inkCanvas.Strokes)
            //{
            //    Matrix matrix = new Matrix();
            //    matrix.ScaleAt(e.DeltaManipulation.Scale.X, e.DeltaManipulation.Scale.Y, centerPoint.X, centerPoint.Y);
            //    matrix.Translate(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y);
            //    stroke.Transform(matrix, false);
            //}




        }
        private void InkCanvas_StrokeCollected1(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
            {
                tochtextbox.AppendText("InkCanvas_StrokeCollected1" + "\r\t");
                inkCanvas.EditingMode = InkCanvasEditingMode.None;
            }));

        }
        private void InkCanvas_PreviewTouchUp(object sender, System.Windows.Input.TouchEventArgs e)
        {
            tochtextbox.AppendText("InkCanvas_PreviewTouchUp" + "\r\t");

        }
        private void InkCanvas_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
             {
                 tochtextbox.AppendText("InkCanvas_ManipulationCompleted" + "\r\t");

                 DecTouchsDic.Clear();
                 startEraseByPoin = false;
                 //GetDateTime = DateTime.Now;
                 //if (GetDateTime.Subtract(StartDateTime).TotalMilliseconds > 100)
                 //{
                 //    DecTouchsDic.Clear();
                 //    inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                 //}

                 _ScaleXSum = 0;
                 _ScaleYSum = 0;
                 //_TranslationXSum = 0;
                 //_TranslationYSum = 0;
                 sumIndex = 0;

                 OneSingleNum = 0;
                 EasNum = 0;
                 FiveNum = 0;
             }));
            toch_Num.Text = "";

        }
        //中心点
        Point centerPoint;
        private int LinkNum = 0;
        private bool LinkBool = false;
        private DateTime StartDateTime;
        private bool startEraseByPoin = false;
        private void InkCanvas_PreviewTouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new Action(() =>
            {

                //  tochtextbox.AppendText("InkCanvas_PreviewTouchDown" + "\r\t");
                int id = e.TouchDevice.Id;
                Size ret = e.GetTouchPoint(this).Size;

                if (DecTouchsDic.ContainsKey(id))
                {
                }
                else
                {
                    DecTouchsDic.Add(id, 1);
                }
                if (startEraseByPoin || (ret.Width != drawingAttributes.Width && ret.Height != drawingAttributes.Height && (ret.Width > 50 || ret.Height > 50)))
                {
                    inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
                    startEraseByPoin = true;
                }
                else if (DecTouchsDic.Count == 1)
                {
                    inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                }
                //else if (DecTouchsDic.Count >= 10)
                //{
                //    inkCanvas.EditingMode = InkCanvasEditingMode.None;
                //}
            }));

        }
        DoubleAnimation disappearAnimation = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(100));
        //记时间
        DateTime GetDateTime = new DateTime();


        private void InkCanvas_StrokeErased(object sender, RoutedEventArgs e)
        {
            //inkCanvas.EditingMode = InkCanvasEditingMode.None;
        }

        private void InkCanvas_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //inkCanvas.EditingMode = InkCanvasEditingMode.Ink;   //添加会出现橡皮檫过程中出现写状态
            //   tochtextbox.AppendText("InkCanvas_PreviewMouseLeftButtonDown" + "\r\t");
        }

        private void InkCanvas_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // tochtextbox.AppendText("InkCanvas_PreviewMouseLeftButtonUp" + "\r\t");
        }







        IncrementalStrokeHitTester eraseTester;
        // Prepare to collect stylus packets. Get the 
        // IncrementalHitTester from the InkPresenter's 
        // StrokeCollection and subscribe to its StrokeHitChanged event.
        protected override void OnStylusDown(StylusDownEventArgs e)
        {
            base.OnStylusDown(e);

            EllipseStylusShape eraserTip = new EllipseStylusShape(3, 3, 0);
            IncrementalStrokeHitTester eraseTester =
                inkCanvas.Strokes.GetIncrementalStrokeHitTester(eraserTip);
            eraseTester.StrokeHit += new StrokeHitEventHandler(eraseTester_StrokeHit);
            eraseTester.AddPoints(e.GetStylusPoints(this));

        }

        // Collect the StylusPackets as the stylus moves.
        protected override void OnStylusMove(StylusEventArgs e)
        {
            if (eraseTester.IsValid)
            {
                eraseTester.AddPoints(e.GetStylusPoints(this));
            }
        }

        // Unsubscribe from the StrokeHitChanged event when the
        // user lifts the stylus.
        protected override void OnStylusUp(StylusEventArgs e)
        {

            eraseTester.AddPoints(e.GetStylusPoints(this));
            eraseTester.StrokeHit -= new
                StrokeHitEventHandler(eraseTester_StrokeHit);
            eraseTester.EndHitTesting();
        }


        // When the stylus intersects a stroke, erase that part of
        // the stroke.  When the stylus dissects a stoke, the 
        // Stroke.Erase method returns a StrokeCollection that contains
        // the two new strokes.
        void eraseTester_StrokeHit(object sender,
            StrokeHitEventArgs args)
        {
            StrokeCollection eraseResult =
                args.GetPointEraseResults();
            StrokeCollection strokesToReplace = new StrokeCollection();
            strokesToReplace.Add(args.HitStroke);

            // Replace the old stroke with the new one.
            if (eraseResult.Count > 0)
            {
                inkCanvas.Strokes.Replace(strokesToReplace, eraseResult);
            }
            else
            {
                inkCanvas.Strokes.Remove(strokesToReplace);
            }


        }
    }
}
