using Com.Zhang.Common;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace MVVM.ViewModel
{
    public class EventBindingVm : ViewModelBase
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
                        // Users users = (Users)((ListBox)sender).SelectedValue;
                        ListBox users = ((ListBox)sender);

                    });
                }
                return selectionChanged;
            }
        }


        private MyCommand<MouseEventArgs> mouseLeftButtonDown;

        public MyCommand<MouseEventArgs> MouseLeftButtonDown
        {

            get
            {

                if (mouseLeftButtonDown == null)
                {

                    mouseLeftButtonDown = new MyCommand<MouseEventArgs>(new System.Action<MouseEventArgs>(e =>
                    {



                    })
                    );
                }
                return mouseLeftButtonDown;
            }
        }

        public ObservableCollection<Users> appItems = new ObservableCollection<Users>();

        public ObservableCollection<Users> AppItems
        {
            set
            {
                appItems = value;
            }
            get
            {
                return appItems;
            }
        }

        public EventBindingVm()
        {
            for (int i = 0; i < 30; i++)
            {
                appItems.Add(new Users()
                {
                    UserName = "zhangsn" + i,
                    UserName2 = "历史" + i,
                    UserName3 = "玩忘" + i
                });
            }


            //  selectIndex = 6;
        }

    }
}
