using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Media3D;

namespace HandyControl.Controls
{
    public class CoverFlowItem : ModelVisual3D
    {
        internal static readonly double Interval = .2;

        internal static readonly double AnimationSpeed = 400;

        private readonly AxisAngleRotation3D _rotation3D;

        private readonly TranslateTransform3D _transform3D;

        private readonly Model3DGroup _model3DGroup;

        private readonly UIElement _uiElement;

        internal int Index { get; set; }

        public CoverFlowItem(int itemIndex, int currentIndex, UIElement element)
        {
            Index = itemIndex;
            _uiElement = element;
            _uiElement.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));

            _rotation3D = new AxisAngleRotation3D(new Vector3D(0, 1, 0), GetAngleByPos(currentIndex));
            _transform3D = new TranslateTransform3D(GetXByPos(currentIndex), 0, GetZByPos(currentIndex));

            var transformGroup = new Transform3DGroup();
            transformGroup.Children.Add(new RotateTransform3D(_rotation3D));
            transformGroup.Children.Add(_transform3D);

            _model3DGroup = new Model3DGroup();
            _model3DGroup.Children.Add(new GeometryModel3D(CreateItemGeometry(), new DiffuseMaterial(new VisualBrush(element))));
            _model3DGroup.Transform = transformGroup;

