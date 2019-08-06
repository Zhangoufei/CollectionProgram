﻿using Com.Zhang.Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestConnection.app;
using Utility;

namespace TestConnection
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
        public async Task<LastTest> getClassList(string sercretKey, string userId, string classId)
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            keyValuePairs.Add("sercretKey", sercretKey);
            keyValuePairs.Add("userId", userId);
            keyValuePairs.Add("classId", classId);

            string strResult = await Task.Run<string>(() =>
             {
                 return HttpSourceServer.GetResponse("/app/takePhotoClass/getLast", keyValuePairs, Request_type.TYPE_POST);
             });

            LastTest lastTest = JsonHelper.DeserializeObject<LastTest>(strResult);

            return lastTest;
        }

    }
}
