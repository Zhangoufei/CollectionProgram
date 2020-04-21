using Com.Zhang.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utility;

namespace TestCEfPrimatry
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
        public static async Task<SecretClientModel> GetAccesssToken(string client_id, string client_secret, string grant_type)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("client_id", client_id);
            keyValuePairs.Add("client_secret", client_secret);
            keyValuePairs.Add("grant_type", grant_type);

            string strResult = await Task.Run<string>(() =>
            {
                return HttpLangCaoeServer.GetResponse("/auth/oauth/token", keyValuePairs, Request_type.TYPE_GET);
            });
            SecretClientModel lastTest = JsonHelper.JsonDeserialize<SecretClientModel>(strResult);

            return lastTest;
        }

        /// <summary>
        /// 验证用户名和密码的接口
        /// </summary>
        public static async Task<string> CheckUserAndPassword(string userId, string passwd, string client_id, string client_secret)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("userId", userId);
            keyValuePairs.Add("passwd", passwd);
            keyValuePairs.Add("client_id", client_id);
            keyValuePairs.Add("client_secret", client_secret);

            string strResult = await Task.Run<string>(() =>
            {
                return HttpLangCaoeServer.GetResponse("/auth/api/inspur/uc/validatePassword/1.0", keyValuePairs, Request_type.TYPE_GET);
            });

            return strResult;
        }

    }

    public class SecretClientModel
    {

        public string access_token { set; get; }

        public string token_type { set; get; }

        public string expires_in { set; get; }

    }
}
