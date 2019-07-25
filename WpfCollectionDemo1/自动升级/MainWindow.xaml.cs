using AutoUpdaterDotNET;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 自动升级
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
            AutoUpdater.AppTitle = "111";
            AutoUpdater.AppCastURL = ConfigurationManager.AppSettings["UrlUpdate"];
            AutoUpdater.ApplicationExitEvent += AutoUpdater_ApplicationExitEvent; ;
            AutoUpdater.CheckForUpdateEvent += AutoUpdater_CheckForUpdateEvent; ;
            AutoUpdater.Start();
        }

        private void AutoUpdater_CheckForUpdateEvent(UpdateInfoEventArgs args)
        {
            if (args == null) return;

            if (args.IsUpdateAvailable) {

                AutoUpdater.ShowUpdateForm();
            }

        }

        private void AutoUpdater_ApplicationExitEvent()
        {
            this.Close();
        }
    }
}
