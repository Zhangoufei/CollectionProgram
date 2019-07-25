using Com.Zhang.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf动画.ViewModel
{
    class WPFtoGIFVM : ViewModelBase
    {

        private string imageGif;

        public string ImageGif
        {
            set
            {
                imageGif = value;
                RaisePropertyChanged("ImageGif");
            }
            get { return imageGif; }
        }


        public  List<string> ListImage = new List<string>();

        public WPFtoGIFVM() {

            imageGif = "";

            ListImage.Add("IMAGE_ClassInteraction_Time_clock00001");
            ListImage.Add("IMAGE_ClassInteraction_Time_clock00002");
            ListImage.Add("IMAGE_ClassInteraction_Time_clock00003");
            ListImage.Add("IMAGE_ClassInteraction_Time_clock00004");
            ListImage.Add("IMAGE_ClassInteraction_Time_clock00005");
            ListImage.Add("IMAGE_ClassInteraction_Time_clock00006");
            ListImage.Add("IMAGE_ClassInteraction_Time_clock00007");
            ListImage.Add("IMAGE_ClassInteraction_Time_clock00008");
            ListImage.Add("IMAGE_ClassInteraction_Time_clock00009");
            ListImage.Add("IMAGE_ClassInteraction_Time_clock00010");
            ListImage.Add("IMAGE_ClassInteraction_Time_clock00011");
            ListImage.Add("IMAGE_ClassInteraction_Time_clock00012");
            ListImage.Add("IMAGE_ClassInteraction_Time_clock00013");
            ListImage.Add("IMAGE_ClassInteraction_Time_clock00014");
            ListImage.Add("IMAGE_ClassInteraction_Time_clock00015");
        }

    }
}
