using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MyStyle.Styles
{

    public class MyButtonStyle : Button
    {

        /// <summary>
        ///button 按钮按压的背景色
        /// </summary>
        public Brush MyBackgroundPress
        {
            get { return (Brush)GetValue(MyBackgroundPressProperty); }
            set { SetValue(MyBackgroundPressProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyBackgroundPressProperty =
            DependencyProperty.Register("MyBackgroundPress", typeof(Brush), typeof(MyButtonStyle), new PropertyMetadata(Brushes.AntiqueWhite));


        static MyButtonStyle()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyButtonStyle), new FrameworkPropertyMetadata(typeof(MyButtonStyle)));
        }
    }
}
