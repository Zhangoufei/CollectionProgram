using System;
using System.IO;
using System.Reflection;
using System.Windows;

namespace WpfVLCTest
{
    /// <summary>
    /// VlcWindowTest.xaml 的交互逻辑
    /// </summary>
    public partial class VlcWindowTest : Window
    {
        public VlcWindowTest()
        {
            InitializeComponent();


            var currentAssembly = Assembly.GetEntryAssembly();
            var currentDirectory = new FileInfo(currentAssembly.Location).DirectoryName;
            var libDirectory = new DirectoryInfo(System.IO.Path.Combine(currentDirectory, "libvlc", IntPtr.Size == 4 ? "win-x86" : "win-x64"));

            //初始化SourceProvider
            vlc.SourceProvider.CreatePlayer(libDirectory);


            this.Loaded += VlcWindowTest_Loaded;
          
        }

        private void VlcWindowTest_Loaded(object sender, RoutedEventArgs e)
        {
            string[] options = { ":rtsp-tcp" };
            string url = "http://10.95.33.13:90/4a4cb1fe3e667a0d8408078dcd6b8dc1/1.幸运大转盘.mp4";
            //url = @"D:\TIYE\IntelligentClass\cachedFiles\5.玩毛线的小猫咪.mp4";
            vlc.SourceProvider.MediaPlayer.Play(new Uri(url), options);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            vlc.SourceProvider.MediaPlayer.Position = 0.95f;
        }
    }
}
