using Com.Zhang.Common;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace WpfCollectionDemo1.mvvm的使用.ViewModel
{
    public class DropWindowVm : ViewModelBase
    {
        private ObservableCollection<FileNames> pictureInfors = new ObservableCollection<FileNames>();

        public ObservableCollection<FileNames> PictureInfors
        {
            set
            {
                pictureInfors = value;
            }
            get
            {
                return pictureInfors;
            }
        }

        private string picImageDrop;

        public string PicImageDrop
        {
            set
            {
                picImageDrop = value;
                RaisePropertyChanged("PicImageDrop");
            }
            get { return picImageDrop; }
        }


        public DropWindowVm()
        {
            FileInfo[] fileInfos = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory+"/Pic").GetFiles();


            foreach (var item in fileInfos)
            {
                pictureInfors.Add(new FileNames()
                {
                    TitleName = item.Name,
                    PicImage = item.FullName
                });
            }

            picImageDrop = pictureInfors.FirstOrDefault().PicImage;
        }

    }


    public class FileNames : ViewModelBase
    {

        private string picImage;

        public string PicImage
        {
            set
            {
                picImage = value;
                RaisePropertyChanged("PicImage");
            }
            get { return picImage; }
        }


        private string titleName;

        public string TitleName
        {
            set
            {
                titleName = value;
                RaisePropertyChanged("TitleName");
            }
            get { return titleName; }
        }

    }
}
