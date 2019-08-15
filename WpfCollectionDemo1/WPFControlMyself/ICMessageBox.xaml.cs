using System.Windows;

namespace CommonCtrls
{
    /// <summary>
    /// ICMessageBox.xaml 的交互逻辑
    /// </summary>
    public partial class ICMessageBox : Window
    {
        MessageBoxButton buttonType = MessageBoxButton.OK;
        public MessageBoxResult ReturnResult = MessageBoxResult.OK;

        public ICMessageBox(string strContent)
        {
            InitializeComponent();
            content.Text = strContent;
           // Style = FindResource("ShadowWindow") as Style;
        }

        ICMessageBox(string strContent, string strTitle, MessageBoxButton btType)
        {
            InitializeComponent();
            content.Text = strContent;
           // Style = FindResource("ShadowWindow") as Style;


            this.title.Text = strTitle;
            buttonType = btType;
        }

        static public MessageBoxResult Show(string strContent)
        {
            var msgWnd = new ICMessageBox(strContent);
            msgWnd.Focusable = true;
            msgWnd.Focus();

            msgWnd.ShowDialog();

            return msgWnd.ReturnResult;
        }

        static public MessageBoxResult Show(string strContent, string strTitle, MessageBoxButton btType)
        {
            var msgWnd = new ICMessageBox(strContent, strTitle, btType);

            switch (btType)
            {
                case MessageBoxButton.OK:
                    break;

                case MessageBoxButton.OKCancel:
                    msgWnd.buttonCancel.Visibility = Visibility.Visible;
                    break;

                case MessageBoxButton.YesNo:
                    msgWnd.buttonOK.Visibility = Visibility.Collapsed;
                    msgWnd.buttonYES.Visibility = Visibility.Visible;
                    msgWnd.buttonNO.Visibility = Visibility.Visible;
                    break;

                case MessageBoxButton.YesNoCancel:
                    msgWnd.buttonOK.Visibility = Visibility.Collapsed;
                    msgWnd.buttonYES.Visibility = Visibility.Visible;
                    msgWnd.buttonNO.Visibility = Visibility.Visible;
                    msgWnd.buttonCancel.Visibility = Visibility.Visible;
                    break;
            }

            msgWnd.ShowDialog();

            return msgWnd.ReturnResult;
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            ReturnResult = MessageBoxResult.OK;
            Close();
        }

        private void buttonYES_Click(object sender, RoutedEventArgs e)
        {
            ReturnResult = MessageBoxResult.Yes;
            Close();
        }

        private void buttonNO_Click(object sender, RoutedEventArgs e)
        {
            ReturnResult = MessageBoxResult.No;
            Close();
        }

        private void buttonCancle_Click(object sender, RoutedEventArgs e)
        {
            ReturnResult = MessageBoxResult.Cancel;
            Close();
        }

        private void ButtonOK_TouchDown(object sender, System.Windows.Input.TouchEventArgs e)
        {

        }
    }
}
