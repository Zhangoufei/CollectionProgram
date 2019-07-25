using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static WpfCollectionDemo1.mvvm的使用.UserControl.ListBox滚动条样式;

namespace WpfCollectionDemo1.mvvm的使用.UserControl
{
    /// <summary>
    /// Popup使用.xaml 的交互逻辑
    /// </summary>
    public partial class Popup使用 : Window
    {
        public Popup使用()
        {
            InitializeComponent();


            //Popup codePopup = new Popup();
            //TextBlock popupText = new TextBlock();
            //popupText.Text = "Popup Text";
            //popupText.Background = Brushes.LightBlue;
            //popupText.Foreground = Brushes.Blue;
            //codePopup.Child = popupText;


            List<Users> users = new List<Users>();
            users.Add(new Users()
            {
                UserName = "zhangsn",
                UserName2 = "历史",
                UserName3 = "玩忘"
            });
            users.Add(new Users()
            {
                UserName = "zhangsn",
                UserName2 = "历史",
                UserName3 = "玩忘"
            });
            users.Add(new Users()
            {
                UserName = "zhangsn",
                UserName2 = "历史",
                UserName3 = "玩忘"
            });

            listBox.ItemsSource = users;

        }

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            pop1.IsOpen = true;
        }
    }
}
