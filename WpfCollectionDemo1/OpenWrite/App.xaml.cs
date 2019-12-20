using System;
using System.Windows;

namespace OpenWrite
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
           StartupUri = new Uri("WindowEraserShap.xaml", UriKind.Relative);

         //  StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
        }
    }
}
