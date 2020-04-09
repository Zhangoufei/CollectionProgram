using CefSharp;
using CefSharp.Wpf;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace TestCefMp4
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //ChromiumWebBrowser brower;
        public static List<string> list = new List<string>();
        public MainWindow()
        {
            InitializeComponent();

            var settings = new CefSettings()
            {
                BrowserSubprocessPath = "CefSharp.BrowserSubprocess.exe"
            }; ;
            settings.CefCommandLineArgs.Add("disable-gpu", "1");
            settings.CefCommandLineArgs.Add("force-device-scale-factor", "2");
            settings.CefCommandLineArgs["touch-events"] = "enabled";

            ////settings.AutoDetectProxySettings = false;
            //settings.PackLoadingDisabled = true;
            //settings.LogSeverity = LogSeverity.Disable;
            //settings.UserAgent = "";

            Cef.Initialize(settings);

            Cef.SetCookiePath("D://Tiye//", true);



            //brower = new ChromiumWebBrowser();


            this.WindowState = WindowState.Maximized;
            brower.IsManipulationEnabled = false;
            brower.LifeSpanHandler = new CefSharpOpenPageSelf();

            string url = "https://www.huidu.com/lzx/hnyx/eaindex?userId=001986030&client_id=20&client_secret=e169a0d2b72d5d74f8f1957305ca0126&access_token=7c432cfd-842c-44c4-a1c1-700f14e21c8f";
            list.Add(url);
            //解析网页
            brower.Address = url;

            brower.TouchDown += Brower_TouchDown;
            brower.TouchMove += Brower_TouchMove;
            brower.TouchUp += Brower_TouchUp;

            //grid.Children.Add(brower);

            brower.FrameLoadEnd += Brower_FrameLoadEnd;
        }


        private void Brower_TouchUp(object sender, System.Windows.Input.TouchEventArgs e)
        {

        }
        private void Brower_TouchMove(object sender, System.Windows.Input.TouchEventArgs e)
        {
            //brower.ExecuteScriptAsync("window.scrollTo(100,100);  ");
        }

        private void Brower_TouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {

        }



        /// <summary>
        /// 退一步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (list.Count > 1)
            {
                list.Remove(list.Last());
            }
            brower.Load(list.Last());
        }

        //注册一个事件，调出软键盘
        public class JsObj
        {
            public void ShowKeyBoard()
            {
                //Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.System) + System.IO.Path.DirectorySeparatorChar + "osk.exe");

            }
        }

        private void Brower_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {

            if (e.IsMainFrame)
            {

                brower.GetSourceAsync().ContinueWith(taskinfo =>
                {


                    string temp = taskinfo.Result;




                    ////添加css3样式
                    //string linkText = "<link rel=" + AppDomain.CurrentDomain.BaseDirectory + "//" + "StyleSheet1.css>";
                    //var jsFunctionText = string.Format("(function() {{ $('head').append('{0}'); return true;}}) ();", linkText);
                    //brower.ExecuteScriptAsync(jsFunctionText);


                    //string tempStyle=  ConfigurationManager.AppSettings["stringtemp"];
                    // var jsFunctionText = "(function() {}) ();";
                    // brower.ExecuteScriptAsync(jsFunctionText);
                });


                //将滚动条宽度变大

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CheckService checkService = new CheckService();
            checkService.Show();

        }



        public class CefSharpOpenPageSelf : ILifeSpanHandler
        {

            public void OnBeforeClose(IWebBrowser browser)
            {
            }

            public bool OnBeforePopup(IWebBrowser browser, string sourceUrl, string targetUrl, ref int x, ref int y, ref int width, ref int height)
            {
                var chromiumWebBrowser = (ChromiumWebBrowser)browser;
                list.Add(targetUrl);
                chromiumWebBrowser.Load(targetUrl);
                return true; //Return true to cancel the popup creation copyright by  codebye.com.
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            brower.ShowDevTools();
        }


    }



}
