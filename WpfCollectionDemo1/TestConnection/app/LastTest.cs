using System.Collections.Generic;

namespace TestConnection.app
{
    public class LastTest
    {

        public string code { set; get; }

        public string message { set; get; }

        public Data data { set; get; }

    }


    public class Data
    {
        public string uid { set; get; }

        public string imgIds { set; get; }

        public string userId { set; get; }

        public List<string> imgUrlList { set; get; }

        public string createTime { set; get; }
    }
  
}
