using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Interop;

namespace 嵌入第三方应用程序
{
    /// <summary>
    /// WindowTestProgram.xaml 的交互逻辑
    /// </summary>
    public partial class WindowTestProgram : Window
    {
        public WindowTestProgram()
        {
            InitializeComponent();


            //IntPtr handle = new WindowInteropHelper(this).Handle;


            Loaded += WindowTestProgram_Loaded;


            IntPtr handle = new WindowInteropHelper(this).Handle;
            //  app = Process.Start(@"D:\Program Files\Notepad++\notepad++.exe");
            //  app = Process.Start(@"D:\Program Files (x86)\Seewo\SeewoLink\LatestVersion\SeewoLink.exe");
            app = Process.Start(@"D:\Program Files (x86)\TE Desktop\TE_Desktop.exe");

            prsmwh = app.MainWindowHandle;
            while (prsmwh == IntPtr.Zero)
            {
                Thread.Sleep(10);
                prsmwh = app.MainWindowHandle;
            }
            //设置父窗口
            SetParent(prsmwh, handle);
            ShowWindowAsync(prsmwh, 1);//子窗口最大化 
        }

        private void WindowTestProgram_Loaded(object sender, RoutedEventArgs e)
        {
            //var temp = CommonUntility.FindProcess("SeewoLink");
            //if (!temp)
            //{
            //获取当前窗口句柄

            //}
            //else
            //{
            //    IntPtr handle = new WindowInteropHelper(this).Handle;
            //    //app = Process.Start(@"D:\Program Files\Notepad++\notepad++.exe");
            //    app = Process.Start(@"D:\Program Files (x86)\Seewo\SeewoLink\LatestVersion\SeewoLink.exe");

            //    prsmwh = app.MainWindowHandle;
            //    while (prsmwh == IntPtr.Zero)
            //    {
            //        prsmwh = app.MainWindowHandle;
            //    }
            //    //设置父窗口
            //    SetParent(prsmwh, handle);
            //    ShowWindowAsync(prsmwh, 3);//子窗口最大化 
            //}
        }

        //定义变量
        private IntPtr prsmwh;//外部EXE文件运行句柄

        private Process app;//外部exe文件对象



        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);


        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);


        [DllImport("user32.dll")]
        static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
