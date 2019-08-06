using Com.Zhang.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace 多线程类型
{

    public class HttpRestFul
    {


        public static HttpRestFul HttpRestFulStatic;

        public static HttpRestFul GetInstance()
        {
            if (HttpRestFulStatic == null)
            {
                HttpRestFulStatic = new HttpRestFul();
            }
            return HttpRestFulStatic;
        }

        private HttpRestFul() { }

        /// <summary>
        /// 获取班级列表
        /// </summary>
        public async Task<string> getClassList()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("xxJbxxId", "8f19c9bf5d5a4d8f9075a789b32a13d0");

            string strResult = await HttpSourceServer.GetResponse("/pc/school/allClass", keyValuePairs);

            return strResult;
        }

    }
}
