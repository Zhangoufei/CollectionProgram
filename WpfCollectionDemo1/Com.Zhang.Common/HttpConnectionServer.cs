using Com.Tiye.Log;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utility;

namespace Com.Zhang.Common
{

    public enum Request_type
    {
        TYPE_GET,
        TYPE_POST
    }


    abstract public class HttpServer
    {
        public static async Task<string> GetHttpResponse(string url, Dictionary<string, string> paraData, int Timeout, Request_type type = Request_type.TYPE_GET)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            string temp = string.Empty;
            string ret;
            try
            {
                string strContentType = "application/x-www-form-urlencoded";
                if (Request_type.TYPE_POST == type)
                {
                    if (paraData != null)
                    {
                        foreach (var item in paraData)
                        {
                            if (item.Key == "jsonKey")  //待优化
                            {
                                temp += item.Value + "&";
                                strContentType = "application/json;charset=UTF-8";
                            }
                            else
                            {
                                temp += item.Key + "=" + item.Value + "&";
                            }
                        }
                        temp = temp.Substring(0, temp.Length - 1);
                    }
                    Encoding encoding = Encoding.GetEncoding("utf-8");
                    byte[] byteArray = encoding.GetBytes(temp);
                    HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(url));
                    webReq.Method = "POST";
                    webReq.ContentType = strContentType;
                    webReq.ContentLength = byteArray.Length;

                    Stream newStream = webReq.GetRequestStream();
                    newStream.Write(byteArray, 0, byteArray.Length);

                    HttpWebResponse response2 = null;

                    try
                    {
                        response2 = (HttpWebResponse)webReq.GetResponse();
                    }
                    catch (WebException ex)
                    {
                        response2 = (HttpWebResponse)ex.Response;
                    }


                    StreamReader sr = new StreamReader(response2.GetResponseStream(), encoding);

                    //  ret = sr.ReadToEnd();

                    ret = await sr.ReadToEndAsync();

                    sr.Close();
                    response2.Close();
                    newStream.Close();
                    return ret;
                }
                else
                {
                    if (paraData != null)
                    {
                        foreach (var item in paraData)
                        {
                            temp += item.Key + "=" + item.Value + "&";
                        }
                        temp = temp.Substring(0, temp.Length - 1);
                    }


                    url = url + "?" + temp;
                    request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "GET";

                    request.ContentType = "text/html;charset=UTF-8";
                    request.UserAgent = null;
                    request.Timeout = Timeout;

                    try
                    {
                        response = (HttpWebResponse)request.GetResponse();
                    }
                    catch (WebException ex)
                    {
                        response = (HttpWebResponse)ex.Response;
                    }
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                    // string retString = myStreamReader.ReadToEnd();

                    string retString = await myStreamReader.ReadToEndAsync();

                    myStreamReader.Close();
                    myResponseStream.Close();

                    return retString;
                }
            }
            catch (Exception ee)
            {
                Logger.Error("服务器连接异常" + ee.StackTrace);
                return string.Empty;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
        }

        public static bool pingPort(string strServer)
        {
            string tempPort = "";
            string pingip = "";
            if (strServer.Contains("http"))
            {
                strServer = strServer.Substring(strServer.IndexOfAny(new char[] { '/', '/' }) + 2);
            }

            if (strServer.Contains(":") && strServer.Contains("/"))
            {
                int startIndex = strServer.IndexOf(":");
                int endIndex = strServer.IndexOf("/");
                tempPort = strServer.Substring(startIndex + 1, endIndex - startIndex - 1);
                pingip = strServer.Substring(0, startIndex);
            }
            else if (strServer.Contains(":") && !strServer.Contains("/"))
            {
                int startIndex = strServer.IndexOf(":");
                tempPort = strServer.Substring(startIndex + 1);
                pingip = strServer.Substring(0, startIndex);
            }
            else
            {
                tempPort = "80";
                pingip = strServer;
            }
            return ICUtility.PingPort(pingip, int.Parse(tempPort));
        }

        //服务器连接状态
        //public static bool IsServerConnected { get; private set; } = false;
        //服务器状态变更回调
        public delegate void ServerConnectChanged(bool bIsconnected);

        static ServerConnectChanged serverStatusCallback = null;

        //服务器状态的心跳检测
        static Thread heartBeatThread = null;

        public static void startupHeartbeatTimer(ServerConnectChanged callback)
        {
            serverStatusCallback = callback;
            if (heartBeatThread == null)
            {
                heartBeatThread = new Thread(onHeartBeat);
            }

            if (heartBeatThread.ThreadState == ThreadState.Unstarted)
                heartBeatThread.Start();
        }

        //缓存判断心跳 
        public static event Action<bool> CatcheHeartEvent;

        //断开连接次数
        private static int NotConnectionTick = 0;

        //检测损坏文件次数
        private static int CheckBadFileTick = 0;

        /// <summary>
        /// 心跳频率 单位s  心跳间隔为5秒
        /// </summary>
        private static int HeartTick = 5;

        /// <summary>
        /// 断开连接 
        /// </summary>
        public static bool DisConnection = false;

        static void onHeartBeat()
        {
            while (true)
            {
                try
                {
                    HttpSourceServer.IsServerConnected = HttpSourceServer.pingPort();

                    if (serverStatusCallback != null)
                        serverStatusCallback(HttpSourceServer.IsServerConnected);

                    //删除 郑州下载的文件
                }
                catch (Exception)
                {

                }


                Thread.Sleep(1000 * HeartTick);//心跳间隔为5秒
            }

        }

        //释放心跳线程
        public static void Release()
        {
            if (heartBeatThread != null)
                heartBeatThread.Abort();
        }

    }

    /// <summary>
    /// 资源中心服务
    /// </summary>
    public class HttpSourceServer
    {
        static string strServer = ConfigurationManager.AppSettings["httpSource"];

        public static async Task<string> GetResponse(string strSubAddress, Dictionary<string, string> pairs, Request_type type = Request_type.TYPE_GET)
        {
            if (!ICUtility.Ping(strServer)) return "";

            string url = "http://" + strServer + "/" + strSubAddress;

            return await HttpServer.GetHttpResponse(url, pairs, 4000, type);
        }


        public static bool pingPort()
        {
            bool result = HttpServer.pingPort(strServer);
            if (result)
            {
                return true;
            }
            else
            {
                Logger.Error("服务器不通:" + strServer);
                return false;
            }
        }
        //服务器连接状态
        public static bool IsServerConnected { get; set; } = false;
    }


}
