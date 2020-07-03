using NUnit.Framework;
using System;
using System.Text;
using System.Web;

namespace NUnitTestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            string temp = "http://res.yxjtj.cn:10001/v1/file?d=resources%2F362880034c360b1b014c362d5b7d0067&n=%E3%80%8AChina%20attracts%20millions%20of%20tourists%20from%20all%20over%20the%20world.%E3%80%8B%E6%95%99%E5%AD%A6%E8%AF%BE%E4%BB%B6%EF%BC%88Section%20A%EF%BC%89.ppt";

            string url = GetLocalFileName(temp);


        }


        private static string GetLocalFileName(string strUrl)
        {
            int nIndex = -1;
            strUrl = HttpUtility.UrlDecode(strUrl, Encoding.GetEncoding("UTF-8"));

            if (strUrl.Contains("n="))
            {
                //nIndex = strUrl.LastIndexOfAny(new Char[] { 'n', '=' });

                //nIndex = strUrl.LastIndexOfAny(new Char[] { '&', 'n', '=' });
                nIndex = strUrl.LastIndexOf("n=") + 1;
            }
            else
            {
                nIndex = strUrl.LastIndexOf('/');
            }
            if (nIndex != -1)
                strUrl = strUrl.Substring(nIndex + 1);
            return strUrl;
        }

    }
}