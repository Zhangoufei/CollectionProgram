using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WpfCollectionDemo1.mvvm的使用.mode
{

    public class CollectionSection
    {

        private ObservableCollection<Section> sections = new ObservableCollection<Section>();

        public ObservableCollection<Section> Sections
        {
            set
            {
                sections = value;
            }
            get { return sections; }
        }

        private ObservableCollection<Numbers> numbers = new ObservableCollection<Numbers>();

        public ObservableCollection<Numbers> Numbers
        {
            set
            {
                numbers = value;
            }
            get { return numbers; }
        }


    }


    public class Section : ViewModelBase
    {

        private string sectionName;

        public string SectionName
        {

            set
            {
                sectionName = value;
                RaisePropertyChanged("SectionName");
            }
            get
            {
                return sectionName;
            }
        }

        private ObservableCollection<Numbers> numbers;

        public ObservableCollection<Numbers> Numbers
        {
            set
            {
                numbers = value;
            }
            get { return numbers; }
        }

    }

    public class Numbers : ViewModelBase
    {
        private string numberName;

        public string NumberName
        {

            set
            {
                numberName = value;
                RaisePropertyChanged("NumberName");
            }
            get
            {
                return numberName;
            }
        }

    }
}
