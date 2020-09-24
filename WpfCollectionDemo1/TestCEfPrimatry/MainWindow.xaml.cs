using CefSharp.Wpf;
using System;
using System.Text;
using System.Threading;
using System.Windows;

namespace TestCEfPrimatry
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private ChromiumWebBrowser webView;
        public MainWindow()
        {

            InitializeComponent();

            var setting = new CefSettings();
            // 设置语言
            setting.Locale = "zh-CN";
            //cef设置userAgent
            setting.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36";
            //配置浏览器路径
            setting.BrowserSubprocessPath = AppDomain.CurrentDomain.BaseDirectory + @"x86\CefSharp.BrowserSubprocess.exe";
            setting.CefCommandLineArgs.Add("proxy-auto-detect", "0");
            setting.CefCommandLineArgs.Add("no-proxy-server", "1");
            CefSharp.Cef.Initialize(setting, performDependencyCheck: true, browserProcessHandler: null);

            //webview.PreviewTextInput += (obj, args) =>
            //{
            //    foreach (var character in args.Text)
            //    {
            //        // 把每个字符向浏览器组件发送一遍
            //        webview.GetBrowser().GetHost().SendKeyEvent((int)WM.CHAR, (int)character, 0);
            //    }

            //    // 不让cef自己处理
            //    args.Handled = true;
            //};

            webView = new ChromiumWebBrowser();
            grid.Children.Add(webView);
            // webView.Address = "http://yw.chinatiye.cn:8000/#/index";

            // Init();


        }

        private async void Init()
        {
            string userId = "001986030";
            string passwd = "tiye@123";
            string cliend_Id = "20";
            string client_secret = "e169a0d2b72d5d74f8f1957305ca0126";
            string grant_type = "client_credentials";
            //验证用户合法性
            string userCheckResult = await HttpService.CheckUserAndPassword(userId, passwd, cliend_Id, client_secret);

            if (userCheckResult.Contains("true"))
            {
                SecretClientModel secretClientModel = await HttpService.GetAccesssToken(cliend_Id, client_secret, grant_type);

                if (!string.IsNullOrEmpty(secretClientModel.access_token))
                {
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append("https://www.huidu.com/lzx/hnyx/eaindex");
                    stringBuilder.Append("?userId=" + userId);
                    stringBuilder.Append("&client_id=" + cliend_Id);
                    stringBuilder.Append("&client_secret=" + client_secret);
                    stringBuilder.Append("&access_token=" + secretClientModel.access_token);

                    //解析网页
                    webView.Address = stringBuilder.ToString();

                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            webView.Address = txtBox.Text.Trim();
        }
    }
}
