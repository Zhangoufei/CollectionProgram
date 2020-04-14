using Com.Zhang.Common;
using Microsoft.Win32;
using SevenZip;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MVVMLite
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {

            Button button = sender as Button;

            switch (button.Content)
            {
                case "点击":
                    ListBoxWindow listBoxWindow = new ListBoxWindow();
                    listBoxWindow.Show();
                    break;
                case "安装应用":
                    SetApp();
                    break;
                case "下载器":
                    await DownlowadUrl();
                    break;
                case "解压拷贝":
                    ZIpFile();
                    break;
                case "读取配置文件":
                    ReadRegedit();
                    break;
                default:
                    break;
            }

        }

        //读取配置文件
        private void ReadRegedit()
        {
            string path = GetELFICRegeditValue("AIclass");
        }

        /// <summary>
        /// 在注册表中读取value 标准目录为 SOFTWARE\\TIYE\\IC_ELF 下面
        /// </summary>
        /// <param name="keyRegister"></param>
        public static string GetELFICRegeditValue(string keyRegister)
        {
            RegistryKey key = Registry.ClassesRoot;
            RegistryKey IC = key.OpenSubKey(@"Local Settings\Software\Microsoft\Windows\Shell\MuiCache", true);

            var temp2 = IC.GetValueNames();

            var tempIndex = temp2.FirstOrDefault(p => p.Contains("wisdom_class.exe"));

            UtilityCommon.StartExE(tempIndex);
            if (IC == null)
            {
                return "";
            }
            if (IC.GetValue(tempIndex) == null)
            {
                return "";
            }
            return IC.GetValue(tempIndex).ToString();
        }

        //安装应用
        private void SetApp()
        {
            string path = "D://360Downloads" + "//AIClass.exe";

            UtilityCommon.StartExE(path);
        }

        //解压包
        private void ZIpFile()
        {
            ////ZipFile.ExtractToDirectory("D://Program Files (x86)"+ "//Xuele.zip", @"D:\TIYE\Soft//");
            if (IntPtr.Size == 4)
            {
                SevenZipCompressor.SetLibraryPath(AppDomain.CurrentDomain.BaseDirectory + "x86//" + @"7z.dll");
            }
            else
            {
                SevenZipCompressor.SetLibraryPath(@"7z.dll");
            }

            SevenZipExtractor sevenZipExtractor = new SevenZipExtractor(@"D:\TIYE\Dowload\TE_Desktop.7z");
            int sumFile = 0;
            sevenZipExtractor.FileExtractionFinished += (ssd, mm) =>
            {
                sumFile++;
                if (sumFile == sevenZipExtractor.FilesCount)
                {
                    MessageBox.Show("解压完成");
                }
            };
            sevenZipExtractor.BeginExtractArchive(@"D:\TIYE\Soft");

        }

        private async Task DownlowadUrl()
        {
            DownloadModel downloadModel = new DownloadModel();
            downloadModel.Url = "https://m.xueleyun.com/download/wisdom_class/cpp";
            downloadModel.Name = "AIClass";
            downloadModel.Cachepath = "D://360Downloads//" + downloadModel.Name + ".exe";
            //DownLoadService.GetInstance().DownLoad += MainWindow_DownloadEvent;
            await DownLoadService.GetInstance().DownLoad(downloadModel);
        }

        private void MainWindow_DownloadEvent(DownloadModel obj)
        {
            Dispatcher.Invoke(() =>
            {
                SetApp();
            });

        }
    }
}
