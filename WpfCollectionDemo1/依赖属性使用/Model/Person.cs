using System.Windows;
using System.Windows.Controls;

namespace 依赖属性使用.Model
{

    /// <summary>
    /// 使用依赖属性
    ///1 让依赖属性的所在类型继承自DependencyObject类。
    ///2 使用public static 声明一个DependencyProperty的变量，该变量就是真正的依赖属性。
    ///3 在类型的静态构造函数中通过Register方法完成依赖属性的元数据注册。
    ///4 提供一个依赖属性的包装属性，通过这个属性来完成对依赖属性的读写操作。
    /// </summary>
    public class Person : DependencyObject
    {

        //2  ,静态的依赖属性
        public static DependencyProperty dependencyProperty;


        //3 在静态构造方法中完成元数据注册
        static Person()
        {

            dependencyProperty = DependencyProperty.Register("Name", typeof(string), typeof(Person), new PropertyMetadata("helloProperty", OnValueChanged));
        }


        private static void OnValueChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
        {
            //发生改变时回调
        }

        //4,属性包装器
        public string Name
        {
            get { return (string)GetValue(dependencyProperty); }

            set { SetValue(dependencyProperty, value); }
        }
    }

    public class MyButton : Button
    {

        static MyButton()
        {
            FrameworkPropertyMetadata propertyMetadata = new FrameworkPropertyMetadata();
            propertyMetadata.Inherits = false;

            Button.FontSizeProperty.OverrideMetadata(typeof(MyButton), propertyMetadata);

        }
    }

    public class MyButton2 : Button
    {



        public string MyProperty
        {
            get { return (string)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty =
            DependencyProperty.Register("MyProperty", typeof(string), typeof(MyButton2), new PropertyMetadata("**"));

    }


    public class MyLable : Label
    {

        static MyLable()
        {

            //   MyPropertyProperty = DependencyProperty.Register("MyProperty", typeof(string), typeof(MyLable), new PropertyMetadata("*"));

            //  MyPropertyProperty = MyButton2.MyPropertyProperty.AddOwner(typeof(MyLable), new PropertyMetadata("&"));
        }



        public string MyProperty
        {
            get { return (string)GetValue(MyPropertyProperty); }
            set { SetValue(MyPropertyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPropertyProperty = MyButton2.MyPropertyProperty.AddOwner(typeof(MyLable), new PropertyMetadata("&"));



    }


}
