using log4net;
using System;
using System.Runtime.InteropServices;

namespace Com.Tiye.Log
{
    /// <summary>
    /// 日志辅助类
    /// </summary>
    public static class Logger
    {
        private static ILog fileLogger;
        private static ILog consoleLogger;


        static Logger()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(
               new System.IO.FileInfo("log4netLogger.config")); 
        }
        /// <summary>
        /// 只有在使用日志的时候才会创建日志文件,避免创建大量空日志文件
        /// </summary>
        /// 
        private static ILog FileLogger
        {
            get { return fileLogger ?? (fileLogger = LogManager.GetLogger("FileLogger")); }
        }

        static ILog ConsoleLogger
        {
            get
            {
                if (consoleLogger == null)
                {
                    NativeMethods.AllocConsole();
                    consoleLogger = LogManager.GetLogger("ConsoleLogger");
                }
                return consoleLogger;
            }
        }

        private static ILog CurrentLogger
        {
            get
            {
                return DebugMode ? ConsoleLogger : FileLogger;
            }
        }

        public static Boolean DebugMode { get; set; }

        /// <summary>
        /// 记录调试信息
        /// </summary>
        /// <param name="msg"></param>
        public static void Debug(string msg)
        {
            CurrentLogger.Debug(msg);

            Console.WriteLine(msg);
        }

        public static void Info(string msg)
        {
            CurrentLogger.Info(msg);
            Console.WriteLine(msg);
        }
        public static void Warn(string msg)
        {
            CurrentLogger.Warn(msg);
            Console.WriteLine(msg);
        }
        public static void Fatal(string msg)
        {
            CurrentLogger.Fatal(msg);
            Console.WriteLine(msg);
        }


        /// <summary>
        /// 记录错误信息
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            CurrentLogger.Error(msg);
            Console.WriteLine(msg);
        }
    }
    public static class NativeMethods
    {
        [DllImport("kernel32.dll")]
        public static extern Boolean AllocConsole();
    }
}
