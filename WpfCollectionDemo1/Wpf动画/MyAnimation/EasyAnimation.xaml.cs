using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Wpf动画.MyAnimation
{
    /// <summary>
    /// EasyAnimation.xaml 的交互逻辑
    /// </summary>
    public partial class EasyAnimation : Window
    {
        public EasyAnimation()
        {
            InitializeComponent();
        }


        private void UniformGrid_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            switch (button.Content)
            {
                case "宽度改变":
                    //使用故事板
                    Storyboard storyboard = new Storyboard();
                    DoubleAnimation doubleAnimation = new DoubleAnimation();
                    doubleAnimation.From = button.Width;
                    doubleAnimation.To = button.Width - 30;
                    doubleAnimation.Duration = TimeSpan.FromSeconds(4);
                    Storyboard.SetTarget(doubleAnimation, button);
                    Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath("Width"));
                    storyboard.Children.Add(doubleAnimation);
                    storyboard.Begin();
                    break;
                case "高度改变":
                    //不使用故事板
                    DoubleAnimation doubleAnimation2 = new DoubleAnimation(button.Height, button.Height - 30, TimeSpan.FromSeconds(5));
                    //前0.5  50%时间加速
                    doubleAnimation2.AccelerationRatio = 0.5;
                    //前0.2  20%时间减速
                    doubleAnimation2.DecelerationRatio = 0.2;
                    button.BeginAnimation(HeightProperty, doubleAnimation2);
                    break;
                case "高度宽度透明度旋转改变":
                    Storyboard storyboard3 = new Storyboard();
                    DoubleAnimation doubleAnimation3 = new DoubleAnimation(button.Height, button.Height - 30, TimeSpan.FromSeconds(5));
                    Storyboard.SetTarget(doubleAnimation3, button);
                    Storyboard.SetTargetProperty(doubleAnimation3, new PropertyPath("Height"));
                    storyboard3.Children.Add(doubleAnimation3);
                    DoubleAnimation doubleAnimation4 = new DoubleAnimation(button.Width, button.Width - 30, TimeSpan.FromSeconds(5));
                    Storyboard.SetTarget(doubleAnimation4, button);
                    Storyboard.SetTargetProperty(doubleAnimation4, new PropertyPath("Width"));
                    storyboard3.Children.Add(doubleAnimation4);
                    DoubleAnimation doubleAnimation5 = new DoubleAnimation(1, 0.2, TimeSpan.FromSeconds(5));
                    Storyboard.SetTarget(doubleAnimation5, button);
                    Storyboard.SetTargetProperty(doubleAnimation5, new PropertyPath("Opacity"));
                    storyboard3.Children.Add(doubleAnimation5);

                    //旋转
                    RotateTransform rtf = new RotateTransform();
                    button.RenderTransformOrigin = new Point(0.5, 0.5);
                    button.RenderTransform = rtf;  //赋值给动画
                    DoubleAnimation doubleAnimation6 = new DoubleAnimation(0, 160, TimeSpan.FromSeconds(5));
                    storyboard3.Children.Add(doubleAnimation6);
                    Storyboard.SetTarget(doubleAnimation6, button);
                    Storyboard.SetTargetProperty(doubleAnimation6, new PropertyPath("RenderTransform.Angle"));
                    storyboard3.Begin();
                    break;
                case "关键帧动画":
                    //线性动画只能一个属性 两种状态改变，关键帧动画可以，一个属性多个状态改变
                    DoubleAnimationUsingKeyFrames doubleAnimationUsingKeyFrames = new DoubleAnimationUsingKeyFrames();
                    var keyFrames = doubleAnimationUsingKeyFrames.KeyFrames;

                    keyFrames.Add(new LinearDoubleKeyFrame(0, TimeSpan.FromSeconds(0)));
                    keyFrames.Add(new LinearDoubleKeyFrame(350, TimeSpan.FromSeconds(2)));
                    keyFrames.Add(new LinearDoubleKeyFrame(100, TimeSpan.FromSeconds(4)));
                    keyFrames.Add(new LinearDoubleKeyFrame(200, TimeSpan.FromSeconds(6)));
                    button.BeginAnimation(WidthProperty, doubleAnimationUsingKeyFrames);

                    break;

            }
        }

    }
}
