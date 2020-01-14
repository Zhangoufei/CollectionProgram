using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace MystyleUserControl.控件
{
    /// <summary>
    /// UserContrl.xaml 的交互逻辑
    /// </summary>
    public partial class UserContrl : Window
    {
        public UserContrl()
        {
            InitializeComponent();
        }

        int sumClick = 0;
        private void RepeatButton_Click(object sender, RoutedEventArgs e)
        {
            sumClick++;
            textNum.Text = sumClick + "";
        }

        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleButton toggleButton = sender as ToggleButton;
            textStatus.Text = toggleButton.IsChecked + "";
        }

        private void StackPanel_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)e.OriginalSource;
            switch (radioButton.Name)
            {
                case "radioSingle":
                    lb.SelectionMode = SelectionMode.Single;
                    break;
                case "radioMultiple":
                    lb.SelectionMode = SelectionMode.Multiple;
                    break;
                case "radioExtended":
                    lb.SelectionMode = SelectionMode.Extended;
                    break;
                default:
                    break;
            }
        }
    }
}
