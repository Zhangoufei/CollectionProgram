using EFSqlite.DbModel;
using EFSqlite.EntityFrameWork;
using EFSqlite.UserControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

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

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            List<Student> books = new List<Student>();

            for (int i = 0; i < 100; i++)
            {
                var list = new List<Student>(){
                    new Student() { Name = "小明"+i, Age = 11, Gender = "男",CreateTime=DateTime.Now },
                    new Student() { Name = "老王"+i,  Age = 12, Gender = "男" ,CreateTime=DateTime.Now },
                    new Student() { Name = "小郭"+i, Age = 10, Gender = "女" ,CreateTime=DateTime.Now },
                    new Student() { Name = "小李"+i,  Age = 11, Gender = "男" ,CreateTime=DateTime.Now },
                    new Student() { Name = "小玉"+i,  Age = 9, Gender = "女" ,CreateTime=DateTime.Now },
                };
                books.AddRange(list);
            }

            using (var db = new StudentContext())
            {
                db.Students.AddRange(books);
                int count = await db.SaveChangesAsync();
                textBlock.Text = $"{DateTime.Now}, 插入{count}条记录";
            }
            QueryButton_Click(null, null);  //刷新下数据

        }

        private void QueryButton_Click(object sender, RoutedEventArgs e)
        {

            using (var db = new StudentContext())
            {
                var students = db.Students.AsQueryable().OrderByDescending(p => p.ID).ToList();

                listData.ItemsSource = students;

            }

        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (listData.SelectedIndex != -1)
            {
                var temp = listData.SelectedValue as Student;

                UpdateWindow updateWindow = new UpdateWindow(temp);
                updateWindow.Owner = this;
                this.Background = new SolidColorBrush(Colors.Gray);
                updateWindow.Closed += (senders, ee) =>
                {
                    this.Background = new SolidColorBrush(Colors.White);
                    QueryButton_Click(null, null);  //刷新下数据
                };
                updateWindow.Show();

            }
        }

        private StudentContext studentContext = new StudentContext();
        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (listData.SelectedIndex != -1)
            {
                var temp = listData.SelectedValue as Student;


                //第一种删除   操作数据库两次  不太好
                //using (var db = new StudentContext())
                //{
                //    var delteId = db.Students.FirstOrDefault(p => p.ID == temp.ID);

                //    db.Students.Remove(delteId);

                //    await db.SaveChangesAsync();

                //    textBlock.Text = $"{DateTime.Now}, 删除1条记录";
                //}

                Student student = new Student() { ID = temp.ID };
                //第二种
                studentContext.Students.Attach(student);   //附加到ef中
                studentContext.Students.Remove(student);

                //第三种
                // DbEntityEntry<Student> entity = studentContext.Entry(student);   //将对象加入到ef容器中
                //entity.State = System.Data.Entity.EntityState.Deleted;
                var result = await studentContext.SaveChangesAsync();
                textBlock.Text = $"{DateTime.Now}, 删除{result}条记录";




                QueryButton_Click(null, null);  //刷新下数据

            }
        }
    }
}
