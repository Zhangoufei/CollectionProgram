using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using MS.Internal.PresentationFramework;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using WpfCollectionDemo1.Common;

namespace IntelligentClass.ViewModel.model
{
    public class SubjectName : ViewModelBase
    {

        private string subjectClass;

        private string subjectClassName;

        private string subjectClassImage;

        private string subjectImage;

        private string subjectClassNameColor;

        /// <summary>
        /// 拼音
        /// </summary>
        public string subjectChinaLua { set; get; }

        /// <summary>
        /// 课程简称  例如 语 ,数
        /// </summary>
        public string SubjectClass
        {
            set
            {
                subjectClass = value;

                RaisePropertyChanged("SubjectClass");
            }
            get
            {
                return subjectClass;
            }
        }
        /// <summary>
        /// 课程名称 例如语文，数学
        /// </summary>
        public string SubjectClassName
        {
            set
            {
                subjectClassName = value;

                RaisePropertyChanged("SubjectClassName");
            }
            get
            {
                return subjectClassName;
            }
        }

        /// <summary>
        /// 课程简称 的图片
        /// </summary>
        public string SubjectClassImage
        {
            set
            {
                subjectClassImage = value;

                RaisePropertyChanged("SubjectClassImage");
            }
            get
            {
                return subjectClassImage;
            }
        }

        /// <summary>
        /// 课程的背景色
        /// </summary>
        public string SubjectImage
        {
            set
            {
                subjectImage = value;

                RaisePropertyChanged("SubjectImage");
            }
            get
            {
                return subjectImage;
            }
        }
        /// <summary>
        /// 课程名称的背景色 
        /// </summary>
        public string SubjectClassNameColor
        {
            set
            {
                subjectClassNameColor = value;

                RaisePropertyChanged("SubjectClassNameColor");
            }
            get
            {
                return subjectClassNameColor;
            }
        }

    }
    public class TestValue : ViewModelBase
    {
        private string unit;

        private string lessonValue;

        private string textValue;

        public string LessonVlaueId { get; set; }

        private string lessonValueColor;

        private string textValueColor;

        public string Unit
        {

            set
            {

                unit = value;
                RaisePropertyChanged("Unit");
            }
            get
            {
                return unit;
            }
        }
        public string LessonValue
        {
            set
            {

                lessonValue = value;
                RaisePropertyChanged("LessonValue");
            }
            get
            {

                return lessonValue;
            }
        }
        public string TextValue
        {
            set
            {

                textValue = value;
                RaisePropertyChanged("TextValue");
            }
            get { return textValue; }
        }
        public string LessonValueColor
        {
            set
            {

                lessonValueColor = value;
                RaisePropertyChanged("LessonValueColor");
            }
            get { return lessonValueColor; }
        }
        public string TextValueColor
        {
            set
            {

                textValueColor = value;
                RaisePropertyChanged("TextValueColor");
            }
            get { return textValueColor; }
        }
    }

    public class FileName : ViewModelBase
    {
        /// <summary>
        /// 图片名称
        /// </summary>
        private string fileImageBinding;

        private string fileClassName;

        /// <summary>
        /// 文件后缀
        /// </summary>
        public string FilePostfix { set; get; }

        public string url { set; get; }

        public string FileImageBinding
        {
            set
            {

                fileImageBinding = value;
                RaisePropertyChanged("FileImageBinding");
            }
            get { return fileImageBinding; }
        }

        public string FileClassName
        {
            set
            {

                fileClassName = value;
                RaisePropertyChanged("FileClassName");
            }
            get { return fileClassName; }
        }
    }

    public class ConvertStaticResource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string path = (string)value;
            if (!string.IsNullOrEmpty(path))
            {
                //return new BitmapImage(new Uri(path, UriKind.Absolute));
                //return FindResource(path) as BitmapImage;
                return ConvertCommon.FindResource(path) as BitmapImage;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConvertStaticBrushImageResource : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string path = (string)value;
            if (!string.IsNullOrEmpty(path))
            {
                //return new BitmapImage(new Uri(path, UriKind.Absolute));
                //return FindResource(path) as BitmapImage;
                return ConvertCommon.FindResource(path) as ImageBrush;
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
