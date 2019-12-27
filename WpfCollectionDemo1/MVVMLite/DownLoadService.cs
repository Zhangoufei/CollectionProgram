using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace MVVMLite
{
    public class DownLoadService
    {

        private DownLoadService() { }

        public static DownLoadService downLoadService;

        public static DownLoadService GetInstance()
        {
            if (downLoadService == null)
            {
                downLoadService = new DownLoadService();
            }
            return downLoadService;
        }
        //是否开启1.2s后的连续动画事件
        public bool AnimationBool = false;
        /// <summary>
        /// 下载完成事件
        /// </summary>
        public event Action<DownloadModel> DownloadFileCompleted;

        public event Action<DownloadModel> DownloadSecondsOnePointTwoLaterEvent;

        public event Action<DownloadModel> DownloadSecondsOnePointTwoLaterTwoEvent;

        //下载器   先不使用队列 ，之后再考虑
        public Task DownLoad(DownloadModel downloadModel)
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    webClient.DownloadFileCompleted += (s, e) =>
                    {
                        DownloadFileCompleted?.Invoke(downloadModel);
                        //1.2s后动画
                        if (AnimationBool)
                        {
                            DispatcherTimer dispatcherTimer = new DispatcherTimer();
                            dispatcherTimer.Interval = TimeSpan.FromSeconds(1.2);
                            dispatcherTimer.Tick += (ss, ee) =>
                            {
                                dispatcherTimer.Stop();
                                DownloadSecondsOnePointTwoLaterEvent?.Invoke(downloadModel);

                                DispatcherTimer dispatcherTimer2 = new DispatcherTimer();
                                dispatcherTimer2.Interval = TimeSpan.FromSeconds(1.2);
                                dispatcherTimer2.Tick += (sst, eet) =>
                                {
                                    dispatcherTimer2.Stop();
                                    DownloadSecondsOnePointTwoLaterTwoEvent?.Invoke(downloadModel);
                                };
                                dispatcherTimer2.Start();
                            };
                            dispatcherTimer.Start();

                        }

                    };
                    return webClient.DownloadFileTaskAsync(downloadModel.Url, downloadModel.Cachepath);
                }
                catch (System.Exception ee)
                {

                }
            }
            return null;
        }
    }


    public class DownloadModel
    {

        public string Id { set; get; }

        /// <summary>
        /// 下载的地址
        /// </summary>
        public string Url { set; get; }

        /// <summary>
        /// 下载的文件名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 存储的路径
        /// </summary>
        public string Cachepath { set; get; }
    }
}
