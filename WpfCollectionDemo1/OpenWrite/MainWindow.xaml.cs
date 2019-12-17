using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
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
            drawingAttributes.FitToCurve = true;

            //随着压力增大而增大
            drawingAttributes.IgnorePressure = false;



            inkCanvas.EraserShape = new RectangleStylusShape(50, 70);
            //画笔大小
            drawingAttributes.Height = 5;
            drawingAttributes.Width = 5;

            //橡皮檫大小
            //  inkCanvas.EraserShape = new RectangleStylusShape(50, 50);

            inkCanvas.Gesture += InkCanvas_Gesture;
            disappearAnimation.Completed += DisappearAnimation_Completed;
        }

        private void InkCanvas_Gesture(object sender, InkCanvasGestureEventArgs e)
        {

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



        private Dictionary<int, int> DecTouchsDic = new Dictionary<int, int>();
        private void InkCanvas_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {

        }

        public TransformGroup scrops = new TransformGroup();

        private double _ScaleXSum = 0;
        private double _ScaleYSum = 0;
        private double _TranslationXSum = 0;
        private double _TranslationYSum = 0;

        private int sumIndex;



        private int OneSingleNum = 0;

        private int EasNum = 0;

        private int FiveNum = 0;

        int TounchIndexNum = 0;

        private void InkCanvas_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {

            TounchIndexNum++;

            Dispatcher.Invoke(() =>
            {
                toch_Num.Text = DecTouchsDic.Count + "";

                //  TounchIndex.Text = TounchIndexNum + "";
            });


            //缩放  拖动
            //foreach (Stroke stroke in inkCanvas.Strokes)
            //{
            //    Matrix matrix = new Matrix();
            //    matrix.ScaleAt(e.DeltaManipulation.Scale.X, e.DeltaManipulation.Scale.Y, centerPoint.X, centerPoint.Y);
            //    matrix.Translate(e.DeltaManipulation.Translation.X, e.DeltaManipulation.Translation.Y);
            //    stroke.Transform(matrix, false);
            //}


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
                ScaleX.Text = e.DeltaManipulation.Scale.X + "";
                ScaleY.Text = e.DeltaManipulation.Scale.Y + "";


                _ScaleXSum = _ScaleXSum + e.DeltaManipulation.Scale.X;
                _ScaleYSum = _ScaleYSum + e.DeltaManipulation.Scale.Y;

                ScaleXSum.Text = _ScaleXSum + "";
                ScaleYSum.Text = _ScaleYSum + "";
                _ScaleXSum = e.CumulativeManipulation.Scale.X;
                _ScaleYSum = e.CumulativeManipulation.Scale.Y;
                ScaleXSum.Text = e.CumulativeManipulation.Scale.X + "";
                ScaleYSum.Text = e.CumulativeManipulation.Scale.Y + "";

                tochtextbox.AppendText("e.DeltaManipulation.Translation.X =" + e.DeltaManipulation.Translation.X + "e.Translation.Y =" + e.DeltaManipulation.Translation.Y + "\r\t");
                TranslationX.Text = e.DeltaManipulation.Translation.X + "";
                TranslationY.Text = e.DeltaManipulation.Translation.Y + "";

                _TranslationXSum = _TranslationXSum + e.DeltaManipulation.Translation.X;
                _TranslationYSum = _TranslationYSum + e.DeltaManipulation.Translation.Y;
                TranslationXSum.Text = _TranslationXSum + "";
                TranslationYSum.Text = _TranslationYSum + "";

                TranslationXSum.Text = e.CumulativeManipulation.Translation.X + "";
                TranslationYSum.Text = e.CumulativeManipulation.Translation.Y + "";

                _TranslationXSum = e.CumulativeManipulation.Translation.X;
                _TranslationYSum = e.CumulativeManipulation.Translation.Y;

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

        }
        private void InkCanvas_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                // inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                DecTouchsDic.Clear();

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
            });
            toch_Num.Text = "";

        }
        //中心点
        Point centerPoint;
        private int LinkNum = 0;
        private bool LinkBool = false;
        private DateTime StartDateTime;
        private bool eas = false;
        private void InkCanvas_PreviewTouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                int id = e.TouchDevice.Id;
                Size ret = e.GetTouchPoint(this).Size;

                if (DecTouchsDic.ContainsKey(id))
                {
                }
                else
                {
                    DecTouchsDic.Add(id, 1);
                }
                if ((DecTouchsDic.Count > 1 && DecTouchsDic.Count < 5) || ((ret.Width != drawingAttributes.Width && ret.Height != drawingAttributes.Height) && (ret.Width > 100 || ret.Height > 100)))
                {
                    inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
                }
                else if (DecTouchsDic.Count == 1)
                {
                    inkCanvas.EditingMode = InkCanvasEditingMode.Ink;
                }

                else if (DecTouchsDic.Count >= 10)
                {
                    inkCanvas.EditingMode = InkCanvasEditingMode.None;
                }
            });
        }
        DoubleAnimation disappearAnimation = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(100));
        //记时间
        DateTime GetDateTime = new DateTime();
        private void InkCanvas_PreviewTouchUp(object sender, System.Windows.Input.TouchEventArgs e)
        {
        }

        private void InkCanvas_StrokeErased(object sender, RoutedEventArgs e)
        {
            //inkCanvas.EditingMode = InkCanvasEditingMode.None;
        }

        /// <summary>
        /// 画笔
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InkCanvas_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            //Rect rect = inkCanvas.Strokes.GetBounds();
            //toch_Witdh.Text = rect.Width + "";
            //toch_Heigth.Text = rect.Height + "";

            //if (rect.Width > 100 && rect.Height > 100)
            //{
            //    inkCanvas.EditingMode = InkCanvasEditingMode.EraseByPoint;
            //}

            //inkCanvas.EditingMode = InkCanvasEditingMode.None;
        }

        private void InkCanvas_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //inkCanvas.EditingMode = InkCanvasEditingMode.Ink;   //添加会出现橡皮檫过程中出现写状态
        }

        private void InkCanvas_PreviewMouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }
    }
}
