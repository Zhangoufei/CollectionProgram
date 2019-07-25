using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 磁盘操作
{
    /// <summary>
    /// Office打开.xaml 的交互逻辑
    /// </summary>
    public partial class Office打开 : Page
    {
        public Office打开()
        {
            InitializeComponent();
        }

        string path = AppDomain.CurrentDomain.BaseDirectory;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            process.StartInfo.FileName = "POWERPNT.EXE";
          //  string temp =@"""D:\12 3.ppt""";
         //   string temp = "\"d:\\12 3.ppt\"";
            string temp = "\"d:/12 3.ppt\"";
            //  temp= HttpUtility.HtmlAttributeEncode(temp);
            temp = temp.Replace("/",@"\");
            process.StartInfo.Arguments = temp;
            process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Maximized;
            process.Start();
        }
    }
}
