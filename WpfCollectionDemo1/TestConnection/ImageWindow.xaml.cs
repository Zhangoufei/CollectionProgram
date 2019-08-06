using System.Collections.Generic;
using System.Windows;

namespace TestConnection
{
    /// <summary>
    /// ImageWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ImageWindow : Window
    {
        public ImageWindow(List<string> listImage)
        {
            InitializeComponent();
            List<ImageControl> imageControls = new List<ImageControl>();
            for (int i = 0; i < 50; i++)
            {
                foreach (var item in listImage)
                {
                    ImageControl imageControl = new ImageControl(item);
                    imageControls.Add(imageControl);
                }
            }
           
           
            listBoxName.ItemsSource = imageControls;
        }
    }
}
