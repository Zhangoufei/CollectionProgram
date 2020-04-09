using Com.Zhang.Common;

namespace MVVM绑定.ViewModel
{
    public class ProgressWindowVm : ViewModelBase
    {

        private int progressValue;
        public int ProgressValue
        {
            set
            {
                progressValue = value;

                RaisePropertyChanged("ProgressValue");
            }
            get
            {
                return progressValue;
            }
        }


        public ProgressWindowVm()
        {


        }

        public void SetAddValue()
        {
            if (ProgressValue <= 100)
            {
                ProgressValue++;
            }
        }
    }
}
