using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Linq;
namespace TestConnection
{
    /// <summary>
    /// ImageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ImageWindow : Window
    {

        private ObservableCollection<ImageControl> informalClassPhotoBoxControls = new ObservableCollection<ImageControl>();

        public ImageWindow(List<string> listImage)
        {
            InitializeComponent();
            listBoxName.ItemsSource = informalClassPhotoBoxControls;

            List<ImageControl> imageControls = new List<ImageControl>();

            foreach (var item in listImage)
            {
                ImageControl imageControl = new ImageControl(item);
                informalClassPhotoBoxControls.Add(imageControl);
            }

        }

        //添加图片
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string temp7 = "22318f0b1b1f570dc33591a1968e10b0/4CBA5205-66B9-4266-8405-1606A002B6A1.jpg";
            string testUrl = "http://192.168.17.82/" + temp7;

            ImageControl imageControl = new ImageControl(testUrl);

            informalClassPhotoBoxControls.Insert(0, imageControl);

            //滚动到底部
            //List<ScrollViewer> scrollViewers = GetChildObjects<ScrollViewer>(listBoxName);
            //ScrollViewToVerticalTop(listBoxName, scrollViewers[0]);

        }

        /// <summary>
        /// 垂直方向滚动到顶部
        /// </summary>
        /// <param name="element"></param>
        /// <param name="scrollViewer"></param>
        public static void ScrollViewToVerticalTop(FrameworkElement element, ScrollViewer scrollViewer)
        {
            //var scrollViewerOffset = scrollViewer.VerticalOffset;
            var scrollViewerOffset = scrollViewer.ExtentHeight ;
            var point = new Point(0, scrollViewerOffset);
            var tarPos = element.TransformToVisual(scrollViewer).Transform(point);
            scrollViewer.ScrollToVerticalOffset(tarPos.Y);

        }

        /// <summary>  
        /// 获得指定元素的所有子元素  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="obj"></param>  
        /// <returns></returns>  
        public List<T> GetChildObjects<T>(DependencyObject obj) where T : FrameworkElement
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
    }
}
