using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using ZXing;
using ZXing.Common;

namespace WpfCollectionDemo1
{
    /// <summary>
    /// QrocodeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class QrocodeWindow : Window
    {
        public QrocodeWindow()
        {
            InitializeComponent();

            GeneratorBar(txtbox.Text);
            GeneratorQR(txtbox.Text);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            imageQR.Source = null;
            try
            {
                GeneratorQR(txtbox.Text);
            }
            catch (Exception ex)
            {
                txtbox.Text = ex.Message;
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            imageBar.Source = null;
            try
            {
                GeneratorBar(txtbox.Text);
            }
            catch (Exception ex)
            {
                txtbox.Text = ex.Message + "(code39合法字符集 [0-9A-Z+-*/%. ] 共43个)";
            }
        }

        // 生成二维码
        private System.Drawing.Image GeneratorQR(string msg)
        {
            BarcodeWriter writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE
            };
            writer.Options.Hints.Add(EncodeHintType.CHARACTER_SET, "UTF-8");     // 编码问题
            writer.Options.Hints.Add(EncodeHintType.ERROR_CORRECTION, ZXing.QrCode.Internal.ErrorCorrectionLevel.H);
            int codeSizeInPixels = 256;      // 设置图片长宽
            writer.Options.Height = codeSizeInPixels;
            writer.Options.Width = codeSizeInPixels;
            writer.Options.Margin = 0;       // 设置边框
            BitMatrix bm = writer.Encode(msg);
            Bitmap img = writer.Write(bm);
            imageQR.Source = BitmapToBitmapImage(img);
            return img;
        }

        // 生成条形码
        private System.Drawing.Image GeneratorBar(string msg)
        {
            MultiFormatWriter mutiWriter = new MultiFormatWriter();
            BitMatrix bm = mutiWriter.encode(msg, BarcodeFormat.CODE_39, 350, 256);
            Bitmap img = new BarcodeWriter().Write(bm);
            imageBar.Source = BitmapToBitmapImage(img);
            return img;
        }

        // Bitmap --> BitmapImage
        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }

        // BitmapImage --> Bitmap
        public static Bitmap BitmapImageToBitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new Bitmap(outStream);
                return new Bitmap(bitmap);
            }
        }

     
    }
}
