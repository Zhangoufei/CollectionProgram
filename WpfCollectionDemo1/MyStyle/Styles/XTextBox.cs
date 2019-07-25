using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MyStyle.Styles
{
    public class XTextBox : TextBox
    {
        #region 依赖属性
        public static readonly DependencyProperty XWmkTextProperty;//水印文字
        public static readonly DependencyProperty XWmkForegroundProperty;//水印着色
        public static readonly DependencyProperty XIsErrorProperty;//是否字段有误
        public static readonly DependencyProperty XAllowNullProperty;//是否允许为空
        public static readonly DependencyProperty XRegExpProperty;//正则表达式
        #endregion

        #region 内部方法
        /// <summary>
        /// 注册事件
        /// </summary>
        public XTextBox()
        {
            this.LostFocus += new RoutedEventHandler(XTextBox_LostFocus);
            this.GotFocus += new RoutedEventHandler(XTextBox_GotFocus);
            this.PreviewMouseDown += new MouseButtonEventHandler(XTextBox_PreviewMouseDown);
        }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static XTextBox()
        {
            //注册依赖属性
            XTextBox.XWmkTextProperty = DependencyProperty.Register("XWmkText", typeof(String), typeof(XTextBox), new PropertyMetadata(null));
            XTextBox.XAllowNullProperty = DependencyProperty.Register("XAllowNull", typeof(bool), typeof(XTextBox), new PropertyMetadata(true));
            XTextBox.XIsErrorProperty = DependencyProperty.Register("XIsError", typeof(bool), typeof(XTextBox), new PropertyMetadata(false));
            XTextBox.XRegExpProperty = DependencyProperty.Register("XRegExp", typeof(string), typeof(XTextBox), new PropertyMetadata(""));
            XTextBox.XWmkForegroundProperty = DependencyProperty.Register("XWmkForeground", typeof(Brush),
                typeof(XTextBox), new PropertyMetadata(Brushes.Silver));

            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(XTextBox), new FrameworkPropertyMetadata(typeof(XTextBox)));
        }

        /// <summary>
        /// 失去焦点时检查输入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void XTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            this.XIsError = false;
            if (XAllowNull == false && this.Text.Trim() == "")
            {
                this.XIsError = true;
            }
            if (Regex.IsMatch(this.Text.Trim(), XRegExp) == false)
            {
                this.XIsError = true;
            }
        }

        /// <summary>
        /// 获得焦点时选中文字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void XTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            this.SelectAll();
        }

        /// <summary>
        /// 鼠标点击时选中文字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void XTextBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.IsFocused == false)
            {
                TextBox textBox = e.Source as TextBox;
                textBox.Focus();
                e.Handled = true;
            }
        }
        #endregion

        #region 公布属性
        /// <summary>
        /// 公布属性XWmkText（水印文字）
        /// </summary>
        public String XWmkText
        {
            get
            {
                return base.GetValue(XTextBox.XWmkTextProperty) as String;
            }
            set
            {
                base.SetValue(XTextBox.XWmkTextProperty, value);
            }
        }

        /// <summary>
        /// 公布属性XWmkForeground（水印着色）
        /// </summary>
        public Brush XWmkForeground
        {
            get
            {
                return base.GetValue(XTextBox.XWmkForegroundProperty) as Brush;
            }
            set
            {
                base.SetValue(XTextBox.XWmkForegroundProperty, value);
            }
        }

        /// <summary>
        /// 公布属性XIsError（是否字段有误）
        /// </summary>
        public bool XIsError
        {
            get
            {
                return (bool)base.GetValue(XTextBox.XIsErrorProperty);
            }
            set
            {
                base.SetValue(XTextBox.XIsErrorProperty, value);
            }
        }

        /// <summary>
        /// 公布属性XAllowNull（是否允许为空）
        /// </summary>
        public bool XAllowNull
        {
            get
            {
                return (bool)base.GetValue(XTextBox.XAllowNullProperty);
            }
            set
            {
                base.SetValue(XTextBox.XAllowNullProperty, value);
            }
        }

        /// <summary>
        /// 公布属性XRegExp（正则表达式）
        /// </summary>
        public string XRegExp
        {
            get
            {
                return base.GetValue(XTextBox.XRegExpProperty) as string;
            }
            set
            {
                base.SetValue(XTextBox.XRegExpProperty, value);
            }
        }
        #endregion

    }
}
