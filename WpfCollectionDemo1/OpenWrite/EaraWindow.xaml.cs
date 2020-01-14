using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace OpenWrite
{
    /// <summary>
    /// EaraWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EaraWindow : Window
    {
        public EaraWindow()
        {
            InitializeComponent();
        }

        private void StackPanel_Click(object sender, RoutedEventArgs e)
        {
            Button button = e.OriginalSource as Button;

            switch (button.Content)
            {
                case "画笔模式":
                    inkCanvase.EditingMode = InkCanvasEditingMode.Ink;
                    break;
                case "橡皮檫模式":
                    inkCanvase.EditingMode = InkCanvasEditingMode.EraseByPoint;
                    break;
                default:
                    break;
            }
        }
    }
}
