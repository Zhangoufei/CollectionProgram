using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace dependency.Model
{


    public class ListBoxDependancyProperty : ListBox
    {

        /// <summary>
        /// 鼠标按下背景样式
        /// </summary>
        public Brush SelectedBackground
        {
            get { return (Brush)GetValue(SelectedBackgroundProperty); }
            set { SetValue(SelectedBackgroundProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBackgroundProperty =
            DependencyProperty.RegisterAttached("SelectedBackground", typeof(Brush), typeof(ListBoxDependancyProperty), new PropertyMetadata(0));

        /// <summary>
        /// 鼠标按下背景样式
        /// </summary>
        public Brush SelectedBorderBrush
        {
            get { return (Brush)GetValue(SelectedBorderBrushProperty); }
            set { SetValue(SelectedBorderBrushProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SelectedBorderBrushProperty =
            DependencyProperty.RegisterAttached("SelectedBorderBrush", typeof(Brush), typeof(ListBoxDependancyProperty), new PropertyMetadata(0));



    }
}
