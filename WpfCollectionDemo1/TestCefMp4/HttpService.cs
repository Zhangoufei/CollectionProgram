using Com.Zhang.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace TestCefMp4
{
    public class HttpService
    {


        /// <summary>
        /// 获取用户名获取用户信息
        /// </summary>
        public static async Task<string> GetUserInfo(string userId)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("userJbxxId", userId);

            string strResult = await Task.Run<string>(() =>
            {
                return HttpLangCaoeServer.GetResponse("/auth/api/inspur/uc/getUserByUserId/1.0", keyValuePairs, Request_type.TYPE_GET);
            });

            string lastTest = JsonHelper.JsonDeserialize<string>(strResult);

            return lastTest;
        }


        /// <summary>
        /// access_token的接口
        /// </summary>
        public static async Task<string> GetAccesssToken(string sercretKey, string userId, string classId)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("client_id", 20+"");
            keyValuePairs.Add("client_secret", "e169a0d2b72d5d74f8f1957305ca0126");
            keyValuePairs.Add("grant_type", "client_credentials");

            string strResult = await Task.Run<string>(() =>
            {
                return HttpLangCaoeServer.GetResponse("/auth/oauth", keyValuePairs, Request_type.TYPE_GET);
            });

            string lastTest = JsonHelper.JsonDeserialize<string>(strResult);

            return lastTest;
        }

        /// <summary>
        /// 验证用户名和密码的接口
        /// </summary>
        public static async Task<string> CheckUserAndPassword(string sercretKey, string userId, string classId)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("userid", "001986030");
            keyValuePairs.Add("passwd", "tiye@123");

            string strResult = await Task.Run<string>(() =>
            {
                return HttpLangCaoeServer.GetResponse("/auth/api/inspur/uc/validatePassword/1.0", keyValuePairs, Request_type.TYPE_GET);
            });

            string lastTest = JsonHelper.JsonDeserialize<string>(strResult);

            return lastTest;
        }

    }
}
