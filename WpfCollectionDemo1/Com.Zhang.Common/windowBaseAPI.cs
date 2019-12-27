using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Com.Zhang.Common
{
    public class windowBaseAPI
    {
        public const int SW_HIDE = 0;
        public const int SW_SHOWNORMAL = 1;
        public const int SW_SHOWMINIMIZED = 2;
        public const int SW_SHOWMAXIMIZED = 3;
        public const int SW_SHOW = 5;

        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_CHAR = 0x0102;
        public const int WM_COMMAND = 0x0112;      // Code for Windows command
        public const int WM_CLOSE = 0xF060;      // Command code for close window

        public const int VK_LEFT = 0x25;
        public const int VK_RIGHT = 0x27; 
        public const int VK_RETURN = 0x0D;
        public const int VK_SPACE = 0x20;

        public struct WindowRect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.dll")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string className, string windowName);

        [DllImport("user32.dll")]
        public static extern IntPtr CloseWindow(IntPtr hwnd);

        [DllImport("user32.dll", EntryPoint = "ShowWindow", CharSet = CharSet.Auto)]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(HandleRef hwnd, out WindowRect rect);

        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, uint wParam, uint lParam);


        [DllImport("user32.dll", SetLastError = true)]
        public static extern int SetParent(IntPtr hWndChild, IntPtr hWndNewParent);


        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);


        [DllImport("user32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd,
        int hWndInsertAfter,
        int X,
        int Y,
        int cx,
        int cy,
        int uFlags
        );

        public static void SetWindowPos(IntPtr hWnd)
        {
            SetWindowPos(hWnd, -1, 0, 0, 0, 0, 3);
            Thread.Sleep(500);
            SetWindowPos(hWnd, -2, 0, 0, 0, 0, 3);
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);


        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        private static extern int SystemParametersInfo(
           int uAction,
           int uParam,
           string lpvParam,
           int fuWinIni
       );

        /// <summary>
        /// 设置壁纸
        /// </summary>
        /// <param name="url">壁纸物理路径</param>
        public static void SetWall(string url)
        {
            SystemParametersInfo(20, 0, url, 0x2);
        }
    }


}
