using System.Windows;
using System.Windows.Interop;

namespace 嵌入第三方应用程序
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

            try
            {
                PDFwindow pDFwindow = new PDFwindow(@"D:\Program Files\Notepad++\notepad++.exe");
                //pDFwindow.ShowDialog();
                pDFwindow.Show();
            }
            catch (System.Exception)
            {

            }
          
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            //Chrom chrom = new Chrom();
            //chrom.Show();


            var hostWinHandle = ((HwndSource)PresentationSource.FromVisual(border)).Handle;
            var plugin = new Chrom(@"D:\Program Files\Notepad++\notepad++.exe", hostWinHandle);
            //   border.Content = plugin;
            Content = plugin;
            plugin.EmbedProcess((int)border.Width, (int)border.Height);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            WindowTestProgram windowTestProgram = new WindowTestProgram();
            windowTestProgram.Show();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            PDFwindow pDFwindow = new PDFwindow(@"D:\Program Files (x86)\TE Desktop\TE_Desktop.exe");
            pDFwindow.Show();
        }
    }
}
