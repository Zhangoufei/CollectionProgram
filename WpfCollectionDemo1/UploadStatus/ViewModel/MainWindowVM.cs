using Com.Zhang.Common;

namespace UploadStatus.ViewModel
{
    class MainWindowVM : ViewModelBase
    {

        /// <summary>
        /// 图片背景色
        /// </summary>
        private string background { set; get; }


        public string Background
        {
            set
            {
                background = value;

                RaisePropertyChanged("Background");
            }
            get
            {
                return background;
            }
        }

        /// <summary>
        /// 图片前景色
        /// </summary>
        private string forceColore;

        public string ForceColore
        {
            set
            {
                forceColore = value;

                RaisePropertyChanged("ForceColore");
            }
            get
            {
                return forceColore;
            }
        }

        /// <summary>
        /// 前景色 显示隐藏
        /// </summary>
        private bool foreceColoreVisibility;

        public bool ForeceColoreVisibility
        {
            set
            {
                foreceColoreVisibility = value;

                RaisePropertyChanged("ForeceColoreVisibility");
            }
            get
            {
                return foreceColoreVisibility;
            }
        }

        /// <summary>
        /// 缓存加载 gif
        /// </summary>
        private string forceLoadingGif;
        public string ForceLoadingGif
        {
            set
            {
                forceLoadingGif = value;

                RaisePropertyChanged("ForceLoadingGif");
            }
            get
            {
                return forceLoadingGif;
            }
        }


        /// <summary>
        /// 前景加载 gif显示隐藏
        /// </summary>
        private bool foreceLoadingVisibility;

        public bool ForeceLoadingVisibility
        {
            set
            {
                foreceLoadingVisibility = value;

                RaisePropertyChanged("ForeceLoadingVisibility");
            }
            get
            {
                return foreceLoadingVisibility;
            }
        }
        public PictureStyles pictureStyles = new WorldStyle();

        public MainWindowVM()
        {

            pictureStyles.Init();

            //初始化颜色
            Background = pictureStyles.background;
            ForceColore = pictureStyles.forceColore;

            ForeceColoreVisibility = pictureStyles.foreceColoreVisibility;

            ForceLoadingGif = pictureStyles.forceLoadingGif;

            ForeceLoadingVisibility = pictureStyles.foreceLoadingVisibility;

        }

    }
}
