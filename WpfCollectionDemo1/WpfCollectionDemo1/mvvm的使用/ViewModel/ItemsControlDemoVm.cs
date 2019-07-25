using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using WpfCollectionDemo1.mvvm的使用.mode;

namespace WpfCollectionDemo1.mvvm的使用.ViewModel
{
    public class ItemsControlDemoVm : ViewModelBase
    {
        private CollectionSection collectionSection = new CollectionSection();


        public CollectionSection CollectionSection
        {

            set
            {
                collectionSection = value;
                RaisePropertyChanged("CollectionSection");
            }
            get { return collectionSection; }
        }

        private ObservableCollection<Section> sections = new ObservableCollection<Section>();

        public ObservableCollection<Section> Sections
        {
            set
            {
                sections = value;
            }
            get { return sections; }
        }

        private ObservableCollection<Numbers> numbers=new ObservableCollection<Numbers>();

        public ObservableCollection<Numbers> Numbers
        {
            set
            {
                numbers = value;
            }
            get { return numbers; }
        }
        public ItemsControlDemoVm()
        {
           
            numbers.Add(new Numbers()
            {
                NumberName = 1 + ""
            });
            numbers.Add(new Numbers()
            {
                NumberName = 2 + ""
            });
            numbers.Add(new Numbers()
            {
                NumberName = 3 + ""
            });

            Sections.Add(new Section()
            {
                SectionName = "上午",
                Numbers = numbers
            });

        }

    }
}
