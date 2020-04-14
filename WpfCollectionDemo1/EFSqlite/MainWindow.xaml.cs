using EFSqlite.EntityFrameWork;
using System;
using System.Data.SQLite;
using System.Windows;

namespace EFSqlite
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            using (var context = new MyContext(new SQLiteConnection(@"D:\DataBase\Note.DB")))
            {
                context.SaveChanges();
            }

        }



    }
}
