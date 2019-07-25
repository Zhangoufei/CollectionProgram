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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace 磁盘操作
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private bool m_IsMouseLeftButtonDown;

        private void MasterImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;
            if (rectangle == null)
                return;

            rectangle.ReleaseMouseCapture();
            m_IsMouseLeftButtonDown = false;
        }

        private Point m_PreviousMousePoint;
        private void MasterImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;
            if (rectangle == null)
                return;

            rectangle.CaptureMouse();
            m_IsMouseLeftButtonDown = true;
            m_PreviousMousePoint = e.GetPosition(rectangle);
        }

        private void MasterImage_MouseMove(object sender, MouseEventArgs e)
        {
            Rectangle rectangle = sender as Rectangle;
            if (rectangle == null)
                return;

            if (m_IsMouseLeftButtonDown)
                DoImageMove(rectangle, e);
        }

        private void DoImageMove(Rectangle rectangle, MouseEventArgs e)
        {
            //Debug.Assert(e.LeftButton == MouseButtonState.Pressed); 
            if (e.LeftButton != MouseButtonState.Pressed)
                return;

            TransformGroup group = MainPanel.FindResource("ImageTransformResource") as TransformGroup;
            TranslateTransform transform = group.Children[1] as TranslateTransform;
            Point position = e.GetPosition(rectangle);
            transform.X += position.X - m_PreviousMousePoint.X;
            transform.Y += position.Y - m_PreviousMousePoint.Y;

            m_PreviousMousePoint = position;
        }

        private void MasterImage_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            TransformGroup group = MainPanel.FindResource("ImageTransformResource") as TransformGroup;
            ScaleTransform transform = group.Children[0] as ScaleTransform;
            transform.ScaleX += e.Delta * 0.001;
            transform.ScaleY += e.Delta * 0.001;
        }
    }
}
