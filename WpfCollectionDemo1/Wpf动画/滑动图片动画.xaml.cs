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
    /// 滑动图片动画.xaml 的交互逻辑
    /// </summary>
    public partial class 滑动图片动画 : Window
    {
        public 滑动图片动画()
        {
            InitializeComponent();
        }


        ///
        /// 完成缓冲效果
        ///
        /// 起始位置
        /// 目标位置
        /// 加速加速度
        /// 减速加速度
        /// 持续时间
        private void DoMove(DependencyProperty dp, double to, double ar, double dr, double duration)
        {
            DoubleAnimation doubleAnimation = new DoubleAnimation();//创建双精度动画对象

            doubleAnimation.To = to;//设置动画的结束值
            doubleAnimation.Duration = TimeSpan.FromSeconds(duration);//设置动画时间线长度
            doubleAnimation.AccelerationRatio = ar;//动画加速
            doubleAnimation.DecelerationRatio = dr;//动画减速
            doubleAnimation.FillBehavior = FillBehavior.HoldEnd;//设置动画完成后执行的操作

            grdTransfer.BeginAnimation(dp, doubleAnimation);//设置动画应用的属性并启动动画
        }



        private double pressedX;

        ///
        /// 点击鼠标，记录鼠标单击的位置
        ///
        ///
        ///
        private void grdTest_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ////获得鼠标点击的X坐标
            pressedX = e.GetPosition(cvsGround).X;

        }



        ////鼠标释放时的操作

        private void grdTest_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            double transferLeft = Convert.ToDouble(grdTransfer.GetValue(Canvas.LeftProperty));
            btn1.Content = transferLeft.ToString();
            if (transferLeft > 0)
            {
                transferLeft = 0;
            }
            if (this.Width - transferLeft > cvsGround.Width)
            {
                transferLeft = this.Width - cvsGround.Width;
            }

            ////获得鼠标释放时的位置

            double releasedX = e.GetPosition(cvsGround).X;

            ////获得距离间隔
            double interval = releasedX - pressedX;
            pressedX = 0;
            ////计算出传送带要的目标位置
            double to = transferLeft + interval;
            ////移动

            btn1.Content = transferLeft.ToString() + " " + to.ToString();
            // btn1.Content = transferLeft.ToString() + " " + to.ToString();
            DoMove(Canvas.LeftProperty, to, 0.1, 0.5, 0.5);
        }
    }
}
