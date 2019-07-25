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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyStyle.pages
{
    /// <summary>
    /// ScrollViewerPage.xaml 的交互逻辑
    /// </summary>
    public partial class ScrollViewerPage : Page
    {
        public ScrollViewerPage()
        {
            InitializeComponent();


            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 100; i++)
            {
                stringBuilder.Append("添加文字描述"+i);
            }
            textData.Text = stringBuilder.ToString();

            textData2.Text= stringBuilder.ToString();

            List<Person> list = new List<Person>();

            for (int i = 0; i < 10; i++)
            {
                list.Add(new Person()
                {
                    Name = "张三",
                    Age = 12,
                    Email = "23333",
                    Sex = 1
                });
            }
            itemsControl.ItemsSource = list;



            var brushes = from property in typeof(Brushes).GetProperties()
                          let value = property.GetValue(null)
                          select value;

            itemsControl.ItemsSource = brushes.Take(100).ToArray();
        }

        //protected override Size ArrangeOverride(Size finalSize)
        //{
        //    var size = base.ArrangeOverride(finalSize);
        //    if (ScrollOwner != null)
        //    {
        //        var yOffsetAnimation = new DoubleAnimation() { To = -VerticalOffset, Duration = TimeSpan.FromSeconds(0.3) };
        //        _transForm.BeginAnimation(TranslateTransform.YProperty, yOffsetAnimation);

        //        var xOffsetAnimation = new DoubleAnimation() { To = -HorizontalOffset, Duration = TimeSpan.FromSeconds(0.3) };
        //        _transForm.BeginAnimation(TranslateTransform.XProperty, xOffsetAnimation);

        //        ScrollOwner.InvalidateScrollInfo();
        //    }
        //    return _screenSize;
        //}

    }
}
