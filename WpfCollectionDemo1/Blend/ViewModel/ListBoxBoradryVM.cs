using Com.Zhang.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Threading;

namespace Blend.ViewModel
{
    class ListBoxBoradryVM : ViewModelBase
    {

        private ObservableCollection<ImageList> imageLists = new ObservableCollection<ImageList>();

        public ObservableCollection<ImageList> ImageLists
        {
            set
            {

                imageLists = value;
            }
            get
            {
                return imageLists;
            }
        }

        public void InitObservable(FileInfo[] list)
        {

            imageLists.Clear();
            foreach (var item in list)
            {
                imageLists.Add(new ImageList() { ImagePic = item.FullName });
            }

        }


    }



    public class ImageList : ViewModelBase
    {
        private string imagePic;

        public string ImagePic
        {

            set
            {
                imagePic = value;
                RaisePropertyChanged("ImagePic");
            }
            get
            {
                return imagePic;
            }
        }

    }
}
