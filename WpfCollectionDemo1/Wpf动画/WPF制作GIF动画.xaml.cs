using System;
using System.Windows;
using System.Windows.Threading;
using Wpf动画.ViewModel;

namespace Wpf动画
{
    /// <summary>
    /// WPF制作GIF动画.xaml 的交互逻辑
    /// </summary>
    public partial class WPF制作GIF动画 : Window
    {


        private WPFtoGIFVM wPFtoGIFVM = null;

        public WPF制作GIF动画()
        {
            InitializeComponent();

            DataContext = wPFtoGIFVM = new WPFtoGIFVM();

            Init();
        }


        private void Init()
        {
            DispatcherTimer dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(20);
            dispatcherTimer.Start();
        }


        public int Number = 0;
        private void DispatcherTimer_Tick(object sender, System.EventArgs e)
        {
            if (Number == wPFtoGIFVM.ListImage.Count)
            {
                Number = 0;
            }

            wPFtoGIFVM.ImageGif = wPFtoGIFVM.ListImage[Number];

            Number++;
        }
    }
}
