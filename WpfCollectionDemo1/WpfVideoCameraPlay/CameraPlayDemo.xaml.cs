using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFMediaKit.DirectShow.Controls;

namespace WpfVideoCameraPlay
{
    /// <summary>
    /// CameraPlayDemo.xaml 的交互逻辑
    /// </summary>
    public partial class CameraPlayDemo : Window
    {
        public CameraPlayDemo()
        {
            InitializeComponent();

            //  cb.ItemsSource = MultimediaUtil.VideoInputNames;//获得所有摄像头
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            cameraCaptureElement.VideoCaptureDevice = MultimediaUtil.VideoInputDevices[0];

        }

        int tempsum = 0;
        protected void OnVideoCaptureSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                tempsum++;
                test.Text += tempsum + "";
            });

        }


        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            //抓取控件做成图片
            RenderTargetBitmap bmp = new RenderTargetBitmap(
            (int)cameraCaptureElement.ActualWidth, (int)cameraCaptureElement.ActualHeight,
            96, 96, PixelFormats.Default);
            bmp.Render(cameraCaptureElement);
            BitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                byte[] captureData = ms.ToArray();
                //保存图片
                File.WriteAllBytes("D:/1.jpg", captureData);
            }
        }
    }
}