            Content = _model3DGroup;
        }

        /// <summary>
        ///     创建网格形状
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <param name="p3"></param>
        /// <returns></returns>
        private static MeshGeometry3D CreateMeshGeometry(Point3D p0, Point3D p1, Point3D p2, Point3D p3)
        {
            var positions = new Point3DCollection
            {
                p0,
                p1,
                p2,
                p3
            };

            var triangleIndices = new Int32Collection
            {
                0,
                1,
                2,
                2,
                3,
                0
            };

            var textureCoordinates = new PointCollection
            {
                new Point(0, 1),
                new Point(1, 1),
                new Point(1, 0),
                new Point(0, 0),
                new Point(1, 0),
                new Point(1, 1)
            };

            var normals = new Vector3DCollection
            {
                ArithmeticHelper.CalNormal(p0, p1, p2),
                ArithmeticHelper.CalNormal(p2, p3, p0)
            };

            var geometry3D = new MeshGeometry3D
            {
                Positions = positions,
                TriangleIndices = triangleIndices,
                TextureCoordinates = textureCoordinates,
                Normals = normals
            };

            geometry3D.Freeze();
            return geometry3D;
        }

        /// <summary>
        ///     创建内容形状
        /// </summary>
        /// <returns></returns>
        private Geometry3D CreateItemGeometry()
        {
            var sx = _uiElement.DesiredSize.Width > _uiElement.DesiredSize.Height ? 0 : 1 - _uiElement.DesiredSize.Width / _uiElement.DesiredSize.Height;
            var sy = _uiElement.DesiredSize.Width < _uiElement.DesiredSize.Height ? 0 : 1 - _uiElement.DesiredSize.Height / _uiElement.DesiredSize.Width;

            var p0 = new Point3D(-1 + sx, -1 + sy, 0);
            var p1 = new Point3D(1 - sx, -1 + sy, 0);
            var p2 = new Point3D(1 - sx, 1 - sy, 0);
            var p3 = new Point3D(-1 + sx, 1 - sy, 0);

            return CreateMeshGeometry(p0, p1, p2, p3);
        }

        private double GetAngleByPos(int index) => Math.Sign(Index - index) * -90;

        private double GetXByPos(int index) => Index * Interval + Math.Sign(Index - index) * 1.5;

        private double GetZByPos(int index) => Index == index ? 1 : 0;

        /// <summary>
        ///     移动
        /// </summary>
        internal void Move(int currentIndex, bool animated)
        {
            if (animated || _rotation3D.HasAnimatedProperties)
            {
                _rotation3D.BeginAnimation(AxisAngleRotation3D.AngleProperty,
                    AnimationHelper.CreateAnimation(GetAngleByPos(currentIndex), AnimationSpeed));
                _transform3D.BeginAnimation(TranslateTransform3D.OffsetXProperty,
                    AnimationHelper.CreateAnimation(GetXByPos(currentIndex), AnimationSpeed));
                _transform3D.BeginAnimation(TranslateTransform3D.OffsetZProperty,
                    AnimationHelper.CreateAnimation(GetZByPos(currentIndex), AnimationSpeed));
            }
            else
            {
                _rotation3D.Angle = GetAngleByPos(currentIndex);
                _transform3D.OffsetX = GetXByPos(currentIndex);
                _transform3D.OffsetZ = GetZByPos(currentIndex);
            }
        }

        /// <summary>
        ///     命中测试
        /// </summary>
        /// <param name="mesh"></param>
        /// <returns></returns>
        internal bool HitTest(MeshGeometry3D mesh)
        {
            foreach (var item in _model3DGroup.Children)
            {
                if (item is GeometryModel3D geometryModel3D)
                {
                    if (Equals(geometryModel3D.Geometry, mesh))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }




    /// <summary>
    ///     包含一些常用的动画辅助方法
    /// </summary>
    public class AnimationHelper
    {
        /// <summary>
        ///     创建一个Thickness动画
        /// </summary>
        /// <param name="thickness"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static ThicknessAnimation CreateAnimation(Thickness thickness = default(Thickness), double milliseconds = 200)
        {
            return new ThicknessAnimation(thickness, new Duration(TimeSpan.FromMilliseconds(milliseconds)))
            {
                EasingFunction = new PowerEase { EasingMode = EasingMode.EaseInOut }
            };
        }

        /// <summary>
        ///     创建一个Double动画
        /// </summary>
        /// <param name="toValue"></param>
        /// <param name="milliseconds"></param>
        /// <returns></returns>
        public static DoubleAnimation CreateAnimation(double toValue, double milliseconds = 200)
        {
            return new DoubleAnimation(toValue, new Duration(TimeSpan.FromMilliseconds(milliseconds)))
            {
                EasingFunction = new PowerEase { EasingMode = EasingMode.EaseInOut }
            };
        }
    }



    /// <summary>
    ///     包含内部使用的一些简单算法
    /// </summary>
    internal class ArithmeticHelper
    {
        /// <summary>
        ///     平分一个整数到一个数组中
        /// </summary>
        /// <param name="num"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int[] DivideInt2Arr(int num, int count)
        {
            var arr = new int[count];
            var div = num / count;
            var rest = num % count;
            for (int i = 0; i < count; i++)
            {
                arr[i] = div;
            }
            for (int i = 0; i < rest; i++)
            {
                arr[i] += 1;
            }
            return arr;
        }

        /// <summary>
        ///     计算控件在窗口中的可见坐标
        /// </summary>
        public static Point CalSafePoint(FrameworkElement element, FrameworkElement showElement, Thickness thickness = default(Thickness))
        {
            if (element == null || showElement == null) return default(Point);
            var point = element.PointToScreen(new Point(0, 0));

            if (point.X < 0) point.X = 0;
            if (point.Y < 0) point.Y = 0;

            var maxLeft = SystemParameters.WorkArea.Width -
                          ((double.IsNaN(showElement.Width) ? showElement.ActualWidth : showElement.Width) +
                           thickness.Left + thickness.Right);
            var maxTop = SystemParameters.WorkArea.Height -
                         ((double.IsNaN(showElement.Height) ? showElement.ActualHeight : showElement.Height) +
                          thickness.Top + thickness.Bottom);
            return new Point(maxLeft > point.X ? point.X : maxLeft, maxTop > point.Y ? point.Y : maxTop);
        }

        /// <summary>
        ///     获取布局范围框
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static Rect GetLayoutRect(FrameworkElement element)
        {
            var num1 = element.ActualWidth;
            var num2 = element.ActualHeight;
            if (element is Image || element is MediaElement)
                if (element.Parent is Canvas)
                {
                    num1 = double.IsNaN(element.Width) ? num1 : element.Width;
                    num2 = double.IsNaN(element.Height) ? num2 : element.Height;
                }
                else
                {
                    num1 = element.RenderSize.Width;
                    num2 = element.RenderSize.Height;
                }
            var width = element.Visibility == Visibility.Collapsed ? 0.0 : num1;
            var height = element.Visibility == Visibility.Collapsed ? 0.0 : num2;
            var margin = element.Margin;
            var layoutSlot = LayoutInformation.GetLayoutSlot(element);
            var x = 0.0;
            var y = 0.0;
            switch (element.HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    x = layoutSlot.Left + margin.Left;
                    break;
                case HorizontalAlignment.Center:
                    x = (layoutSlot.Left + margin.Left + layoutSlot.Right - margin.Right) / 2.0 - width / 2.0;
                    break;
                case HorizontalAlignment.Right:
                    x = layoutSlot.Right - margin.Right - width;
                    break;
                case HorizontalAlignment.Stretch:
                    x = Math.Max(layoutSlot.Left + margin.Left,
                        (layoutSlot.Left + margin.Left + layoutSlot.Right - margin.Right) / 2.0 - width / 2.0);
                    break;
            }
            switch (element.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    y = layoutSlot.Top + margin.Top;
                    break;
                case VerticalAlignment.Center:
                    y = (layoutSlot.Top + margin.Top + layoutSlot.Bottom - margin.Bottom) / 2.0 - height / 2.0;
                    break;
                case VerticalAlignment.Bottom:
                    y = layoutSlot.Bottom - margin.Bottom - height;
                    break;
                case VerticalAlignment.Stretch:
                    y = Math.Max(layoutSlot.Top + margin.Top,
                        (layoutSlot.Top + margin.Top + layoutSlot.Bottom - margin.Bottom) / 2.0 - height / 2.0);
                    break;
            }
            return new Rect(x, y, width, height);
        }

        /// <summary>
        ///     计算两点的连线和x轴的夹角
        /// </summary>
        /// <param name="center"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static double CalAngle(Point center, Point p) => Math.Atan2(p.Y - center.Y, p.X - center.X) * 180 / Math.PI;

        /// <summary>
        ///     判断是否是有效的双精度浮点数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsValidDoubleValue(object value)
        {
            var d = (double)value;
            if (!double.IsNaN(d))
                return !double.IsInfinity(d);
            return false;
        }

        /// <summary>
        ///     计算法线
        /// </summary>
        /// <param name="p0"></param>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static Vector3D CalNormal(Point3D p0, Point3D p1, Point3D p2)
        {
            var v0 = new Vector3D(p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z);
            var v1 = new Vector3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            return Vector3D.CrossProduct(v0, v1);
        }
    }
}