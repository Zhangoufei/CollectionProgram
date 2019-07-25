using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Wpf动画
{
    /// <summary>
    /// 封面相册动画.xaml 的交互逻辑
    /// </summary>
    public partial class 封面相册动画 : Window
    {
        public 封面相册动画()
        {
            InitializeComponent();

            Loaded += 封面相册动画_Loaded;

        }

        private void 封面相册动画_Loaded(object sender, RoutedEventArgs e)
        {
        }


        public void Init(List<Uri> listUri)
        {

            CoverFlowMain.AddRange(listUri);

            CoverFlowMain.JumpTo(2);

        }
    }
}
