using Com.Zhang.Common;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVVM绑定.ViewModel
{
    class EventBindingVm : ViewModelBase
    {



        private int selectIndex;

        public int SelectIndex
        {
            set
            {
                selectIndex = value;
                RaisePropertyChanged("SelectIndex");
            }
            get
            {
                return selectIndex;
            }
        }


        private ICommand selectionChanged;

        public ICommand SelectionChanged
        {

            get
            {

                if (selectionChanged == null)
                {

                    selectionChanged = new RelayCommand((sender) =>
                    {
                        Users users = (Users)((ListBox)sender).SelectedValue;

                    });
                }
                return selectionChanged;
            }
        }


        public EventBindingVm()
        {
            selectIndex = 6;
        }

    }
}
