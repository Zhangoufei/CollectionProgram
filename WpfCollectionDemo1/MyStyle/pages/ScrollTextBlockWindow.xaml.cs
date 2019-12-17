using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace MyStyle.pages
{
    /// <summary>
    /// ScrollTextBlockWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ScrollTextBlockWindow : Window
    {


        public ScrollTextBlockWindow()
        {
            InitializeComponent();

            //scrollingTextControl.ItemsSource = new ObservableCollection<string>() {
            //    "消防安全直播通知：为进一步提高学生和老师消防安全防范意识和技能，营造良好的消防安全环境，接叶县教育局通知，需要所有学生和老师2019年9月11日上午九时，在直播间马庄直播教室观看“2019消防安全第一课”，感谢配合!"
            //};

        }

        private void NameTest_Click(object sender, RoutedEventArgs e)
        {
            scrollingTextControl.Add("消防安全直播通知：为进一步提高学生和老师消防安全防范意识和技能，营造良好的消防安全环境，接叶县教育局通知，需要所有学生和老师2019年9月11日上午九时，在直播间马庄直播教室观看“2019消防安全第一课”，感谢配合!");
        }
    }
}
