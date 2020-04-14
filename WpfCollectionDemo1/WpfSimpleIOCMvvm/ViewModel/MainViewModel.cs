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


        #region ����
        /// <summary>
        /// ��һҳ
        /// </summary>
        public RelayCommand NavToPreUpContent { set; get; }
        /// <summary>
        /// ��һҳ
        /// </summary>
        public RelayCommand NavToPreNextContent { set; get; }

        /// <summary>
        /// ��һҳ
        /// </summary>
        public RelayCommand NavToPreUpPage { set; get; }
        /// <summary>
        /// ��һҳ
        /// </summary>
        public RelayCommand NavToPreNextPage { set; get; }
        #endregion

        #region Pageҳ��
        /// <summary>
        /// ��ҳFrame;
        /// </summary>
        private Frame m_MainWindowFrame;

        public Frame MainWindFrame
        {
            get { return m_MainWindowFrame; }
            set { Set(ref m_MainWindowFrame, value); }
        }

        /// <summary>
        /// ����ǩҳ;
        /// </summary>
        private Page m_MPage;
        public Page MPage
        {
            get { return m_MPage; }
            set { Set(ref m_MPage, value); }
        }

        /// <summary>
        /// ����ǩҳ;
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
            TextTitle = "��ð�";

            TextTitle2 = "��һ��֪ͨ��ʽ";

            NavToPreUpContent = new RelayCommand(NavUpPageContent);
            NavToPreNextContent = new RelayCommand(NavNextPageContent);

            NavToPreUpPage = new RelayCommand(NavUpPage);   // ��������һ��Pageҳ;
            NavToPreNextPage = new RelayCommand(NavNextPage);


            //Page��ֵ
            MPage = new MainPage1();

            AttachPage = new AttachPage2();

            MainWindFrame = new Frame();

            //��ҳ�����
            MainWindFrame.Content = MPage;
        }


        /// <summary>
        /// ��һҳ ����
        /// </summary>
        private void NavUpPageContent()
        {
            TextTitle = "�л���һҳ";
        }
        /// <summary>
        /// �����һҳ����;
        /// </summary>
        private void NavNextPageContent()
        {
            TextTitle = "�л���һҳ";
        }

        /// <summary>
        /// ��һ��ҳ��
        /// </summary>
        private void NavNextPage() {

            MainWindFrame.Content = MPage;
        }
        /// <summary>
        /// ��һ��ҳ��
        /// </summary>
        private void NavUpPage()
        {
            MainWindFrame.Content = AttachPage;
        }

    }
}