﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfDependencyDemo.ModelDependency
{
    public class ButtonDependency : Button
    {

        public static readonly DependencyProperty PressedBackgroundProperty =
       DependencyProperty.Register("PressedBackground", typeof(Brush), typeof(ButtonDependency), new PropertyMetadata(Brushes.DarkBlue));
        /// <summary>
        /// 鼠标按下背景样式
        /// </summary>
        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty PressedForegroundProperty =
            DependencyProperty.Register("PressedForeground", typeof(Brush), typeof(ButtonDependency), new PropertyMetadata(Brushes.White));
        /// <summary>
        /// 鼠标按下前景样式（图标、文字）
        /// </summary>
        public Brush PressedForeground
        {
            get { return (Brush)GetValue(PressedForegroundProperty); }
            set { SetValue(PressedForegroundProperty, value); }
        }

        public static readonly DependencyProperty MouseOverBackgroundProperty =
            DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(ButtonDependency), new PropertyMetadata(Brushes.RoyalBlue));
        /// <summary>
        /// 鼠标进入背景样式
        /// </summary>
        public Brush MouseOverBackground
        {
            get { return (Brush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MouseOverForegroundProperty =
            DependencyProperty.Register("MouseOverForeground", typeof(Brush), typeof(ButtonDependency), new PropertyMetadata(Brushes.White));
        /// <summary>
        /// 鼠标进入前景样式
        /// </summary>
        public Brush MouseOverForeground
        {
            get { return (Brush)GetValue(MouseOverForegroundProperty); }
            set { SetValue(MouseOverForegroundProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ButtonDependency), new PropertyMetadata(new CornerRadius(2)));
        /// <summary>
        /// 按钮圆角大小,左上，右上，右下，左下
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty ContentDecorationsProperty = DependencyProperty.Register(
            "ContentDecorations", typeof(TextDecorationCollection), typeof(ButtonDependency), new PropertyMetadata(null));
        public TextDecorationCollection ContentDecorations
        {
            get { return (TextDecorationCollection)GetValue(ContentDecorationsProperty); }
            set { SetValue(ContentDecorationsProperty, value); }
        }


        // <summary>
        /// 按钮点击后的样式
        /// </summary>
        public ClickStyles ClickStyle
        {
            get { return (ClickStyles)GetValue(ClickStyleProperty); }
            set { SetValue(ClickStyleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClickStyle.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ClickStyleProperty =
            DependencyProperty.Register("ClickStyle", typeof(ClickStyles), typeof(ButtonDependency), new PropertyMetadata(ClickStyles.Default));



        /// <summary>
        /// 初始化
        /// </summary>
        static ButtonDependency()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ButtonDependency), new FrameworkPropertyMetadata(typeof(ButtonDependency)));
        }
    }

    public enum ClickStyles
    {
        /// <summary>
        /// 默认样式
        /// </summary>
        Default,
        /// <summary>
        /// 下沉样式
        /// </summary>
        Sink,

    }
}
