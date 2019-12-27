using Com.Zhang.Common;

namespace MVVM.ViewModel
{

    public class Users : ViewModelBase
    {
        public string userName;

        public string UserName
        {
            set
            {
                userName = value;
                RaisePropertyChanged("UserName");
            }
            get { return userName; }
        }

        public string userName2;

        public string UserName2
        {
            set
            {
                userName2 = value;
                RaisePropertyChanged("UserName2");
            }
            get { return userName2; }
        }

        public string userName3 { set; get; }

        public string UserName3
        {
            set
            {
                userName3 = value;
                RaisePropertyChanged("UserName3");
            }
            get { return userName3; }
        }
    }
}
