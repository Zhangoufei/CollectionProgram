using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WpfCollectionDemo1.mvvm的使用.ViewModel
{
    public class TextMvvmModel : ViewModelBase
    {

        //使用绑定lable1
        private string lableText1;

        private string colorRed;

        public string LableText1
        {
            set
            {

                if (lableText1 != null)
                {
                    lableText1 = value;
                    RaisePropertyChanged("LableText1");
                }
            }
            get
            {
                return lableText1;
            }
        }
        public string ColorRed
        {
            set
            {

                if (colorRed != null)
                {
                    colorRed = value;
                    RaisePropertyChanged("ColorRed");
                }
            }
            get
            {
                return colorRed;
            }
        }
        private ObservableCollection<ListBoxTest1> listBoxBinding1 = new ObservableCollection<ListBoxTest1>();

        public ObservableCollection<ListBoxTest1> ListBoxBinding1
        {
            set
            {
                listBoxBinding1 = value;
            }
            get { return listBoxBinding1; }
        }


        public TextMvvmModel()
        {

            lableText1 = "测试使用lable绑定1";
            colorRed = "Red";
            listBoxBinding1.Add(new ListBoxTest1()
            {

                ListboxTextBlock1 = "测试1",
                ListboxTextBlock2 = "测试2"
            });

            listBoxBinding1.Add(new ListBoxTest1()
            {

                ListboxTextBlock1 = "测试3",
                ListboxTextBlock2 = "测试4"
            });

            listBoxBinding1.Add(new ListBoxTest1()
            {

                ListboxTextBlock1 = "测试4",
                ListboxTextBlock2 = "测试45"
            });
        }

    }
    public class ListBoxTest1 : ViewModelBase
    {

        private string listboxTextBlock1;
        private string listboxTextBlock2;

        public string ListboxTextBlock1
        {
            set
            {
                listboxTextBlock1 = value;
                RaisePropertyChanged("ListboxTextBlock1");
            }
            get
            {
                return listboxTextBlock1;
            }

        }
        public string ListboxTextBlock2
        {
            set
            {
                listboxTextBlock2 = value;
                RaisePropertyChanged("ListboxTextBlock2");
            }
            get
            {
                return listboxTextBlock2;
            }

        }

    }
}
