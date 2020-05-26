using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace WindowsForms.Class
{
    public enum Angle
    {
        _0,
        _90,
        _180,
        _270
    }
    public class DrawingWPf
    {
        /// <summary>
        /// 实例化画图类
        /// </summary>
        /// <param name="_control"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public DrawingWPf(Control _control, Angle _Angle, double width, double height)
        {
            myControl = _control;
            m_nDeviceW = width;
            m_nDeviceH = height;
            Angle = _Angle;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_control"></param>
        /// <param name="_Angle"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="_offsetX"></param>
        /// <param name="_offsetY"></param>
        public DrawingWPf(Control _control, Angle _Angle, double width, double height, int _offsetX, int _offsetY)
        {
            myControl = _control;
            m_nDeviceW = width;
            m_nDeviceH = height;
            Angle = _Angle;
        }

        private Angle Angle = Angle._0;
        private int offsetX = 0;
        private int offsetY = 0;
        Control myControl;
        //bool bScreenO = true;//判断是否是横屏，默认横屏
        //画布宽高
        public double m_nDeviceW;
        public double m_nDeviceH;
        /// <summary>
        /// 点缩放比例
        /// </summary>
        public double m_nCompress_x = 0;
        public double m_nCompress_y = 0;

        private List<RobotPoint> plist = new List<RobotPoint>();

        /// <summary>
        /// 根据点缩放比例，缩放XY轴
        /// </summary>
        /// <param name="point"></param>
        public virtual void compressPoint(ref Point point)
        {

            double px = 0;
            double py = 0;
            switch (Angle)
            {
                case Angle._0:
                    {
                        px = point.X;
                        py = point.Y;

                        m_nCompress_x = (double)myControl.Width / (double)m_nDeviceW;
                        m_nCompress_y = (double)myControl.Height / (double)m_nDeviceH;

                        break;
                    }
                case Angle._90:
                    {
                        px = (m_nDeviceH - point.Y);
                        py = point.X;

                        m_nCompress_x = (double)myControl.Width / (double)m_nDeviceH;
                        m_nCompress_y = (double)myControl.Height / (double)m_nDeviceW;

                        break;
                    }
                case Angle._180:
                    {
                        px = (m_nDeviceW - point.X);
                        py = (m_nDeviceH - point.Y);

                        m_nCompress_x = (double)myControl.Width / (double)m_nDeviceW;
                        m_nCompress_y = (double)myControl.Height / (double)m_nDeviceH;
                        break;
                    }
                case Angle._270:
                    {
                        px = point.Y;
                        py = (m_nDeviceW - point.X);

                        m_nCompress_x = (double)myControl.Width / (double)m_nDeviceH;
                        m_nCompress_y = (double)myControl.Height / (double)m_nDeviceW;
                        break;
                    }
                default:
                    {
                        px = point.X;
                        py = point.Y;
                        m_nCompress_x = (double)myControl.Width / (double)m_nDeviceW;
                        m_nCompress_y = (double)myControl.Height / (double)m_nDeviceH;
                        break;
                    }
            }
            float nx = (float)((px + offsetX) * m_nCompress_x);
            float ny = (float)((py + offsetY) * m_nCompress_y);

            //point.X = int.Parse(nx + "");
            //point.Y = int.Parse(ny + "");

            point.X = nx;
            point.Y = ny;


        }
        //上一个点
        private Point m_point;
        //上一个点状态
        private int m_nPenStatus = 0;
        /// <summary>
        /// 判断是否是有效点
        /// </summary>
        /// <param name="nPenStatus"></param>
        /// <param name="pointValue"></param>
        /// <returns></returns>
        private bool pointIsInvalid(int nPenStatus, ref Point pointValue)
        {
            if ((m_point == pointValue) && (m_nPenStatus == nPenStatus))
                return false;
            m_point = pointValue;
            m_nPenStatus = nPenStatus;
            return true;
        }


        public void recvData(int nPenStatus, int x, int y, int nPress, float nWidth = 1)
        {
            Point pointf = new Point(x, y);
            if (!pointIsInvalid(nPenStatus, ref pointf))
                return;

            if (nPenStatus != 17 && nPenStatus != 33 && nPenStatus != 49)  // 笔离开到板子
            {
                if (nFlags == 1)
                {
                    plist.Add(new RobotPoint()
                    {
                        bx = x,
                        by = y,
                        bPenStatus = nPenStatus,
                        bPress = nPress,
                        bWidth = nWidth,
                        bIndex = plist.Count() + 1
                    });
                    if (DrawingCallbackBrushstroke_Evt != null)
                    {
                        DrawingCallbackBrushstroke_Evt(plist);
                        plist.Clear();
                    }
                    onEndDraw();
                }
                else
                {
                    onEndDraw();
                }
                nFlags = 0;
            }
            else
            {
                plist.Add(new RobotPoint()
                {
                    bx = x,
                    by = y,
                    bPenStatus = nPenStatus,
                    bPress = nPress,
                    bWidth = nWidth,
                    bIndex = plist.Count() + 1
                });
                if (nFlags == 0)
                {
                    nFlags = 1;
                    compressPoint(ref pointf);
                    onBeginDraw(ref pointf, nPenStatus);
                }
                else
                {
                    compressPoint(ref pointf);
                    onTrackDraw(ref pointf, nPenStatus, nWidth);
                }
            }
        }

        //轨迹点
        private List<CanvasItem> m_items = new List<CanvasItem>();  // 所有线条
        private CanvasItem m_currentItem;
        private bool m_bDrawing = false;
        private Point m_lastPoint;
        private int nFlags = 0;
        private void onBeginDraw(ref Point p, int status)
        {
            m_bDrawing = true;
            m_lastPoint = p;
            CanvasItem item = new CanvasItem();
            item.listpoints = new List<Point>();
            item.listPenWidthf = new List<float>();
            item.beginPoint = p;
            m_currentItem = item;
        }
        private void onTrackDraw(ref Point p, int status, float width)
        {
            if (!m_bDrawing)
                return;
            doDrawing(ref p, status, width);
            m_currentItem.listpoints.Add(p);

        }


        public event Action<Point, Point> DoDrawingEvent;
        /// <summary>
        /// 绘制的方法
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="nCompress"></param>
        private void doDrawing(ref Point pos, int status, float width)
        {
            //Graphics grap = myControl.CreateGraphics();
            //grap.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //grap.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //Color pointc = Color.Black;
            //float penW = width;
            //if (status == 33)
            //{
            //    pointc = Color.Red;
            //}
            //else if (status == 49)
            //{
            //    pointc = Color.White;
            //    penW = 10;
            //}
            //grap.DrawLine(new Pen(pointc, penW), m_lastPoint, pos);
            //m_lastPoint = pos;
            //grap.Dispose();


            DoDrawingEvent(m_lastPoint, pos);
            m_lastPoint = pos;

            //ellipses.Children.Add(new LineGeometry(m_lastPoint, pos));

            //m_lastPoint = pos;

            //(myControl as System.Windows.Shapes.Path).Data = ellipses;

            // path.Data = ellipses;
        }
        private void onEndDraw()
        {
            m_bDrawing = false;
            if (m_currentItem.listpoints == null)
            {
                return;
            }
            m_items.Add(m_currentItem);
        }


        // 接收到优化后的数据点数据
        public void recvOptimizeData(int nPenStatus, int x, int y, float fPenWidth)
        {
            if (fPenSize < 0)
                return;

            Point pointf;
            pointf = new Point(x, y);



            if (!pointIsInvalid(nPenStatus, ref pointf))
                return;

            if (fPenWidth == 0)  // 笔离开到板子
            {
                if (nFlags == 1)
                {
                    plist.Add(new RobotPoint()
                    {
                        bx = x,
                        by = y,
                        bPenStatus = nPenStatus,
                        bWidth = fPenWidth,
                        bIndex = plist.Count() + 1
                    });
                    if (DrawingCallbackBrushstroke_Evt != null)
                    {
                        DrawingCallbackBrushstroke_Evt(plist);
                        plist.Clear();
                    }
                    onEndDrawBezier();
                    //else
                    // onEndDrawBezier();
                    nFlags = 0;
                }

            }
            else
            {
                plist.Add(new RobotPoint()
                {
                    bx = x,
                    by = y,
                    bPenStatus = nPenStatus,
                    bWidth = fPenWidth,
                    bIndex = plist.Count() + 1
                });
                if (nFlags == 0)
                {
                    nFlags = 1;
                    compressPoint(ref pointf);
                    onBeginDrawBezier(ref pointf, fPenWidth);
                }
                else
                {
                    compressPoint(ref pointf);
                    onTrackDrawBezier(ref pointf, fPenWidth);
                }
            }
        }
        List<Point> lstBezierPoints = new List<Point>();
        bool bFristPoint = false;
        float fPenSize = 0;
        public void onBeginDrawBezier(ref Point p, float fPenWidth)
        {
            // doPointDrawing(ref p);
            //return;

            bFristPoint = true;
            lstBezierPoints.Clear();
            m_bDrawing = true;
            lstBezierPoints.Add(p);

            CanvasItem item = new CanvasItem();
            item.listpoints = new List<Point>();
            item.listPenWidthf = new List<float>();

            item.beginPoint = p;
            m_currentItem = item;
        }

        public void onTrackDrawBezier(ref Point p, float fPenWidth)
        {
            if (!m_bDrawing)
                return;

            //doPointDrawing(ref p);
            // return;


            lstBezierPoints.Add(p);
            m_currentItem.listpoints.Add(p);
            m_currentItem.listPenWidthf.Add(fPenWidth);
            fPenSize = fPenWidth;
            if (bFristPoint)
            {
                Point centerPos = new Point();
                Point p1 = lstBezierPoints[0];
                Point p2 = lstBezierPoints[1];
                getCenterPoint(ref p1, ref p2, ref centerPos);
                doLineDrawing(ref p1, ref centerPos, fPenWidth);
                m_lastPoint = centerPos;
                lstBezierPoints.RemoveAt(0);
                bFristPoint = false;
            }
            else
            {
                Point centerPos = new Point();
                Point p1 = lstBezierPoints[0];
                Point p2 = lstBezierPoints[1];
                double gap = Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
                if (gap <= fPenWidth)
                {
                    lstBezierPoints.RemoveAt(1);
                    return;
                }

                getCenterPoint(ref p1, ref p2, ref centerPos);
                doBezierDrawing(ref m_lastPoint, ref p1, ref centerPos, fPenSize);
                m_lastPoint = centerPos;
                lstBezierPoints.RemoveAt(0);
            }

        }

        public void onEndDrawBezier()
        {
            m_bDrawing = false;
            //return;

            int nPointCount = lstBezierPoints.Count;
            if (nPointCount == 1)
            {
                Point p = lstBezierPoints[0];
                doLineDrawing(ref m_lastPoint, ref p, fPenSize);
            }
            else if (nPointCount == 2)
            {
                Point centerPos = new Point();
                Point p1 = lstBezierPoints[0];
                Point p2 = lstBezierPoints[1];
                getCenterPoint(ref p1, ref p2, ref centerPos);
                doBezierDrawing(ref m_lastPoint, ref p1, ref centerPos, fPenSize);
                doLineDrawing(ref centerPos, ref p2, fPenSize);
            }

            if (m_currentItem.listpoints == null)
            {
                return;
            }
            m_items.Add(m_currentItem);
        }
        private void getCenterPoint(ref Point p1, ref Point p2, ref Point p3)
        {
            double fx = (p1.X + p2.X) / 2;
            double fy = (p1.Y + p2.Y) / 2;
            p3.X = int.Parse(fx + "");
            p3.Y = int.Parse(fy + "");
        }
        private void doBezierDrawing(ref Point p1, ref Point p2, ref Point p3, float fPenWidthF)
        {
            //Graphics grap = myControl.CreateGraphics();
            //grap.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //grap.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //grap.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
            //grap.DrawBezier(new Pen(Color.Black, fPenWidthF), p1, p2, p2, p3);
            //grap.Dispose();
        }
        private void doLineDrawing(ref Point p1, ref Point p2, float fPenWidthF)
        {
            //Graphics grap = myControl.CreateGraphics();
            //grap.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //grap.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //grap.DrawLine(new Pen(Color.Black, fPenWidthF), p1, p2);
            //grap.Dispose();
        }

        public delegate void DrawingCallbackBrushstroke(List<RobotPoint> _plist);
        public event DrawingCallbackBrushstroke DrawingCallbackBrushstroke_Evt;
    }

    public class RobotPoint
    {
        public int bIndex { get; set; }
        public int bPenStatus { get; set; }
        public int bx { get; set; }
        public int by { get; set; }
        public int bPress { get; set; }
        public float bWidth { get; set; }
        public float bSpeed { get; set; }
        public bool isOptimize { get; set; }
    }

    public struct CanvasItem
    {
        public Point beginPoint { get; set; }
        public List<Point> listpoints;
        public List<float> listPenWidthf;
    }
}
