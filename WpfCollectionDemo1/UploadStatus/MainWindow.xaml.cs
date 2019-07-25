using System;
using System.Net;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using UploadStatus.ViewModel;

namespace UploadStatus
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowVM mainWindowVM = null;

        public MainWindow()
        {
            InitializeComponent();

            ComonConvert.MainWindow = this;

            DataContext = mainWindowVM = new MainWindowVM();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            CommandBinding NewCmd = new CommandBinding(NavigationCommands.GoToPage);
            NewCmd.Command.Execute("/controls/setting/Binding.xaml");
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        //下载地址
        private string pathString = @"D:\picture\download\";
        //下载文件
        //private string url = "http://192.168.17.82/1g/电影名称.mp4";
        private string url = "http://192.168.17.82/9b8531fefead480a9104dd836fadce4b/all falls down.mkv";

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

            using (WebClient client = new WebClient())
            {

                try
                {
                    client.DownloadProgressChanged += Client_DownloadProgressChanged;
                    // 下载完成
                    client.DownloadFileCompleted += Client_DownloadFileCompleted;

                    client.DownloadFileTaskAsync(url, pathString + DateTime.Now.ToString("yyyyMMddHHmmss") + ".mp4");


                    mainWindowVM.ForeceColoreVisibility = false;

                    mainWindowVM.ForeceLoadingVisibility = true;

                    mainWindowVM.ForceLoadingGif = mainWindowVM.pictureStyles.forceLoadingGif;

                }
                catch (Exception ee)
                {

                }
            }


        }
        /// <summary>
        /// 下载完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

            mainWindowVM.ForceLoadingGif = mainWindowVM.pictureStyles.forceLoadingGifComplate;

            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromSeconds(1.2);
            dispatcherTimer.Start();


        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            DispatcherTimer dispatcher = (DispatcherTimer)sender;
            dispatcher.Stop();

            mainWindowVM.ForceColore = mainWindowVM.pictureStyles.forceColoreComplate;   //下载完成

            mainWindowVM.ForeceLoadingVisibility = false;

            mainWindowVM.ForeceColoreVisibility = true;
        }

        /// <summary>
        /// 下载进度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            int persont = e.ProgressPercentage;


        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }
    }
}
