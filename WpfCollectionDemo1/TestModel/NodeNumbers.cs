using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntelligentClass.model
{
    public class NodeNumbers : ViewModelBase
    {

        private string breakNode;

        public string BreakNode
        {

            set
            {

                breakNode = value;
                RaisePropertyChanged("BreakNode");
            }
            get
            {
                return breakNode;
            }
        }

        private ObservableCollection<NodeNumber> nodeNumber;

        public ObservableCollection<NodeNumber> NodeNumber
        {
            set
            {

                nodeNumber = value;
            }
            get
            {
                return nodeNumber;
            }
        }

    }

    public class NodeNumber : ViewModelBase
    {

        private string number;

        public string Number
        {

            set
            {

                number = value;
                RaisePropertyChanged("Number");
            }
            get
            {
                return number;
            }
        }
    }
}
