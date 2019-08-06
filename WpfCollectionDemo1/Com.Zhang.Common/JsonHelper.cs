using Com.Tiye.Log;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Utility
{
    /// <summary>
    /// JSON Serialization and Deserialization Assistant Class
    /// </summary>
    public class JsonHelper
    {
        /// <summary>
        /// JSON Serialization
        /// </summary>
        public static string JsonSerializer<T>(T t)
        {
            string jsonString = "";
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                ser.WriteObject(ms, t);
                jsonString = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
            }
            catch (Exception e)
            {
              //  Logger.Error("JsonSerializer类型转换失败" + e.StackTrace);
            }

            return jsonString;
        }

        /// <summary>
        /// JSON Deserialization
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            T obj;
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                obj = (T)ser.ReadObject(ms);
            }
            catch (Exception e)
            {
               // Logger.Error("JsonDeserialize类型转换失败" + jsonString + e.StackTrace);
                return default(T);
            }
            return obj;
        }

        /// <summary>
        /// JSON反序列化：将JSON字符串解析成预定的数据类型
        /// </summary>
        /// <typeparam name="T">泛型：需要反序列化返回的数据类型</typeparam>
        /// <param name="jsonString">输入需要解析的字符内容</param>
        /// <returns>返回指定的数据类型</returns>
        public static T DeserializeObject<T>(string jsonString)
        {
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

    }

}
