using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace WpfCollectionDemo1.mvvm的使用.UserControl
{
    /// <summary>
    /// ListBox横向滚动条.xaml 的交互逻辑
    /// </summary>
    public partial class ListBox横向滚动条 : Page
    {

        int sum = 1;
        ObservableCollection<ListBoxTemp> listBoxTemps = new ObservableCollection<ListBoxTemp>();
        public ListBox横向滚动条()
        {
            InitializeComponent();

            for (int sum = 0; sum < 50; sum++)
            {
                ListBoxTemp box = new ListBoxTemp();
                box.TextBlock = "测试" + sum;
                listBoxTemps.Add(box);
            }
            listBoxTemp.ItemsSource = listBoxTemps;

        }


        class ListBoxTemp
        {
            public string TextBlock { set; get; }

        }

        private void ListBoxTemp_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer sv = e.OriginalSource as ScrollViewer;
            if (sv != null)
            {
             //   Console.WriteLine(e.VerticalOffset.ToString());
                if (IsHorizationScrollBarAtRight(sv))
                {
                    // vm.MsgTest = "已到底部";
                    MessageBox.Show("已到底部");

                    for (int i = 0; i < 20; i++)
                    {
                        ListBoxTemp box = new ListBoxTemp();
                        box.TextBlock = "测试添加新的" + (sum+i+1);
                        listBoxTemps.Add(box);
                    }
                   // listBoxTemp.ItemsSource = listBoxTemps;
                }
                else
                {
                    //  vm.MsgTest = "未到底部";
                   // MessageBox.Show("未到底部");

                }
            }
        }


        //判断到达底部
        public bool IsVerticalScrollBarAtButtom(ScrollViewer s)
        {

            bool isAtButtom = false;
            double dVer = s.VerticalOffset;
            double dViewport = s.ViewportHeight;
            double dExtent = s.ExtentHeight;
            //Console.WriteLine("dVer:" + dVer + " dViewport:" + dViewport + " dExtent:" + dExtent);
            if (dVer != 0)
            {
                if (dVer + dViewport == dExtent)
                {
                    isAtButtom = true;
                }
                else
                {
                    isAtButtom = false;
                }
            }
            else
            {
                isAtButtom = false;
            }
            if (s.VerticalScrollBarVisibility == ScrollBarVisibility.Disabled
                || s.VerticalScrollBarVisibility == ScrollBarVisibility.Hidden)
            {
                isAtButtom = true;
            }
            return isAtButtom;
        }


        //判断 滚动条是否到达最右边
        public bool IsHorizationScrollBarAtRight(ScrollViewer s)
        {
            bool isAtButtom = false;
            double dVer = s.HorizontalOffset;
            double dViewport = s.ViewportWidth;
            double dExtent = s.ExtentWidth;
            if (dVer != 0)
            {
                if (dVer + dViewport == dExtent)
                {
                    isAtButtom = true;
                }
                else
                {
                    isAtButtom = false;
                }
            }
            else
            {
                isAtButtom = false;
            }
            if (s.VerticalScrollBarVisibility == ScrollBarVisibility.Disabled
              || s.VerticalScrollBarVisibility == ScrollBarVisibility.Hidden)
            {
                isAtButtom = true;
            }
            return isAtButtom;
        }
    }
}
