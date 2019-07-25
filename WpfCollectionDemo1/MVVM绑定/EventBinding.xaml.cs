using MVVM绑定.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MVVM绑定
{
    /// <summary>
    /// EventBinding.xaml 的交互逻辑
    /// </summary>
    public partial class EventBinding : Page
    {
        public EventBinding()
        {
            InitializeComponent();

            List<Users> users = new List<Users>();
            for (int i = 0; i < 30; i++)
            {
                users.Add(new Users()
                {
                    UserName = "zhangsn"+i,
                    UserName2 = "历史"+i,
                    UserName3 = "玩忘"+i
                });
            }
            listBox.ItemsSource = users;


            DataContext = new EventBindingVm();
        }


       
    }
}
