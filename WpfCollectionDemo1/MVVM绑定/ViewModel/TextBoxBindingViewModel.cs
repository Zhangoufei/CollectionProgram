using Com.Zhang.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM绑定.ViewModel
{
    class TextBoxBindingViewModel : ViewModelBase
    {

        private static string demoTextBox;

        public  string DemoTextBox
        {

            get => demoTextBox;
            set
            {

                demoTextBox = value;
                RaisePropertyChanged("DemoTextBox");
            }
        }

        public RelayCommand BtnAddContent { set; get; }

        public RelayCommand BtnAddEnter { set; get; }

        public TextBoxBindingViewModel()
        {

            DemoTextBox = "123";
            BtnAddContent = new RelayCommand(AddContent);

            BtnAddEnter = new RelayCommand(AddEnterContent);
        }

        private void AddContent()
        {
            Console.WriteLine("日志"+ DemoTextBox);
        }


        private void AddEnterContent() {

            Console.WriteLine("AddEnterContent" + DemoTextBox);
        }
    }
}
