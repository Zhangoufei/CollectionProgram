using CefSharp;
using CefSharp.Wpf;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static string titleSplite = "theme?ACJ=%E7%A0%94%E8%AF%BB,theme,listen,student,hot,populars,activity";
        public MainWindow()
        {
            InitializeComponent();

            var settings = new CefSettings();
            settings.CefCommandLineArgs.Add("disable-gpu", "1");
            settings.CefCommandLineArgs.Add("force-device-scale-factor", "2");
            settings.CefCommandLineArgs["touch-events"] = "enabled";

            settings.CefCommandLineArgs.Add("proxy-auto-detect", "0");
            settings.CefCommandLineArgs.Add("no-proxy-server", "1");

            Cef.Initialize(settings);

          //  Cef.SetCookiePath("D://Tiye//", true);

            //brower = new CefSharp.Wpf.ChromiumWebBrowser();
            //grid.Children.Add(brower);

            Init();

            this.WindowState = WindowState.Maximized;
            brower.IsManipulationEnabled = false;
            brower.LifeSpanHandler = new CefSharpOpenPageSelf();

            brower.FrameLoadEnd += Brower_FrameLoadEnd;

            brower.FrameLoadStart += Brower_FrameLoadStart;

            //brower.Address = "http://www.zzbz.net/home/indexpc";

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

                    list.Add(stringBuilder.ToString());
                    //解析网页
                    brower.Address = stringBuilder.ToString();
                  
                }
            }
           

        }

        private void Brower_FrameLoadStart(object sender, FrameLoadStartEventArgs e)
        {
            string lastString = e.Url.Substring(e.Url.LastIndexOf("/") + 1);

            if (titleSplite.Contains(lastString) && !list.Contains(e.Url))
            {
                list.Add(e.Url);
            }
        }

        public static bool returnFalse = false;
        /// <summary>
        /// 退一步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //第一种退一步
            //if (list.Count > 1)
            //{
            //    list.Remove(list.Last());
            //}
            //brower.Load(list.Last());

            string lastString = brower.Address.Substring(brower.Address.LastIndexOf("/") + 1);
            //另一种退一步
            if (brower.CanGoBack && !lastString.Contains("rps")) {

                brower.Back();
            };
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

          

                brower.GetSourceAsync().ContinueWith(taskinfo =>
                {
                    ////string temp = taskinfo.Result;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CheckService checkService = new CheckService();
            checkService.Show();

        }



        public class CefSharpOpenPageSelf : ILifeSpanHandler
        {
            public bool DoClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
            {
                throw new System.NotImplementedException();
            }

            public void OnAfterCreated(IWebBrowser chromiumWebBrowser, IBrowser browser)
            {
                throw new System.NotImplementedException();
            }

            public void OnBeforeClose(IWebBrowser browser)
            {
            }

            public void OnBeforeClose(IWebBrowser chromiumWebBrowser, IBrowser browser)
            {
                throw new System.NotImplementedException();
            }

            public bool OnBeforePopup(IWebBrowser browser, string sourceUrl, string targetUrl, ref int x, ref int y, ref int width, ref int height)
            {
                var chromiumWebBrowser = (ChromiumWebBrowser)browser;

                if (!list.Contains(targetUrl))
                    list.Add(targetUrl);

                chromiumWebBrowser.Load(targetUrl);

                return true; //Return true to cancel the popup creation copyright by  codebye.com.

            }

            public bool OnBeforePopup(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
            {
                newBrowser = null;
                var chromiumWebBrowser2 = (ChromiumWebBrowser)browser;

                if (!list.Contains(targetUrl))
                    list.Add(targetUrl);
                chromiumWebBrowser2.Load(targetUrl);
                return true;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            brower.ShowDevTools();

          
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }



}
