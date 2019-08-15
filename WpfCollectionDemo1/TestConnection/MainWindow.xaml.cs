using System.Collections.Generic;
using System.Windows;
using TestConnection.app;

namespace TestConnection
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

        private async void Open_Click(object sender, RoutedEventArgs e)
        {

            LastTest lastTest = await HttpRestFul.GetInstance().getClassList(sercretKey.Text.Trim(), userId.Text.Trim(), classId.Text.Trim());

            var tempresult = lastTest.data;
            if (tempresult == null || tempresult.imgUrlList == null)
            {
                MessageBox.Show("数据为:");
            }


            List<string> list = tempresult.imgUrlList;

            ImageWindow imageWindow = new ImageWindow(list);
            imageWindow.Show();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            userId.Text = "";
            classId.Text = "";
            sercretKey.Text = "";
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            string temp1, temp2, temp3, temp4, temp5, temp6, temp7;
            temp1 = "0a1c9e5ef4c98606b281166eb46060e7/Jellyfish.jpg";
            temp2 = "0c04f79096806e43c864fb703c50a8cf/fac8f90f2ece420aa73d92a82e7d5f88-19.jpg";
            temp3 = "1087bf98d131f4fd96647bba6ac5fb56/弹性势能.jpg";
            temp4 = "154289b0f58c8150810931b070db99d6/魔幻哈哈镜.jpg";
            temp5 = "1a712a12c7e566fddac3a8852aa6c33c/醉花阴.jpg";
            temp6 = "1db5cb9cfe8972af17cea8f5990fa90c/3.jpg";
            temp7 = "22318f0b1b1f570dc33591a1968e10b0/4CBA5205-66B9-4266-8405-1606A002B6A1.jpg";

            string testUrl = "http://192.168.17.82/";
            List<string> list = new List<string>();

            list.Add(testUrl + temp1);
            list.Add(testUrl + temp2);
            list.Add(testUrl + temp3);
            list.Add(testUrl + temp4);
            list.Add(testUrl + temp5);
            list.Add(testUrl + temp6);
            list.Add(testUrl + temp7);

            ImageWindow imageWindow = new ImageWindow(list);
            imageWindow.Show();
        }
    }
}
