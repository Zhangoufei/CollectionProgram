using Com.Zhang.Common;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Media.Imaging;

namespace WpfCollectionDemo1.mvvm的使用.ViewModel
{



    public class EnlargementShrinkVM : ViewModelBase
    {

        private StrokeCollection inkCanvas = new StrokeCollection();

        public StrokeCollection InkCanvas
        {
            set
            {
                inkCanvas = value;
                RaisePropertyChanged("InkCanvas");
            }
            get { return inkCanvas; }
        }



        private BitmapImage bitMapImage;

        public BitmapImage BitMapImage
        {
            set
            {
                bitMapImage = value;
                RaisePropertyChanged("BitMapImage");
            }
            get { return bitMapImage; }
        }


        public ObservableCollection<PhoneImageShow> phoneImageShows = new ObservableCollection<PhoneImageShow>();

        public ObservableCollection<PhoneImageShow> PhoneImageShows
        {
            set
            {
                phoneImageShows = value;
            }
            get
            {
                return phoneImageShows;
            }
        }


        public EnlargementShrinkVM()
        {
            phoneImageShows.Add(new PhoneImageShow() { BitMapImage = new BitmapImage(new System.Uri("http://192.168.17.82/149b6179711f832e96e8fac422d80a4b/78C3FC75-D7D4-4777-A2F7-944CD388355F.jpg")) });
            phoneImageShows.Add(new PhoneImageShow() { BitMapImage = new BitmapImage(new System.Uri("http://192.168.17.82/04c4a7f4914e8a12e81b3c016387e898/19.jpg")) });
            phoneImageShows.Add(new PhoneImageShow() { BitMapImage = new BitmapImage(new System.Uri("http://192.168.17.82/1A3DFDE96A73E65F44A3834952300C58/-507060681.shuiguodazhanjiangshi.png")) });
            phoneImageShows.Add(new PhoneImageShow() { BitMapImage = new BitmapImage(new System.Uri("http://192.168.17.82/1B3850AE6EAF1AEAE9061FBC62BA8700/Screenshot_20191010_135459_com.yx.teacher.jpg")) });
            phoneImageShows.Add(new PhoneImageShow() { BitMapImage = new BitmapImage(new System.Uri("http://192.168.17.82/0F3BD1EB48853FE2885A9B48EEB704C4/lADPDgQ9q10JxbLNBgDNBMw_1228_1536.jpg_620x10000q90g.jpg")) });

            BitMapImage = phoneImageShows.FirstOrDefault().BitMapImage;
        }
        public BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap)
        {
            BitmapImage bitmapImage = new BitmapImage();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                bitmap.Save(ms, bitmap.RawFormat);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
        }

        /// <summary>
        /// 截图转换成bitmap
        /// </summary>
        /// <param name="element"></param>
        /// <param name="width">默认控件宽度</param>
        /// <param name="height">默认控件高度</param>
        /// <param name="x">默认0</param>
        /// <param name="y">默认0</param>
        /// <returns></returns>
        public static Bitmap ToBitmap(FrameworkElement element, int width = 0, int height = 0, int x = 0, int y = 0)
        {
            if (width == 0) width = (int)element.ActualWidth;
            if (height == 0) height = (int)element.ActualHeight;

            var rtb = new RenderTargetBitmap(width, height, x, y, System.Windows.Media.PixelFormats.Default);
            rtb.Render(element);
            var bit = BitmapSourceToBitmap(rtb);

            return bit;
        }


        /// <summary>
        /// BitmapSource转Bitmap
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Bitmap BitmapSourceToBitmap(BitmapSource source)
        {
            return BitmapSourceToBitmap(source, source.PixelWidth, source.PixelHeight);
        }

        /// <summary>
        /// Convert BitmapSource to Bitmap
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Bitmap BitmapSourceToBitmap(BitmapSource source, int width, int height)
        {
            Bitmap bmp = null;
            try
            {
                PixelFormat format = PixelFormat.Format24bppRgb;
                /*set the translate type according to the in param(source)*/
                switch (source.Format.ToString())
                {
                    case "Rgb24":
                    case "Bgr24": format = PixelFormat.Format24bppRgb; break;
                    case "Bgra32": format = PixelFormat.Format32bppPArgb; break;
                    case "Bgr32": format = PixelFormat.Format32bppRgb; break;
                    case "Pbgra32": format = PixelFormat.Format32bppArgb; break;
                }
                bmp = new Bitmap(width, height, format);
                BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size),
                    ImageLockMode.WriteOnly,
                    format);
                source.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
                bmp.UnlockBits(data);
            }
            catch
            {
                if (bmp != null)
                {
                    bmp.Dispose();
                    bmp = null;
                }
            }

            return bmp;
        }











    }

    //界面显示属性
    public class PhoneImageShow : ViewModelBase
    {
        private string createTime;

        public string CreateTime
        {
            set
            {
                createTime = value;
                RaisePropertyChanged("CreateTime");
            }
            get { return createTime; }
        }

        private string url;

        public string Url
        {
            set
            {
                url = value;
                RaisePropertyChanged("Url");
            }
            get { return url; }
        }

        private BitmapImage bitMapImage;

        public BitmapImage BitMapImage
        {
            set
            {
                bitMapImage = value;
                RaisePropertyChanged("BitMapImage");
            }
            get { return bitMapImage; }
        }

        private StrokeCollection strokesVm = new StrokeCollection();


        public StrokeCollection StrokesVm
        {
            set
            {
                strokesVm = value;
                RaisePropertyChanged("StrokesVm");
            }
            get { return strokesVm; }
        }

    }


}
