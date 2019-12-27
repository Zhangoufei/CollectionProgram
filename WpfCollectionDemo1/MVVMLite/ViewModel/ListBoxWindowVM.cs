using Com.Zhang.Common;
using SevenZip;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace MVVMLite.ViewModel
{
    public class ListBoxWindowVM : Com.Zhang.Common.ViewModelBase
    {

        public ObservableCollection<APPUserModel> appItems = new ObservableCollection<APPUserModel>();

        public ObservableCollection<APPUserModel> AppItems
        {
            set
            {
                appItems = value;
            }
            get
            {
                return appItems;
            }
        }


        private ICommand selectionChanged;

        public ICommand SelectionChanged
        {

            get
            {

                if (selectionChanged == null)
                {

                    selectionChanged = new RelayCommand((sender) =>
                    {
                        ListBox users = ((ListBox)sender);

                        //加入下载列表
                        DownloadModel downloadModel = new DownloadModel()
                        {
                            Cachepath = "D://TIYE//Dowload//" + "AU" + ".exe",
                            Name = "AU",
                            Url = "https://m.xueleyun.com/download/wisdom_class/cpp",
                        };

                        DownLoadService downLoadService = DownLoadService.GetInstance();
                        downLoadService.AnimationBool = true;

                        //下载完成
                        downLoadService.DownloadFileCompleted += (ss) =>
                        {
                        };
                        //第一次1.2s后
                        downLoadService.DownloadSecondsOnePointTwoLaterEvent += (ss) =>
                        {
                        };
                        downLoadService.DownloadSecondsOnePointTwoLaterTwoEvent += (ss) =>
                        {
                            //开始安装
                            //UtilityCommon.StartExE(ss.Cachepath);

                            //开始解压文件
                          

                        };
                        downLoadService.DownLoad(downloadModel);

                    });
                }
                return selectionChanged;
            }
        }




        //private MyCommand<MouseEventArgs> mouseLeftButtonDown;
        //public MyCommand<MouseEventArgs> MouseLeftButtonDowndd
        //{
        //    get
        //    {
        //        if (mouseLeftButtonDown == null)
        //        {
        //            mouseLeftButtonDown = new MyCommand<MouseEventArgs>(new Action<MouseEventArgs>(e =>
        //            {



        //            }));
        //        }
        //        return mouseLeftButtonDown;
        //    }
        //}



        public ListBoxWindowVM()
        {
            for (int i = 0; i < 1000; i++)
            {
                AppItems.Add(new APPUserModel()
                {
                    ImageBackGround = new BitmapImage(new System.Uri("http://192.168.17.82/0201AA592241DA015D5F2B899FDAFE6E/lALPDgQ9q124XrzNAsTNAuI_738_708.png_620x10000q90g.jpg")),
                    TitleName = "22222"
                });
            }

        }

    }

    /// <summary>
    /// 界面model
    /// </summary>
    public class APPUserModel : Com.Zhang.Common.ViewModelBase
    {
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

        private BitmapImage imageBackGround;

        public BitmapImage ImageBackGround
        {
            set
            {
                imageBackGround = value;
                RaisePropertyChanged("ImageBackGround");
            }
            get { return imageBackGround; }
        }


    }
}
