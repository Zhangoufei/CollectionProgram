using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace 多线程类型
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(Cal)) { IsBackground = true };
            thread.Start();

        }

        public static void Cal()
        {
            int sum = 0;
            for (int i = 0; i < 999; i++)
            {
                sum = sum + i;
            }
            Console.WriteLine($"sum={sum}");
        }

        public static int Cal2()
        {
            int sum = 0;
            for (int i = 0; i < 999; i++)
            {
                sum = sum + i;
            }
            Console.WriteLine($"sum={sum}");
            return sum;
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Task<int> result = CalAsync();
            Console.WriteLine(result);

        }

        public async Task<int> CalAsync()
        {
            int result = await Task.Run(new Func<int>(Cal2));
            return result;
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //Task<int> result = CallAsyncTest();
            //Console.WriteLine(result);

            int result = await Task.Run(() => Cal3(5000000));
            MessageBox.Show(result + "");

        }

        public async Task<int> CallAsyncTest()
        {
            int result = await Task.Run(() => Cal3(100));
            return result;
        }

        public static int Cal3(int temp)
        {
            int sum = 0;
            for (int i = 0; i < temp; i++)
            {
                sum = sum + i;
            }
            Console.WriteLine($"sum={sum}");
            return sum;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            LoadFile();
        }

        private async static void LoadFile()
        {
            string loadPath = "http://192.168.17.82/5fadd2cbd7339345c0f9f495ec11a51d/救世超能：永无止.mp4";

            string pathLoadl = @"D:\picture";


            using (WebClient client = new WebClient())
            {
                try
                {
                    await Task.Run(() =>
                           {
                               client.DownloadFileAsync(new Uri(loadPath), pathLoadl + "/" + DateTime.Now.ToString("yyyy-MM-ddHHmmss") + ".mp4");
                           });

                }
                catch (Exception)
                {
                }
            }

            int result = 0;
            for (int i = 0; i < 200; i++)
            {
                result += i;
            }
            MessageBox.Show(result + "");
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {

        }

        private async void Button_Click_5(object sender, RoutedEventArgs e)
        {

            int result = await Task.Run(() => GetResult());

            MessageBox.Show(result + "");


            MessageBox.Show(result + "12222");

        }

        private int GetResult()
        {

            int reuslt = 0;

            for (int i = 0; i < 100000000; i++)
            {
                reuslt += i;
            }
            Thread.Sleep(30000);
            return reuslt;
        }

        /// <summary>
        /// 请求http接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click_6(object sender, RoutedEventArgs e)
        {
            string temp = await HttpRestFul.GetInstance().getClassList();

            MessageBox.Show(temp);
        }
    }
}
