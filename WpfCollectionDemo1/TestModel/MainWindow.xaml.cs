using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace TestModel
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //ObservableCollection<Persons> persons = new ObservableCollection<Persons>
            //{
            //    new Persons
            //    {
            //         School="小学",
            //        CurrPerson = new List<Person>
            //        {
            //            new Person{Name="Chrysanthemum", Age=21, Email="chrysanthemum@gmail.com", Image="Chrysanthemum.jpg"},
            //            new Person{Name="Desert", Age=23, Email="Desert@gmail.com", Image="Desert.jpg"}
            //        }
            //    },

            //    new Persons
            //    {
            //          School="中学",
            //        CurrPerson = new List<Person>
            //        {
            //            new Person{Name="Jellyfish", Age=32, Email="Jellyfish@gmail.com", Image="Jellyfish.jpg"},
            //            new Person{Name="Hydrangeas", Age=23, Email="Hydrangeas@gmail.com", Image="Hydrangeas.jpg"}
            //            }
            //    }
            //};

            //list1.ItemsSource = persons;

            Content = new 多层嵌套itemscontrol();
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
            public string School { set; get; }
            public List<Person> CurrPerson { set; get; }
        }
    }
}
