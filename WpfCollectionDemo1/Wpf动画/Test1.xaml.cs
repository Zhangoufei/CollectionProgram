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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Wpf动画
{
    /// <summary>
    /// Test1.xaml 的交互逻辑
    /// </summary>
    public partial class Test1 : Window
    {
        public Test1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();
            //doubleAnimation.From = 160;   //可以去掉这个
            //doubleAnimation.From = button1.ActualWidth;   //和不设置初始值一样效果
            doubleAnimation.To = button1.Width- 30;
            doubleAnimation.Duration = TimeSpan.FromSeconds(5);
            button1.BeginAnimation(Button.WidthProperty,doubleAnimation);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //DoubleAnimation doubleAnimation = new DoubleAnimation();
            //doubleAnimation.By = 10;
            //doubleAnimation.Duration = TimeSpan.FromSeconds(0.5);
            //button2.BeginAnimation(Button.WidthProperty, doubleAnimation);


            ObjectAnimationUsingKeyFrames animate = new ObjectAnimationUsingKeyFrames();
            animate.Duration = new TimeSpan(0, 0, 5);
         //   animate.RepeatBehavior = RepeatBehavior.Forever;
            DiscreteObjectKeyFrame kf1 = new DiscreteObjectKeyFrame(Visibility.Visible, new TimeSpan(0, 0, 1));
            DiscreteObjectKeyFrame kf2 = new DiscreteObjectKeyFrame(Visibility.Hidden, new TimeSpan(0, 0, 2));
            DiscreteObjectKeyFrame kf3 = new DiscreteObjectKeyFrame(Visibility.Collapsed, new TimeSpan(0, 0, 3));
         //   animate.KeyFrames.Add(kf1);
            animate.KeyFrames.Add(kf2);
         //   animate.KeyFrames.Add(kf3);
            button2.BeginAnimation(Button.VisibilityProperty, animate);
        }
    }
}
