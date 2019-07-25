using log4net;
using log4net.Config;
using System;
using System.IO;

namespace LoggerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var logCfg = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config");
            XmlConfigurator.ConfigureAndWatch(logCfg);
            ILog logger = LogManager.GetLogger(typeof(Program));

            logger.Debug("Debug");
            logger.Info("Info");
            logger.Warn("Warn");
            logger.Error("Error");
            logger.Fatal("Fatal");

            Console.ReadLine();
        }
    }
}
