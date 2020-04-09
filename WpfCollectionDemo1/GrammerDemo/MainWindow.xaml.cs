using GrammerDemo.Model;
using System;
using System.Collections.Generic;
using System.Windows;

namespace GrammerDemo
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

            Tuple<A, B, string, List<string>> tuple = new Tuple<A, B, string, List<string>>(
                new A() { id = "Abc123", name = "张三" },
                new B() { id = 54, age = 30 },
                "王五",
                new List<string>() { "你好", "不好" }
                );
            MessageBox.Show(tuple.Item1.id + " name=" + tuple.Item1.name);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ThreadWindow threadWindow = new ThreadWindow();
            threadWindow.Show();
        }
    }

    class A
    {

        public string id { set; get; }

        public string name { set; get; }

    }
    class B
    {
        public int id { set; get; }

        public int age { set; get; }
    }
}
