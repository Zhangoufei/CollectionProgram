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

namespace MVVM绑定
{
    /// <summary>
    /// ListBoxWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ListBoxWindow : Window
    {
        public ListBoxWindow()
        {
            InitializeComponent();


            List<ListBoxItemControl> listBoxItemControls = new List<ListBoxItemControl>();
            for (int i = 0; i < 100; i++)
            {
                ListBoxItemControl item = new ListBoxItemControl();
                listBoxItemControls.Add(item);
            }

            listBox.ItemsSource = listBoxItemControls;

        }
    }
}
