using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.Office.Core;
using ppt = Microsoft.Office.Interop.PowerPoint;

namespace OfficeOperator
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public ppt.Presentation ObjPrs { get; private set; }

        public ppt.SlideShowView OSlideShowView { get; private set; }

        public ppt.Application ObjApp { get; private set; }

        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr childIntPtr, IntPtr parentIntPtr);

        public MainWindow()
        {
            InitializeComponent();


            //防止连续打开多个PPT程序.
            if (ObjApp != null) { return; }
            ObjApp = new ppt.Application();


            var tempOpenPPT = OpenPpt(@"D:\TIYE\2.评审活动管理系统平台(1).pptx");

            PlayPpt(tempOpenPPT);
        }


        private void PptPlayWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Closed += OnClosed;
        }

        private void OnClosed(object sender, EventArgs eventArgs)
        {
            try
            {
                ObjPrs.Close();
                ObjApp.Quit();
            }
            catch
            {

            }
        }

        /// <summary>
        /// 播放ppt
        /// </summary>
        /// <param name="objPrs"></param>
        public void PlayPpt(ppt.Presentation objPrs)
        {
            ObjPrs = objPrs;
            //进入播放模式
            var objSlides = objPrs.Slides;
            var objSss = objPrs.SlideShowSettings;
            objSss.LoopUntilStopped = MsoTriState.msoCTrue;
            objSss.StartingSlide = 1;
            objSss.EndingSlide = objSlides.Count;
            objSss.ShowType = ppt.PpSlideShowType.ppShowTypeKiosk;
            var sw = objSss.Run();

            OSlideShowView = objPrs.SlideShowWindow.View;
            var wn = (IntPtr)sw.HWND;

            //嵌入窗体
            var fromVisual = (HwndSource)PresentationSource.FromVisual(Panel);
            if (fromVisual == null)
            {
                return;
            }
            var parentHwnd = fromVisual.Handle;
            SetParent(wn, parentHwnd);
        }

        /// <summary>
        /// 打开PPT
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public ppt.Presentation OpenPpt(string url)
        {
            var objPresSet = ObjApp.Presentations;
            var objPrs = objPresSet.Open(url, MsoTriState.msoTrue, MsoTriState.msoTrue, MsoTriState.msoFalse);
            return objPrs;
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <returns></returns>
        public int Next()
        {
            OSlideShowView.Next();
            var index = OSlideShowView.Slide.SlideIndex - 1;
            return index;
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <returns></returns>
        public int Previous()
        {
            OSlideShowView.Previous();
            var index = OSlideShowView.Slide.SlideIndex - 1;
            return index;
        }

        /// <summary>
        /// 跳到制定页
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int GoToSlide(int index)
        {
            OSlideShowView.GotoSlide(index);
            return index;
        }
    }
}
