using RobotpenGateway;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WindowsForms.Class;

namespace RoobotDrowing
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool usbIsConnected = false;
        eDeviceType deviceType = eDeviceType.Unknow;

        private RobotpenGateway.robotpenController.returnPointData date = null;

        private ReportRate RR = ReportRate.R_200;

        public MainWindow()
        {
            //初始化罗伯特笔
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
            InitPen();

        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            CheckUsbConnect();

            compressPictureBox();
        }

        private void InitPen()
        {
            robotpenController.GetInstance()._ConnectInitialize(eDeviceType.Gateway, IntPtr.Zero);
            date = new RobotpenGateway.robotpenController.returnPointData(Form1_bigDataReportEvt1);
            robotpenController.GetInstance().initDeletgate(ref date);
        }

        bool bScreenO = false;//判断是否是横屏，默认横屏,true:横屏幕；false:竖屏
        /// <summary>
        /// 点缩放比例
        /// </summary>
        private double m_nCompress = 0;
        private double m_nCompress_x = 0;
        private double m_nCompress_y = 0;

        //画布宽高
        private double m_nDeviceW = 29700;
        private double m_nDeviceH = 21000;

        Angle angle = Angle._270;
        /// <summary>
        /// 设置屏幕宽度，并生成点坐标缩放比例
        /// </summary>
        private void compressPictureBox()
        {
            double nBordereW = path.Width;
            double nBordereH = path.Height;

            if (bScreenO)  // 横屏 根据
            {
                m_nCompress = ((double)(m_nDeviceH) / nBordereH);  // 设备与屏幕的宽比例
                nBordereW = (int)Math.Ceiling(m_nDeviceW / m_nCompress);
                m_nCompress_x = ((double)(m_nDeviceW) / nBordereW);
                m_nCompress_y = ((double)(m_nDeviceH) / nBordereH);
            }
            else   // 竖屏
            {
                m_nCompress = ((double)(m_nDeviceW) / nBordereH);  // 设备与屏幕的宽比例
                nBordereW = (int)Math.Ceiling(m_nDeviceH / m_nCompress);                                                    // 计算高的比例
                m_nCompress_x = ((double)(m_nDeviceW) / nBordereH);
                m_nCompress_y = ((double)(m_nDeviceH) / nBordereW);
            }

            if (nBordereW > path.Width)
            {
                int len = int.Parse(nBordereW + "") - int.Parse(path.Width + "");
                path.Width = nBordereW;
            }
            else if (nBordereW < path.Width)
            {
                int len = int.Parse(path.Width + "") - int.Parse(nBordereW + "");
                path.Width = nBordereW;
            }
            drawing = new DrawingWPf(this, angle, m_nDeviceW, m_nDeviceH);
            drawing.DoDrawingEvent += Drawing_DoDrawingEvent;

            path.Data = ellipses;
            //drawing.DrawingCallbackBrushstroke_Evt += new DrawingWPf.DrawingCallbackBrushstroke(FormDrawingCallbackBrushstroke_Evt);
        }
        GeometryGroup ellipses = new GeometryGroup();
        private void Drawing_DoDrawingEvent(System.Windows.Point arg1, System.Windows.Point arg2)
        {
            //wpf  版本
            //   ellipses.Children.Add(new LineGeometry(arg1, arg2));



            //Graphics grap = drawingWinform.pictureBox1.CreateGraphics();
            //grap.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //grap.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            //var pointc = System.Drawing.Color.Black;
            ////float penW = width;
            ////if (status == 33)
            ////{
            ////    pointc = Color.Red;
            ////}
            ////else if (status == 49)
            ////{
            ////    pointc = Color.White;
            ////    penW = 10;
            ////}
            //grap.DrawLine(new System.Drawing.Pen(pointc, penW), m_lastPoint, pos);
          
            //grap.Dispose();

        }


        /// <summary>
        /// 判断是否有设备连接
        /// </summary>
        public void CheckUsbConnect()
        {
            usbIsConnected = false;
            Thread.Sleep(200);
            int nDeviceCount = robotpenController.GetInstance()._GetDeviceCount();
            if (nDeviceCount > 0)
            {
                //this.listView1.BeginUpdate();
                for (int i = 0; i < nDeviceCount; ++i)
                {
                    ushort npid = 0;
                    ushort nvid = 0;
                    string strDeviceName = string.Empty;
                    eDeviceType dtype = eDeviceType.Unknow;
                    if (robotpenController.GetInstance()._GetAvailableDevice(i, ref npid, ref nvid, ref strDeviceName, ref dtype))
                    {
                        if (!usbIsConnected)
                        {
                            usbIsConnected = true;
                            deviceType = dtype;
                            robotpenController.GetInstance()._ConnectInitialize(deviceType, IntPtr.Zero);
                            int nRes = robotpenController.GetInstance()._ConnectOpen();
                            if (nRes != 0)
                            {
                                MessageBox.Show("设备自动连接失败，请重新插拔设备或尝试手动连接!");
                                usbIsConnected = false;
                                break;
                            }
                            //robotpenController.GetInstance()._Send(cmdId.SwitchMode);
                            robotpenController.GetInstance()._Send(cmdId.GetConfig);
                        }

                        //this.listView1.Items.Add(strDeviceName);
                        string strVID = Convert.ToString(nvid);
                        //this.listView1.Items[i].SubItems.Add(strVID);
                        string strPID = Convert.ToString(npid);
                        //this.listView1.Items[i].SubItems.Add(strPID);
                        string strDType = Convert.ToString((int)dtype);
                        //this.listView1.Items[i].SubItems.Add(strDType);
                    }
                }
                //this.listView1.EndUpdate();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bIndex"></param>
        /// <param name="bPenStatus"></param>
        /// <param name="bx"></param>
        /// <param name="by"></param>
        /// <param name="bPress"></param>
        private void Form1_bigDataReportEvt1(byte bIndex, byte bPenStatus, short bx, short by, short bPress)
        {
            Console.WriteLine(string.Format(@"x={0},y={1},s={2},p={3}", bx, by, bPenStatus, bPress));
            switch (RR)
            {
                case ReportRate.R_200:
                    bigDataReport(bIndex, bPenStatus, bx, by, bPress);
                    return;
                case ReportRate.R_160:
                case ReportRate.R_120:
                case ReportRate.R_80:
                case ReportRate.R_40:
                    RobotPoint rp = new RobotPoint()
                    {
                        bIndex = bIndex,
                        bPenStatus = bPenStatus,
                        bx = bx,
                        by = by,
                        bPress = bPress
                    };
                    if (bPenStatus != PreviousPoint.bPenStatus)
                    {
                        foreach (var item in PointList)
                        {
                            bigDataReport(item.bIndex, item.bPenStatus, item.bx, item.by, item.bPress);
                        }
                        PointList.Clear();
                        bigDataReport(bIndex, bPenStatus, bx, by, bPress);
                    }
                    else
                    {
                        PointList.Add(rp);
                        if (PointList.Count == 6)
                        {
                            ReportRatePoint(PointList);
                        }
                    }
                    PreviousPoint = rp;
                    break;
            }

        }

        private void ReportRatePoint(List<RobotPoint> _pList)
        {
            switch (RR)
            {
                case ReportRate.R_160:
                    PointList.RemoveAt(3);
                    break;
                case ReportRate.R_120:
                    PointList.RemoveAt(2);
                    PointList.RemoveAt(4);
                    break;
                case ReportRate.R_80:
                    PointList.RemoveAt(1);
                    PointList.RemoveAt(2);
                    PointList.RemoveAt(3);
                    break;
                case ReportRate.R_40:
                    PointList.RemoveAt(1);
                    PointList.RemoveAt(1);
                    PointList.RemoveAt(2);
                    PointList.RemoveAt(2);
                    break;
            }
            foreach (var item in PointList)
            {
                if (isOptimize)
                {
                    OptimizePointDataReport(item.bPenStatus, item.bx, item.by, item.bWidth);
                }
                else
                {
                    bigDataReport(item.bIndex, item.bPenStatus, item.bx, item.by, item.bPress);
                }

            }
            PointList.Clear();
        }

        private void OptimizePointDataReport(int bPenStatus, int bx, int by, float fPenWidthF)
        {
            RobotPoint rp = new RobotPoint()
            {
                bPenStatus = bPenStatus,
                bx = bx,
                by = by,
                bWidth = fPenWidthF
            };
            plist.Add(rp);
            if (drawing != null && isOpen)
            {
                Dispatcher.Invoke(() =>
                {
                    drawing.recvOptimizeData(Convert.ToInt32(bPenStatus), Convert.ToInt32(bx), Convert.ToInt32(by), fPenWidthF);
                });
            }
        }

        bool isOptimize = false;
        private RobotPoint PreviousPoint = new RobotPoint();
        List<RobotPoint> plist = new List<RobotPoint>();
        private List<RobotPoint> PointList = new List<RobotPoint>();
        DrawingWPf drawing;
        public bool isOpen = true;

        private void bigDataReport(int bIndex, int bPenStatus, int bx, int by, int bPress)
        {
            RobotPoint rp = new RobotPoint()
            {
                bIndex = bIndex,
                bPenStatus = bPenStatus,
                bx = bx,
                by = by,
                bPress = bPress
            };
            plist.Add(rp);
            if (drawing != null && isOpen)
            {
                Dispatcher.Invoke(() =>
                {
                    drawing.recvData(Convert.ToInt32(bPenStatus), Convert.ToInt32(bx), Convert.ToInt32(by), bPress);
                });

            }
        }


    }


    public enum ReportRate
    {
        R_200,
        R_160,
        R_120,
        R_80,
        R_40
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
}
