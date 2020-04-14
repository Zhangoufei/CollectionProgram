using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Controls;
using WpfSimpleIOCMvvm.Pages;

namespace WpfSimpleIOCMvvm.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private string textTitle;

        public string TextTitle
        {
            set
            {
                textTitle = value;
                RaisePropertyChanged("TextTitle");
            }
            get { return textTitle; }
        }

        private string textTitle2;

        public string TextTitle2
        {
            set
            {
                Set(ref textTitle2, value);
            }
            get { return textTitle2; }
        }


        #region 命令
        /// <summary>
        /// 上一页
        /// </summary>
        public RelayCommand NavToPreUpContent { set; get; }
        /// <summary>
        /// 下一页
        /// </summary>
        public RelayCommand NavToPreNextContent { set; get; }

        /// <summary>
        /// 上一页
        /// </summary>
        public RelayCommand NavToPreUpPage { set; get; }
        /// <summary>
        /// 下一页
        /// </summary>
        public RelayCommand NavToPreNextPage { set; get; }
        #endregion

        #region Page页面
        /// <summary>
        /// 主页Frame;
        /// </summary>
        private Frame m_MainWindowFrame;

        public Frame MainWindFrame
        {
            get { return m_MainWindowFrame; }
            set { Set(ref m_MainWindowFrame, value); }
        }

        /// <summary>
        /// 主标签页;
        /// </summary>
        private Page m_MPage;
        public Page MPage
        {
            get { return m_MPage; }
            set { Set(ref m_MPage, value); }
        }

        /// <summary>
        /// 附标签页;
        /// </summary>
        private Page m_AttachPage;
        public Page AttachPage
        {
            get { return m_AttachPage; }
            set { Set(ref m_AttachPage, value); }
        }
        #endregion

        public MainViewModel()
        {
            TextTitle = "你好啊";

            TextTitle2 = "另一种通知方式";

            NavToPreUpContent = new RelayCommand(NavUpPageContent);
            NavToPreNextContent = new RelayCommand(NavNextPageContent);

            NavToPreUpPage = new RelayCommand(NavUpPage);   // 导航到下一个Page页;
            NavToPreNextPage = new RelayCommand(NavNextPage);


            //Page赋值
            MPage = new MainPage1();

            AttachPage = new AttachPage2();

            MainWindFrame = new Frame();

            //主页面设计
            MainWindFrame.Content = MPage;
        }


        /// <summary>
        /// 上一页 内容
        /// </summary>
        private void NavUpPageContent()
        {
            TextTitle = "切换上一页";
        }
        /// <summary>
        /// 点击下一页内容;
        /// </summary>
        private void NavNextPageContent()
        {
            TextTitle = "切换下一页";
        }

        /// <summary>
        /// 下一个页面
        /// </summary>
        private void NavNextPage() {

            MainWindFrame.Content = MPage;
        }
        /// <summary>
        /// 上一个页面
        /// </summary>
        private void NavUpPage()
        {
            MainWindFrame.Content = AttachPage;
        }

    }
}