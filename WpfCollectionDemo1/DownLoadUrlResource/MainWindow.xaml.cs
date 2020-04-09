using HtmlAgilityPack;
using System.Windows;
using System.Windows.Controls;

namespace DownLoadUrlResource
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            textBox1.Text = "https://article.xuexi.cn/articles/video/index.html?art_id=5561421868522845299&read_id=c1d2b392-008c-46cf-8005-d189daad0ff8&ref_read_id=856dd952-a1cf-4add-a3b0-7f7e34079d23&reco_id=&mod_id=&cid=&source=share&study_style_id=video_default&spm=contents.video_page_detail.clst__0.dclk__9&spm_pre=contents.video_page_detail.clst__1.dclk__8";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string url = textBox1.Text.Trim();

           // url = "https://www.cnblogs.com/";

            //WebClient wc = new WebClient();//创建WebClient对象

            //Stream s = wc.OpenRead(url);
            //StreamReader sr = new StreamReader(s, Encoding.UTF8);//把流对象转换为                                                                                        StreamReader对象

            //string textmmm = sr.ReadToEnd();//把流转换为字符串并显示在文本框中

            //sr.Close();//关闭资源

            //s.Close();


            //WebBrowser webBrowser = new WebBrowser();
            //var web = new HtmlWeb();
            //var doc = web.Load(url);
            //HtmlNodeCollection html = doc.DocumentNode.SelectNodes("//*[@id='detail_sections_list']/ol/li[1]/a");






        }
    }
}
