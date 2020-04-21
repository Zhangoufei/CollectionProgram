﻿using CefSharp.Wpf;
using System.Text;
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
            setting.BrowserSubprocessPath = @"x86\CefSharp.BrowserSubprocess.exe";
            setting.CefCommandLineArgs.Add("proxy-auto-detect", "0");
            setting.CefCommandLineArgs.Add("no-proxy-server", "1");
            CefSharp.Cef.Initialize(setting, performDependencyCheck: true, browserProcessHandler: null);


            webView = new ChromiumWebBrowser();
            this.Content = webView;

            //webView.Address = "http://www.zzbz.net/home/indexpc";

            Init();
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
    }
}
