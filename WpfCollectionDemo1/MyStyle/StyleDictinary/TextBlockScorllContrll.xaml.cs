using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MyStyle.StyleDictinary
{
    /// <summary>
    /// TextBlockScorllContrll.xaml 的交互逻辑
    /// </summary>
    public partial class TextBlockScorllContrll : UserControl
    {
        public TextBlockScorllContrll()
        {
            InitializeComponent();
        }

        Storyboard std = null;
        DoubleAnimation animation = null;
        int index, total;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (ShowType == MarqueeType.Up || ShowType == MarqueeType.Down)
            {
                std = (Storyboard)canvas.Resources["stdUp"];
                content.Width = canvas.ActualWidth;
                txtItem.TextWrapping = TextWrapping.Wrap;
            }
            if (ShowType == MarqueeType.Left || ShowType == MarqueeType.Right)
            {
                std = (Storyboard)canvas.Resources["stdLeft"];
                content.Height = canvas.ActualHeight;
            }


            animation = (DoubleAnimation)std.Children[0];
            std.Completed += (t, r) => changeItem();

            
        }

        public MarqueeType ShowType
        {
            get { return (MarqueeType)this.GetValue(ShowTypeProperty); }
            set { this.SetValue(ShowTypeProperty, value); }
        }
        public static readonly DependencyProperty ShowTypeProperty = DependencyProperty.Register("ShowType", typeof(MarqueeType), typeof(TextBlockScorllContrll), new PropertyMetadata(MarqueeType.Up));


        public double Speed
        {
            get { return (double)this.GetValue(SpeedProperty); }
            set { this.SetValue(SpeedProperty, value); }
        }
        public static readonly DependencyProperty SpeedProperty = DependencyProperty.Register("Speed", typeof(double), typeof(TextBlockScorllContrll), new PropertyMetadata(1.5));


        private ObservableCollection<string> itemsSource = new ObservableCollection<string>();
        public ObservableCollection<string> ItemsSource
        {
            get { return itemsSource; }
            set
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (std != null)
                    {
                        std.Stop();
                        txtItem.Text = "";
                        itemsSource = value;


                        if (itemsSource != null && itemsSource.Count > 0)
                        {
                            index = 0;
                            total = value.Count;
                            changeItem();
                        }
                    }
                }));
            }
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="value"></param>
        public void Add(string value)
        {
            if (itemsSource != null && !itemsSource.Contains(value))
            {
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    if (std != null)
                    {
                        std.Stop();
                        txtItem.Text = "";
                        this.itemsSource.Add(value);

                        if (itemsSource != null && itemsSource.Count > 0)
                        {
                            index = itemsSource.IndexOf(value);
                            total = itemsSource.Count;
                            changeItem();
                        }
                    }
                }));
            }
        }

        private void changeItem()
        {
            txtItem.Text = itemsSource[index].ToString();
            txtItem.UpdateLayout();
            double canvasWidth = canvas.ActualWidth;
            double canvasHeight = canvas.ActualHeight;
            double txtWidth = txtItem.ActualWidth;
            double txtHeight = txtItem.ActualHeight;


            if (ShowType == MarqueeType.Up)
            {
                animation.From = canvasHeight;
                animation.To = -txtHeight;
            }
            else if (ShowType == MarqueeType.Down)
            {
                animation.From = -txtHeight;
                animation.To = canvasHeight;
            }
            else if (ShowType == MarqueeType.Left)
            {
                animation.From = canvasWidth;
                animation.To = -txtWidth;
            }
            else if (ShowType == MarqueeType.Right)
            {
                animation.From = -txtWidth;
                animation.To = canvasWidth;
            }
            int time = 0;
            if (ShowType == MarqueeType.Up || ShowType == MarqueeType.Down)
            {
                time = (int)(txtHeight / canvasHeight * Speed);
            }
            if (ShowType == MarqueeType.Left || ShowType == MarqueeType.Right)
            {
                time = (int)(txtWidth / canvasWidth * Speed);
            }
            if (time < 2) time = 2;
            animation.Duration = new Duration(new TimeSpan(0, 0, time));


            index++;
            if (index == total) index = 0;


            if (std != null)
            {
                std.Begin();
            }
        }
    }

    public enum MarqueeType
    {
        Up,
        Down,
        Left,
        Right
    }
}
