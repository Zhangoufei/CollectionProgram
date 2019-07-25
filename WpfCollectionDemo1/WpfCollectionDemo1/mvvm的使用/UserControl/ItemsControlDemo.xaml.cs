using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfCollectionDemo1.mvvm的使用.ViewModel;

namespace WpfCollectionDemo1.mvvm的使用.UserControl
{
    /// <summary>
    /// ItemsControlDemo.xaml 的交互逻辑
    /// </summary>
    public partial class ItemsControlDemo : Page
    {
        public ItemsControlDemo()
        {
            InitializeComponent();

            DataContext = new ItemsControlDemoVm();


            ObservableCollection<Persons> persons = new ObservableCollection<Persons>
            {
                new Persons
                {
                    NameId="张三",
                    CurrPerson = new List<Person>
                    {
                        new Person{Name="Chrysanthemum", Age=21, Email="chrysanthemum@gmail.com", Image="Chrysanthemum.jpg"},
                        new Person{Name="Desert", Age=23, Email="Desert@gmail.com", Image="Desert.jpg"}
                    }
                },

                new Persons
                {
                         NameId="张三",
                    CurrPerson = new List<Person>
                    {
                        new Person{Name="Jellyfish", Age=32, Email="Jellyfish@gmail.com", Image="Jellyfish.jpg"},
                        new Person{Name="Hydrangeas", Age=23, Email="Hydrangeas@gmail.com", Image="Hydrangeas.jpg"}
                        }
                }
            };

            //   itemsControl.ItemsSource = persons;
        }
        private void ItemsControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        class Person
        {
            public string Name { get; set; }
            public int Age { set; get; }
            public string Image { set; get; }
            public string Email { set; get; }
        }

        class Persons
        {
            public string NameId { set; get; }
            public List<Person> CurrPerson { set; get; }
        }
    }
}
