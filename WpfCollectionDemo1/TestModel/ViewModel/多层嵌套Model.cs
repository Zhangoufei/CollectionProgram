using GalaSoft.MvvmLight;
using IntelligentClass.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestModel.ViewModel
{
    public class 多层嵌套Model : ViewModelBase
    {

        private ObservableCollection<NodeNumbers> nodeNumbers = new ObservableCollection<NodeNumbers>();

        public ObservableCollection<NodeNumbers> NodeNumbers
        {

            set
            {
                nodeNumbers = value;

            }
            get { return nodeNumbers; }
        }

        public 多层嵌套Model()
        {




            NodeNumbers NodeNumber = new NodeNumbers()
            {
                BreakNode = "上午",
                NodeNumber = new ObservableCollection<NodeNumber>()
                {
                      new NodeNumber(){ Number=1+"" },
                      new NodeNumber(){ Number=2+"" },
                      new NodeNumber(){ Number=3+"" },
                      new NodeNumber(){ Number=4+"" },
                }
            };
            NodeNumbers.Add(NodeNumber);
            NodeNumber = new NodeNumbers()
            {
                BreakNode = "下午",
                NodeNumber = new ObservableCollection<NodeNumber>()
                {
                      new NodeNumber(){ Number=1+"" },
                      new NodeNumber(){ Number=2+"" },
                      new NodeNumber(){ Number=3+"" },
                      new NodeNumber(){ Number=4+"" },
                }

            };
            NodeNumbers.Add(NodeNumber);
            NodeNumber = new NodeNumbers()
            {
                BreakNode = "晚上",
                NodeNumber = new ObservableCollection<NodeNumber>()
                {
                      new NodeNumber(){ Number=1+"" },
                      new NodeNumber(){ Number=2+"" },
                      new NodeNumber(){ Number=3+"" },
                      new NodeNumber(){ Number=4+"" },
                }
            };
            NodeNumbers.Add(NodeNumber);
        }
    }
}
