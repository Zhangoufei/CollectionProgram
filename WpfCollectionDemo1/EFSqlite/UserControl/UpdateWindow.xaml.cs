using EFSqlite.DbModel;
using EFSqlite.EntityFrameWork;
using System.Windows;

namespace EFSqlite.UserControl
{
    /// <summary>
    /// UpdateWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UpdateWindow : Window
    {
        public UpdateWindow(Student student)
        {
            InitializeComponent();

            txtId.Text = student.ID.ToString();
            txtAge.Text = student.Age + "";
            txtUserName.Text = student.Name;
            cobSex.Text = student.Gender;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {

            Student student = new Student();
            student.ID = int.Parse(txtId.Text);
            student.Age = int.Parse(txtAge.Text);
            student.Name = txtUserName.Text;
            student.Gender = cobSex.Text;

            using (var db = new StudentContext())
            {
                var result = db.Entry(student);
                result.State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
            }
            this.Close();

        }
    }
}
