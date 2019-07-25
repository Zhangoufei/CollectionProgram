namespace UploadStatus
{
    public abstract class PictureStyles
    {
        /// <summary>
        /// 图片背景色
        /// </summary>
        public string background { set; get; }

        /// <summary>
        /// 图片透明度
        /// </summary>
        public double Opacity { set; get; }

        /// <summary>
        /// 图片前景色  无下载
        /// </summary>
        public string forceColore { set; get; }

        /// <summary>
        /// 图片前景色  已下载
        /// </summary>
        public string forceColoreComplate { set; get; }

        /// <summary>
        /// 前景色 显示隐藏
        /// </summary>
        public bool foreceColoreVisibility { set; get; }

        /// <summary>
        /// 缓存加载 gif
        /// </summary>
        public string forceLoadingGif { set; get; }

        /// <summary>
        /// 缓存加载 gif 完成
        /// </summary>
        public string forceLoadingGifComplate { set; get; }

        /// <summary>
        /// 前景加载 gif显示隐藏
        /// </summary>
        public bool foreceLoadingVisibility { set; get; }

        public abstract void Init();

    }
    /// <summary>
    /// world样式
    /// </summary>
    public class WorldStyle : PictureStyles
    {
        public override void Init()
        {
            background = "IMAGE_home_W";

            forceColore = "IMAGE_home_2";

            foreceColoreVisibility = true;   //显示

            forceLoadingGif = "IMAGE_home_rolling";

            foreceLoadingVisibility = false;

            forceLoadingGifComplate = "IMAGE_home_f";

            forceColoreComplate = "IMAGE_home_3";
        }
    }

    /// <summary>
    /// PPT样式
    /// </summary>
    public class PPTStyle : PictureStyles
    {
        public override void Init()
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// Music样式
    /// </summary>
    public class MusicStyle : PictureStyles
    {
        public override void Init()
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// Video样式
    /// </summary>
    public class VideoStyle : PictureStyles
    {
        public override void Init()
        {
            throw new System.NotImplementedException();
        }
    }

    /// <summary>
    /// Flash样式
    /// </summary>
    public class FlashStyle : PictureStyles
    {
        public override void Init()
        {
            throw new System.NotImplementedException();
        }
    }
    /// <summary>
    /// OthersStyle样式
    /// </summary>
    public class OthersStyle : PictureStyles
    {
        public override void Init()
        {
            throw new System.NotImplementedException();
        }
    }
}
