using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Blend
{
    /// <summary>
    /// WindowBehaviiors.xaml 的交互逻辑
    /// </summary>
    public partial class WindowBehaviiors : Window
    {
        public WindowBehaviiors()
        {
            InitializeComponent();

            this.Loaded += WindowBehaviiors_Loaded;
        }

        private void WindowBehaviiors_Loaded(object sender, RoutedEventArgs e)
        {

            Border br = new Border();
            br.Width = br.Height = 100;
            br.Background = Brushes.Pink;
            Canvas.SetLeft(br, 100);
            Canvas.SetTop(br, 100);
            BG.Children.Add(br);
            br.BorderThickness = new Thickness(20);
            br.BorderBrush = Brushes.Red;
            br.CornerRadius = new CornerRadius(50);

            //动画
            //1.找剧本   故事本
            Storyboard story1 = new Storyboard();
            //2.选择动画类型  
            //两点动画  差值动画
            DoubleAnimation da = new DoubleAnimation();
            //必要属性1：设置动画的起始值
            da.From = 100;
            //必要属性2：设置动画的终止值
            da.To = 500;
            //必要属性3：动画的进行时间
            da.Duration = new Duration(TimeSpan.FromSeconds(2));
            //必要属性4：设置某个动画的主角(男女一号)
            Storyboard.SetTarget(da, br);
            //必要属性5：设置动画的属性
            Storyboard.SetTargetProperty(da, new PropertyPath("(Canvas.Left)"));
            //可选属性1.是否反着播放
            da.AutoReverse = true;
            //可选属性2.是否重复播放
            da.RepeatBehavior = RepeatBehavior.Forever;
            //重复几次
            // da.RepeatBehavior = new RepeatBehavior(3);
            //将动画添加进故事版里面
            story1.Children.Add(da);
            //动画开始
            story1.Begin();


        }

        private void BtnClick_Click(object sender, RoutedEventArgs e)
        {
            //Point poninOne = oneBtn.PointToScreen(new Point(0.5, 0.5));

            //Point TwonPoin = twoBtn.PointToScreen(new Point(0.5, 0.5));


        }
    }
}
