using IntelligentClass.ViewModel.model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using WpfCollectionDemo1.mvvm的使用.ViewModel;

namespace WpfCollectionDemo1.mvvm的使用.UserControl
{
    /// <summary>
    /// ListBox滚动条样式.xaml 的交互逻辑
    /// </summary>
    public partial class ListBox滚动条样式 : Page
    {

        ListBox滚动条样式VM listBoxVM = null;

        public ListBox滚动条样式()
        {
            InitializeComponent();

            List<Users> users = new List<Users>();
            for (int i = 0; i < 30; i++)
            {
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
            }
           
            UserList.ItemsSource = users;



            DataContext = listBoxVM = new ListBox滚动条样式VM();

        }

        public class Users
        {

            public string UserName { set; get; }
            public string UserName2 { set; get; }
            public string UserName3 { set; get; }
        }

        private void ListBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void listBoxTest_Drop(object sender, DragEventArgs e)
        {

        }

        private void listBoxTest_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {

            }
        }


        #region 获取所有控件子级元素的方法，返回该类型的List集合


        public static List<T> GetChildObjects<T>(DependencyObject obj) where T : FrameworkElement
        {
            DependencyObject child = null;
            List<T> childList = new List<T>();


            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);


                if (child is T)
                {
                    childList.Add((T)child);
                }
                childList.AddRange(GetChildObjects<T>(child));
            }
            return childList;
        }


        #endregion

        #region 内部滚动条滚动到底部后接着滚动外部滚动条

        int listboxdelta = 0;
        private void 控件名_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            List<ScrollViewer> scrollList = GetChildObjects<ScrollViewer>((ListBox)sender);//查找改控件内部的ScrollViewer集合
            ScrollViewer scroll = scrollList[0];
            double delta = e.Delta;
            double verticalOffset = scroll.VerticalOffset;
            if (delta < 0)
            {
                scroll.ScrollToVerticalOffset(verticalOffset += 25);
                if (IsVerticalScrollBarAtButtom(scroll))
                {

                    //List<ScrollViewer> parentscrollList = GetChildObjects<ScrollViewer>((ListBox滚动条样式)上一节控件的父级控件);
                    //ScrollViewer parentScroll = parentscrollList[0];
                    //double parentdelta = e.Delta;
                    //double parentverticalOffset = parentScroll.VerticalOffset;
                    //if (listboxdelta < 0)
                    //{
                    //    parentScroll.ScrollToVerticalOffset(parentverticalOffset += 25);
                    //}
                    //else
                    //{
                    //    parentScroll.ScrollToVerticalOffset(parentverticalOffset -= 25);
                    //}

                    for (int i = 0; i < 50; i++)
                    {
                        listBoxVM.SubjectNames.Add(new SubjectName()
                        {
                            SubjectClass = ("语" + listboxdelta) + "次数:" + i,
                            SubjectClassName = ("语文" + listboxdelta) + i,
                            subjectChinaLua = "yuwen",
                            SubjectClassImage = "IMAGE_canvas_subject_1",
                            SubjectClassNameColor = "#b5babf"
                        });
                    }
                    listboxdelta++;

                }
            }
            else
            {
                scroll.ScrollToVerticalOffset(verticalOffset -= 25);
            }
        }


        #endregion



        #region 判断滚动条是否到达最底部的代码 需要传进来一个ScrollViewer对象


        public bool IsVerticalScrollBarAtButtom(ScrollViewer s)
        {
            bool isAtButtom = false;
            double dVer = s.VerticalOffset;
            double dViewport = s.ViewportHeight;
            double dExtent = s.ExtentHeight;
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
        #endregion

        private void ListBoxTest_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void ListBoxTest_TouchMove(object sender, TouchEventArgs e)
        {
            List<ScrollViewer> scrollList = GetChildObjects<ScrollViewer>((ListBox)sender);//查找改控件内部的ScrollViewer集合
            ScrollViewer scroll = scrollList[0];
            //  double delta = e.Delta;
            double verticalOffset = scroll.VerticalOffset;
            //if (delta < 0)
            //{
            scroll.ScrollToVerticalOffset(verticalOffset += 25);
            if (IsVerticalScrollBarAtButtom(scroll))
            {
                MessageBox.Show(1 + "");
                //List<ScrollViewer> parentscrollList = GetChildObjects<ScrollViewer>((ListBox滚动条样式)上一节控件的父级控件);
                //ScrollViewer parentScroll = parentscrollList[0];
                //double parentdelta = e.Delta;
                //double parentverticalOffset = parentScroll.VerticalOffset;
                //if (listboxdelta < 0)
                //{
                //    parentScroll.ScrollToVerticalOffset(parentverticalOffset += 25);
                //}
                //else
                //{
                //    parentScroll.ScrollToVerticalOffset(parentverticalOffset -= 25);
                //}

                for (int i = 0; i < 50; i++)
                {
                    listBoxVM.SubjectNames.Add(new SubjectName()
                    {
                        SubjectClass = ("语" + listboxdelta) + "次数:" + i,
                        SubjectClassName = ("语文" + listboxdelta) + i,
                        subjectChinaLua = "yuwen",
                        SubjectClassImage = "IMAGE_canvas_subject_1",
                        SubjectClassNameColor = "#b5babf"
                    });
                }
                listboxdelta++;

            }
            //}
            //else
            //{
            //    scroll.ScrollToVerticalOffset(verticalOffset -= 25);
            //}
        }
    }
}
