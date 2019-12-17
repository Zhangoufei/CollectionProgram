using System;
using System.Windows;

namespace MyStyle
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //SplashScreen splashScreen = new SplashScreen("image/dd.png");
            SplashScreen splashScreen = new SplashScreen("image/dd.png");
            splashScreen.Show(true);
            splashScreen.Close(TimeSpan.FromSeconds(3));
        }
    }
}
