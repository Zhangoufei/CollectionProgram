using Com.Zhang.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;

namespace 嵌入第三方应用程序
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                PDFwindow pDFwindow = new PDFwindow(@"D:\Program Files\Notepad++\notepad++.exe");
                //pDFwindow.ShowDialog();
                pDFwindow.Show();
            }
            catch (System.Exception)
            {

            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            //Chrom chrom = new Chrom();
            //chrom.Show();


            var hostWinHandle = ((HwndSource)PresentationSource.FromVisual(border)).Handle;
            var plugin = new Chrom(@"D:\Program Files\Notepad++\notepad++.exe", hostWinHandle);
            //   border.Content = plugin;
            Content = plugin;
            plugin.EmbedProcess((int)border.Width, (int)border.Height);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowTestProgram windowTestProgram = new WindowTestProgram();
            windowTestProgram.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            PDFwindow pDFwindow = new PDFwindow(@"D:\Program Files (x86)\TE Desktop\TE_Desktop.exe");
            pDFwindow.Show();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Execute(@"D:\TIYE\Soft\TE_Desktop\TE_Desktop.exe","");
        }
        private static string Execute(string exePath, string parameters)
        {
            string result = string.Empty;

            using (Process p = new Process())
            {
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.FileName = exePath;
                p.StartInfo.Arguments = parameters;
                p.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
                p.Start();
            }
            return result;
        }

        //连接视频会议
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuider = new StringBuilder();
            stringBuider.Append("9004");
            //stringBuider.Append("&server_url=" + "117.158.117.154");
            //stringBuider.Append("&port=" + "");
            //stringBuider.Append("&conf_id=" + "9004");
            ////stringBuider.Append("&enter_code=" + "9004");
            //stringBuider.Append("&name=" + "932");
            //stringBuider.Append("&open_mic=" + "true");
            //stringBuider.Append("&open_camera=" + "false");



            string paht = @"D:\Program Files (x86)\TE Desktop/" + "TE_Desktop.exe";

            Execute(paht, stringBuider.ToString());

            // ShellExecute(IntPtr.Zero, "open", paht, stringBuider.ToString(), null, ShowWindowCommands.SW_SHOWNORMAL);
        }


        [DllImport("shell32.dll")]
        public static extern IntPtr ShellExecute(
       IntPtr hwnd,
       string lpszOp,
       string lpszFile,
       string lpszParams,
       string lpszDir,
       ShowWindowCommands FsShowCmd
   );

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            UtilityCommon.FindExeProcess("TE_Desktop");

            FindProcess("TE_Desktop");
        }


        /// <summary>
        /// 找到指定的进程名称
        /// </summary>
        /// <param name="processName"></param>
        public static bool FindProcess(string processName)
        {
            try
            {
                Process[] processlist = Process.GetProcesses();

                List<Process> resultList = new List<Process>();

                var temp = processlist.FirstOrDefault(p => p.ProcessName == processName);
                if (temp != null)
                {
                    return true;
                }
            }
            catch (Exception ee)
            {

            }
            return false;
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            string pName = "Te_Desktop";//要启动的进程名称，可以在任务管理器里查看，一般是不带.exe后缀的;
            Process[] temp = Process.GetProcessesByName(pName);//在所有已启动的进程中查找需要的进程；
            if (temp.Length > 0)//如果查找到
            {
                IntPtr handle = temp[0].MainWindowHandle;
                //SwitchToThisWindow(handle, true);    // 激活，显示在最前

                SetForegroundWindow(handle);
            }
            else
            {
                Process.Start(pName + ".exe");//否则启动进程
            }
        }

        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
    }



    public enum ShowWindowCommands : int
    {

        SW_HIDE = 0,
        SW_SHOWNORMAL = 1,    //用最近的大小和位置显示，激活
        SW_NORMAL = 1,
        SW_SHOWMINIMIZED = 2,
        SW_SHOWMAXIMIZED = 3,
        SW_MAXIMIZE = 3,
        SW_SHOWNOACTIVATE = 4,
        SW_SHOW = 5,
        SW_MINIMIZE = 6,
        SW_SHOWMINNOACTIVE = 7,
        SW_SHOWNA = 8,
        SW_RESTORE = 9,
        SW_SHOWDEFAULT = 10,
        SW_MAX = 10
    }

}
