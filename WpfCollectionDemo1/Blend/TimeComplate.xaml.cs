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

namespace Blend
{
    /// <summary>
    /// TimeComplate.xaml 的交互逻辑
    /// </summary>
    public partial class TimeComplate : Page
    {
        public TimeComplate()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DateTime startTimeS = DateTime.Parse(startTime.Text);

            DateTime endTimeS = DateTime.Parse(endTime.Text);

            List<Weeks> weeks = NewMethod(startTimeS, endTimeS);

            dataGrid.ItemsSource = weeks;
        }

        private static List<Weeks> NewMethod(DateTime startTimeS, DateTime endTimeS)
        {
            //计算出总天数
            int days = (int)endTimeS.Subtract(startTimeS).TotalDays;

            int weekCount = days / 7 + 1;    //18周

            //周次 序号  0-6 当天是几
            int startNum = Convert.ToInt32(startTimeS.DayOfWeek);
            if (startNum == 0) startNum = 7;

            List<Weeks> weeks = new List<Weeks>();

            for (int i = 0; i < weekCount; i++)
            {
                Weeks week = new Weeks();

                if (i == 0)
                {
                    week.StartTime = startTimeS;

                    week.EndTime = startTimeS.AddDays(7 - startNum);
                }
                else
                {
                    week.StartTime = startTimeS.AddDays((7 - startNum + 1) + (i - 1) * 7);

                    week.EndTime = startTimeS.AddDays(7 - startNum + i * 7);
                }


                week.weekNum = (i + 1);
                weeks.Add(week);
            }

            return weeks;
        }

        public class Weeks
        {
            public DateTime StartTime { set; get; }

            public DateTime EndTime { set; get; }

            public int weekNum { set; get; }
        }

    }
}
