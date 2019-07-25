namespace 倒计时
{
    public class ProcessCount
    {

        /// <summary>
        /// 实现倒计时功能的类
        /// </summary>
        private int _TotalSecond;
        public int TotalSecond
        {
            get { return _TotalSecond; }
            set { _TotalSecond = value; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public ProcessCount(int totalSecond)
        {
            this._TotalSecond = totalSecond;
        }

        /// <summary>
        /// 减秒
        /// </summary>
        /// <returns></returns>
        public bool ProcessCountDown()
        {
            if (_TotalSecond == 0)
                return false;
            else
            {
                _TotalSecond--;
                return true;
            }
        }

        /// <summary>
        /// 获取小时显示值
        /// </summary>
        /// <returns></returns>
        public string GetHour()
        {
            return string.Format("{0:D2}", (_TotalSecond / 3600));
        }

        /// <summary>
        /// 获取分钟显示值
        /// </summary>
        /// <returns></returns>
        public string GetMinute()
        {
            return string.Format("{0:D2}", (_TotalSecond % 3600) / 60);
        }

        /// <summary>
        /// 获取秒显示值
        /// </summary>
        /// <returns></returns>
        public string GetSecond()
        {
            return string.Format("{0:D2}", _TotalSecond % 60);
        }


    }
}
