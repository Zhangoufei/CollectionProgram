using Com.Zhang.Common;
using System;
using System.Collections;
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



            string paht = @"c:\Program Files (x86)\TE Desktop/" + "TE_Desktop.exe";

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
            //string pName = "Te_Desktop";//要启动的进程名称，可以在任务管理器里查看，一般是不带.exe后缀的;
            //Process[] temp = Process.GetProcessesByName(pName);//在所有已启动的进程中查找需要的进程；
            //if (temp.Length > 0)//如果查找到
            //{
            //    IntPtr handle = temp[0].MainWindowHandle;
            //    //SwitchToThisWindow(handle, true);    // 激活，显示在最前

            //    SetForegroundWindow(handle);
            //}
            //else
            //{
            //    Process.Start(pName + ".exe");//否则启动进程
            //}
            string paht = @"d:\Program Files (x86)\TE Desktop\" + "TE_Desktop.exe";
            StartProcess(paht);

        }

        [DllImport("user32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            string paht = @"d:\Program Files (x86)\TE Desktop\" + "TE_Desktop.exe";
            StartProcess2(paht);
            this.ShowActivated = false;
            this.Focus();
        }
      

        private static void StartProcess(object obj)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = obj.ToString();
                process.Start();

                IntPtr prsmwh = process.MainWindowHandle;
                while (prsmwh == IntPtr.Zero)
                {
                    prsmwh = process.MainWindowHandle;
                }

                windowBaseAPI.SetWindowPos(process.MainWindowHandle);

                windowBaseAPI.SetForegroundWindow(process.MainWindowHandle);
                //if (process.Start())
                //{
                //    windowBaseAPI.SetWindowPos(process.MainWindowHandle);

                //    windowBaseAPI.SetForegroundWindow(process.MainWindowHandle);
                //}
            }
            catch (Exception ee)
            {

            }
        }

        private static void StartProcess2(object obj)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = obj.ToString();
                process.Start();
                //if (process.Start())
                //{
                //    //windowBaseAPI.ShowWindow(process.MainWindowHandle,2);
                //    windowBaseAPI.SetForegroundWindow(process.MainWindowHandle);
                //}
                CurrentProcess currentProcess = new CurrentProcess();

                currentProcess.GetCurrentWindowHandle(process);
            }
            catch (Exception ee)
            {

            }
        }

        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            if (processName.Length <= 0)
            {
                processName = Process.Start("hash.exe").ProcessName;//启动软件时获取进程名称  
            }
            else
            {
                Process[] temp = Process.GetProcessesByName(processName);//在所有已启动的进程中查找需要的进程；  
                if (temp.Length > 0)//如果查找到  
                {
                    IntPtr handle = temp[0].MainWindowHandle;
                    SwitchToThisWindow(handle, true);    // 激活，显示在最前  
                }

            }
        }
        string processName = "TE_Desktop";
       


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


    public class CurrentProcess
    {
        private static Hashtable processWnd = null;

        public delegate bool WNDENUMPROC(IntPtr hwnd, uint lParam);

        static CurrentProcess()
        {
            if (processWnd == null)
            {
                processWnd = new Hashtable();
            }
        }

        [DllImport("user32.dll", EntryPoint = "EnumWindows", SetLastError = true)]
        public static extern bool EnumWindows(WNDENUMPROC lpEnumFunc, uint lParam);

        [DllImport("user32.dll", EntryPoint = "GetParent", SetLastError = true)]
        public static extern IntPtr GetParent(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "GetWindowThreadProcessId")]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, ref uint lpdwProcessId);

        [DllImport("user32.dll", EntryPoint = "IsWindow")]
        public static extern bool IsWindow(IntPtr hWnd);

        [DllImport("kernel32.dll", EntryPoint = "SetLastError")]
        public static extern void SetLastError(uint dwErrCode);

        public IntPtr GetCurrentWindowHandle(Process process)
        {
            IntPtr ptrWnd = IntPtr.Zero;
            uint uiPid = (uint)process.Id;  // 进程 ID
            object objWnd = processWnd[uiPid];

            if (objWnd != null)
            {
                ptrWnd = (IntPtr)objWnd;
                if (ptrWnd != IntPtr.Zero && IsWindow(ptrWnd))  // 从缓存中获取句柄
                {
                    return ptrWnd;
                }
                else
                {
                    ptrWnd = IntPtr.Zero;
                }
            }

            bool bResult = EnumWindows(new WNDENUMPROC(EnumWindowsProc), uiPid);
            // 枚举窗口返回 false 并且没有错误号时表明获取成功
            if (!bResult && Marshal.GetLastWin32Error() == 0)
            {
                objWnd = processWnd[uiPid];
                if (objWnd != null)
                {
                    ptrWnd = (IntPtr)objWnd;
                }
            }

            return ptrWnd;
        }

        private static bool EnumWindowsProc(IntPtr hwnd, uint lParam)
        {
            uint uiPid = 0;

            if (GetParent(hwnd) == IntPtr.Zero)
            {
                GetWindowThreadProcessId(hwnd, ref uiPid);
                if (uiPid == lParam)    // 找到进程对应的主窗口句柄
                {
                    processWnd[uiPid] = hwnd;   // 把句柄缓存起来
                    SetLastError(0);    // 设置无错误
                    return false;   // 返回 false 以终止枚举窗口
                }
            }

            return true;
        }
    }


}
