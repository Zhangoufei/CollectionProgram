using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;

namespace OpenWrite
{
    public class InkCanvasTest : InkCanvas
    {


        public DrawingAttributes drawingAttributes;
        public InkCanvasTest()
        {

            EraserShape = new RectangleStylusShape(50, 70);

            MouseMove += InkCanvasTest_MouseMove;


            drawingAttributes = new DrawingAttributes();

            DefaultDrawingAttributes = drawingAttributes;

            //画笔大小
            drawingAttributes.Height = 5;
            drawingAttributes.Width = 5;

            drawingAttributes.Color = Colors.Red;

            StrokeCollected += InkCanvasTest_StrokeCollected;

        }

        private void InkCanvasTest_MouseMove(object sender, MouseEventArgs e)
        {

        }

        public PointStyle pointStyle = PointStyle.None;
        private void InkCanvasTest_StrokeCollected(object sender, InkCanvasStrokeCollectedEventArgs e)
        {
            switch (pointStyle)
            {
                case PointStyle.StarghtLine:
                    if (e.Stroke.StylusPoints.Count > 1)
                    {
                        UpdateLine(e.Stroke);
                    }
                    break;
                case PointStyle.None:
                    break;
                case PointStyle.ImaginaryLine:
                    UpdateImagenaryLine(e.Stroke);
                    break;
                case PointStyle.ImageLineMao:
                    Strokes.Remove(e.Stroke);
                    Strokes.Add(new ChinesebrushStroke(e.Stroke.StylusPoints
                        , DefaultDrawingAttributes.Color));
                    break;
                default:
                    break;
            }

        }

        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParams)
        {

            return null;
        }


        private void UpdateLine(Stroke currentStroke)
        {
            StylusPoint beginPoint = currentStroke.StylusPoints[0];//起始点
            StylusPoint endPoint = currentStroke.StylusPoints.Last();//终点
            Strokes.Remove(currentStroke);//移除原来的笔画
            List<Point> pointList = new List<Point>();
            pointList.Add(new Point(beginPoint.X, beginPoint.Y));
            pointList.Add(new Point(endPoint.X, endPoint.Y));
            StylusPointCollection point = new StylusPointCollection(pointList);
            Stroke stroke = new Stroke(point);//用两点实现笔画
            stroke.DrawingAttributes = DefaultDrawingAttributes.Clone();
            Strokes.Add(stroke);
        }



        private void UpdateImagenaryLine(Stroke currentStroke)
        {
            Strokes.Remove(currentStroke);//移除原来笔画

            StylusPoint beginPoint = currentStroke.StylusPoints[0];//起始点
            StylusPoint endPoint = currentStroke.StylusPoints.Last();//终点
            int dotTime = 0;
            int intervalLen = 6;//步长
            double lineLen = Math.Sqrt(Math.Pow(beginPoint.X - endPoint.X, 2) + Math.Pow(beginPoint.Y - endPoint.Y, 2));//线的长度
            Point currentPoint = new Point(beginPoint.X, beginPoint.Y);
            double relativaRate = Math.Abs(endPoint.Y - beginPoint.Y) * 1.0 / Math.Abs(endPoint.X - beginPoint.X);
            double angle = Math.Atan(relativaRate) * 180 / Math.PI;//直线的角度大小，无需考虑正负
            int xOrientation = endPoint.X > beginPoint.X ? 1 : -1;//判断新生成点的X轴方向
            int yOrientation = endPoint.Y > beginPoint.Y ? 1 : -1;
            if (lineLen < intervalLen)
            {
                return;
            }
            while (dotTime * intervalLen < lineLen)
            {
                double x = currentPoint.X + dotTime * intervalLen * Math.Cos(angle * Math.PI / 180) * xOrientation;
                double y = currentPoint.Y + dotTime * intervalLen * Math.Sin(angle * Math.PI / 180) * yOrientation;
                List<Point> pL = new List<Point>();
                pL.Add(new Point(x, y));
                x += intervalLen * Math.Cos(angle * Math.PI / 180) * xOrientation;
                y += intervalLen * Math.Sin(angle * Math.PI / 180) * yOrientation;
                pL.Add(new Point(x, y));
                StylusPointCollection spc = new StylusPointCollection(pL);//相邻两点作为一个笔画
                Stroke stroke = new Stroke(spc);
                stroke.DrawingAttributes = DefaultDrawingAttributes.Clone();
                Strokes.Add(stroke);
                dotTime += 2;
            }
        }

    }


    public enum PointStyle
    {

        /// <summary>
        /// 直线
        /// </summary>
        StarghtLine,
        /// <summary>
        /// 无
        /// </summary>
        None,

        /// <summary>
        /// 虚线
        /// </summary>
        ImaginaryLine,
        /// <summary>
        /// 画毛笔
        /// </summary>
        ImageLineMao,
    }
}
