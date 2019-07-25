using System.Collections.Generic;
using System.Windows.Controls;

namespace MyStyle.pages
{
    /// <summary>
    /// ListBoxPage.xaml 的交互逻辑
    /// </summary>
    public partial class ListBoxPage : Page
    {
        public ListBoxPage()
        {
            InitializeComponent();


            List<Person> list = new List<Person>();

            for (int i = 0; i < 100; i++)
            {
                list.Add(new Person()
                {
                    Name = "张三"+i,
                    Age = 12,
                    Email = "23333",
                    Sex = 1
                });

            }
            listBoxDefualt.ItemsSource = list;

            myListBoxDefualt.ItemsSource = list;

            listBehavioer.ItemsSource = list;
        }


    }

    public class Person
    {


        public string Name { set; get; }

        public int Sex { set; get; }

        public string Email { set; get; }

        public int Age { set; get; }
    }
}
