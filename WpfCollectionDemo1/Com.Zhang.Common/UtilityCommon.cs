using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Interop;

namespace Com.Zhang.Common
{
    public class UtilityCommon
    {

        //定义变量
        private static IntPtr prsmwh;//外部EXE文件运行句柄

        private static Process app;//外部exe文件对象
      

        //启动外部程序
        public static void StartExE(string filePath)
        {
            ThreadPool.QueueUserWorkItem(StartProcess, filePath);
        }

        private static void StartProcess(object obj)
        {
            try
            {
                Process process = new Process();
                process.StartInfo.FileName = obj.ToString();
                if (process.Start())
                {
                    //windowBaseAPI.ShowWindow(process.MainWindowHandle,2);
                    windowBaseAPI.SetForegroundWindow(process.MainWindowHandle);
                }
            }
            catch (Exception ee)
            {
                
            }

        }

        //查找应用程序是否启动，如果启动的话
        public static bool FindExeProcess(string exeName)
        {
            IntPtr ParenthWnd = windowBaseAPI.FindWindow(null, exeName);
            if (ParenthWnd != IntPtr.Zero)
            {
                //选中当前的句柄窗口
                //windowBaseAPI.SetWindowPos(ParenthWnd);
                windowBaseAPI.SetForegroundWindow(ParenthWnd);
                return true;
            }
            else
            {
                return false;
            }
        }



        /// <summary>
        /// 打开exe
        /// </summary>
        /// <param name="exeName"></param>
        /// <returns></returns>
        private static bool OpenExe(string exeName)
        {
            Process pr = new Process();
            try
            {
                pr.StartInfo.FileName = string.Format(@"{0}\..\" + exeName, AssemblyDirectory);
                pr.Start();
                return true;
            }
            catch
            {
                return true;
            }
            finally
            {
                if (pr != null)
                {
                    pr.Close();
                }

            }
        }
        /// <summary>
        /// 获取进程路径名称
        /// </summary>
        private static string AssemblyDirectory
        {
            get
            {
                string codeBase = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }//获取当前代码运行的目录
    }
}
