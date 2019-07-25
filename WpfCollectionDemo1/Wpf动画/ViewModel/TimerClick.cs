using Com.Zhang.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf动画.ViewModel
{
    class TimerClick : ViewModelBase
    {

        private string textBlockBing;

        public string TextBlockBing
        {
            set
            {
                textBlockBing = value;
                RaisePropertyChanged("TextBlockBing");
            }
            get { return textBlockBing; }
        }

    }
}
