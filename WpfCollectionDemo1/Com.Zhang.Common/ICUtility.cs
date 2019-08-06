using Com.Tiye.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Xml.Serialization;

namespace Utility
{
    public class ICUtility
    {


        public static Uri GetUri(string strPath)
        {
            try
            {
                return new Uri(strPath);
            }
            catch
            {
                return null;
            }
        }
        public static bool Ping(string ip)
        {
            if (ip == null || ip == string.Empty)
                return false;

            Ping pingSender = new Ping();
            if (ip.Contains(":"))
            {
                ip = ip.Substring(0, ip.IndexOf(':'));
            }
            try
            {
                PingReply reply = pingSender.Send(ip, 120);//第一个参数为ip地址，第二个参数为ping的时间

                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
                else
                {
                    return false;
                    //ping不通
                }
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static bool PingPort(string ipAddress, int portNum)
        {
            if (!Ping(ipAddress))
            {
                return false;
            }


            System.Net.IPAddress myIpAddress = IPAddress.Parse(ipAddress);
            IPEndPoint point = new IPEndPoint(myIpAddress, portNum);
            try
            {
                using (Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp) { SendTimeout = 120 })
                {
                    //  sock.SendTimeout = 500;
                    sock.Connect(point);
                    return true;
                }
            }
            catch (SocketException ex)
            {
                return false;
            }
        }

        public static bool IsPathExist(string str)
        {
            if (str == null)
                return false;
            return System.IO.Directory.Exists(str);
        }

        public static bool IsFileExist(string str)
        {
            if (str == null)
                return false;
            return File.Exists(str);
        }

        public static string GetCurrentExeFolderPath()
        {
            return System.Windows.Forms.Application.StartupPath;
        }

        public static string GetElfExePath()
        {
            return System.Windows.Forms.Application.StartupPath + "\\IC_ELF.exe";
        }

        /// <summary>
        /// 序列化到本地（授课进度）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="fileName"></param>
        public static void SerializeScoreFieldsSetting<T>(T t, string fileName)
        {
            string path = fileName.Substring(0, fileName.LastIndexOf('/'));
            //path = @AppDomain.CurrentDomain.BaseDirectory + path;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                formatter.Serialize(fs, t);
            }
        }

        /// <summary>
        /// 反序列化（授课进度）
        /// </summary>
        /// <returns></returns>
        public static T DeSerializeScoreFieldsSetting<T>(string fileName)
        {
            T obiBaseInfo;
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                obiBaseInfo = (T)formatter.Deserialize(fs);
            }
            return obiBaseInfo;
        }

        /// <summary>
        /// 序列化对象到本地文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="strPath"></param> 文件全路径
        public static void SerializeObjectToFile<T>(T t, string strPath)
        {
            string folderPath = strPath.Substring(0, strPath.LastIndexOf('/'));

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            using (FileStream fs = new FileStream(strPath, FileMode.Create))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                formatter.Serialize(fs, t);
            }
        }

        /// <summary>
        /// 从本地文件序列化到对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static T SerializeFileToObject<T>(string fileName)
        {

            T obiBaseInfo;
            using (FileStream fs = new FileStream(fileName, FileMode.Open))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T));
                obiBaseInfo = (T)formatter.Deserialize(fs);
            }
            return obiBaseInfo;
        }

        //电脑物理地址
        private static string strMacAddress = "";

        /// <summary>  
        /// 获取本机MAC地址  
        /// </summary>  
        /// <returns>本机MAC地址</returns>  
        public static string GetMacAddress()
        {
            try
            {
                if (string.Empty != strMacAddress)
                    return strMacAddress;
                else
                {
                    //string strMac = string.Empty;
                    ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                    ManagementObjectCollection moc = mc.GetInstances();
                    foreach (ManagementObject mo in moc)
                    {
                        if ((bool)mo["IPEnabled"] == true)
                        {
                            strMacAddress = mo["MacAddress"].ToString();
                        }
                    }
                    moc = null;
                    mc = null;
                    return strMacAddress;
                }

            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 找到指定的进程名称并杀掉该进程
        /// </summary>
        /// <param name="processName"></param>
        public static void FindAndKillProcess(string processName)
        {
            Process[] processlist = Process.GetProcesses();

            List<Process> resultList = new List<Process>();
            foreach (Process process in processlist)
            {
                if (process.ProcessName == processName)
                {
                    process.Kill();
                }
            }
        }

        /// <summary>
        /// 删除指定文件夹下的所有文件
        /// </summary>
        /// <param name="dirFolder"></param>
        public static void DeleteFolder(string dirFolder)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(dirFolder);
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();  //返回目录中所有文件和子目录
                foreach (FileSystemInfo i in fileinfo)
                {
                    try
                    {
                        if (i is DirectoryInfo)            //判断是否文件夹
                        {
                            DirectoryInfo subdir = new DirectoryInfo(i.FullName);
                            subdir.Delete(true);          //删除子目录和文件
                        }
                        else
                        {
                            File.Delete(i.FullName);      //删除指定文件
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.Error("删除文件失败" + e.StackTrace);
                    }
                }
            }
            catch (Exception ee)
            {
                Logger.Error("删除文件失败" + ee.StackTrace);
            }
        }
        /// <summary>
        /// 根据检索磁盘名称查找磁盘对象
        /// </summary>
        /// <param name="findDriveName">要检索的磁盘名称 例如:D</param>
        /// <returns></returns>
        public static DriveInfo GetDriveInfo(string findDriveName)
        {
            DriveInfo[] allDirves = DriveInfo.GetDrives();
            //检索计算机上的所有逻辑驱动器名称
            foreach (DriveInfo item in allDirves)
            {
                if (item.Name.Contains(findDriveName))
                {
                    return item;
                }
            }
            return null;
        }

        /// <summary>
        /// 根据文件路径检索 文件路径下的文件大小
        /// </summary>
        /// <param name="dirPath">文件路径</param>
        /// <returns>大小b</returns>
        public static long GetDirectoryLength(string dirPath)
        {
            long len = 0;
            try
            {
                //判断给定的路径是否存在,如果不存在则创建
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }


                //定义一个DirectoryInfo对象
                DirectoryInfo di = new DirectoryInfo(dirPath);

                //通过GetFiles方法,获取di目录中的所有文件的大小
                foreach (FileInfo fi in di.GetFiles())
                {
                    len += fi.Length;
                }

                //获取di中所有的文件夹,并存到一个新的对象数组中,以进行递归
                DirectoryInfo[] dis = di.GetDirectories();
                if (dis.Length > 0)
                {
                    for (int i = 0; i < dis.Length; i++)
                    {
                        len += GetDirectoryLength(dis[i].FullName);
                    }
                }
            }
            catch (Exception ee)
            {
                Logger.Error("GetDirectoryLength：" + ee.StackTrace);
            }

            return len;
        }
        //字节转换为G
        public static long FormatByteToG(long number)
        {
            return number / 1024 / 1024 / 1024;
        }

        /// <summary>
        /// 删除 存留天数之前的文件
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <param name="stayTime">存留的天数</param>
        public static bool DeleteDateFile(string path, int stayTime)
        {
            FileInfo[] fileInfo = new DirectoryInfo(path).GetFiles();
            DateTime dateTime = DateTime.Now;
            foreach (var item in fileInfo)
            {
                if (dateTime.Subtract(item.LastWriteTime).TotalDays > stayTime)
                {
                    File.Delete(item.FullName);
                }
            }
            return true;
        }
        /// <summary>
        /// 查找文件是否存在本地
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool FindFileExist(string url)
        {
            if (File.Exists(url))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// 判断文件是否在指定大小内
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <param name="size">文件大小</param>
        /// <returns>如果在指定大小内 返回true,不再指定大小内返回false</returns>
        public static bool FindFileSize(string path, long size)
        {
            FileInfo fileInfo = new FileInfo(path);
            if (fileInfo.Length < size)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 删除指定的文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns></returns>
        public static bool DeleteFile(string path)
        {
            bool result = false;
            try
            {
                File.Delete(path);
                result = true;
            }
            catch (Exception e)
            {
                result = false;
                Logger.Error("删除文件异常:" + path + ":" + e.StackTrace);
            }
            return result;
        }


        /// <summary>
        /// 根据文件路径检索 文件对象
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static FileInfo[] GetFileList(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    return new DirectoryInfo(path).GetFiles();
                }
            }
            catch (Exception e)
            {
                Logger.Error("检索文件异常:" + path + ":" + e.StackTrace);
            }
            return null;
        }

        /// <summary>
        /// 去除文件名 中间的空格
        /// </summary>
        /// <param name="temp">文件路径</param>
        /// <returns>格式化后的字符串</returns>
        public static string FormartString(string temp)
        {
            temp = "\"" + temp + "\"";
            temp = temp.Replace("/", @"\");
            return temp;
        }

        public static string GetIpAdress(string url)
        {
            string temp = null;
            try
            {
                temp = url.Substring(url.IndexOfAny(new char[] {
                '/',
                ','
            }) + 2);
                temp = temp.Substring(0, temp.IndexOf('/'));
            }
            catch (Exception)
            {

            }
            return temp;
        }

        //颜色转换  RGB与CMYK之间的转换
        public static void RGB2CMYK(int red, int green, int blue, out double c, out double m, out double y, out double k)
        {
            c = (double)(255 - red) / 255;
            m = (double)(255 - green) / 255;
            y = (double)(255 - blue) / 255;

            k = (double)Math.Min(c, Math.Min(m, y));
            if (k == 1.0)
            {
                c = m = y = 0;
            }
            else
            {
                c = (c - k) / (1 - k);
                m = (m - k) / (1 - k);
                y = (y - k) / (1 - k);
            }
        }
        public static void CMYK2RGB(double c, double m, double y, double k, out int r, out int g, out int b)
        {
            r = Convert.ToInt32((1.0 - c) * (1.0 - k) * 255.0);
            g = Convert.ToInt32((1.0 - m) * (1.0 - k) * 255.0);
            b = Convert.ToInt32((1.0 - y) * (1.0 - k) * 255.0);
        }

        public static void RGB2HSB(int red, int green, int blue, out double hue, out double sat, out double bri)
        {
            double r = ((double)red / 255.0);
            double g = ((double)green / 255.0);
            double b = ((double)blue / 255.0);

            double max = Math.Max(r, Math.Max(g, b));
            double min = Math.Min(r, Math.Min(g, b));

            hue = 0.0;
            if (max == r && g >= b)
            {
                if (max - min == 0) hue = 0.0;
                else hue = 60 * (g - b) / (max - min);
            }
            else if (max == r && g < b)
            {
                hue = 60 * (g - b) / (max - min) + 360;
            }
            else if (max == g)
            {
                hue = 60 * (b - r) / (max - min) + 120;
            }
            else if (max == b)
            {
                hue = 60 * (r - g) / (max - min) + 240;
            }

            sat = (max == 0) ? 0.0 : (1.0 - ((double)min / (double)max));
            bri = max;
        }
        public static void HSB2RGB(double hue, double sat, double bri, out int red, out int green, out int blue)
        {
            double r = 0;
            double g = 0;
            double b = 0;

            if (sat == 0)
            {
                r = g = b = bri;
            }
            else
            {
                // the color wheel consists of 6 sectors. Figure out which sector you're in.
                double sectorPos = hue / 60.0;
                int sectorNumber = (int)(Math.Floor(sectorPos));
                // get the fractional part of the sector
                double fractionalSector = sectorPos - sectorNumber;

                // calculate values for the three axes of the color. 
                double p = bri * (1.0 - sat);
                double q = bri * (1.0 - (sat * fractionalSector));
                double t = bri * (1.0 - (sat * (1 - fractionalSector)));

                // assign the fractional colors to r, g, and b based on the sector the angle is in.
                switch (sectorNumber)
                {
                    case 0:
                        r = bri;
                        g = t;
                        b = p;
                        break;
                    case 1:
                        r = q;
                        g = bri;
                        b = p;
                        break;
                    case 2:
                        r = p;
                        g = bri;
                        b = t;
                        break;
                    case 3:
                        r = p;
                        g = q;
                        b = bri;
                        break;
                    case 4:
                        r = t;
                        g = p;
                        b = bri;
                        break;
                    case 5:
                        r = bri;
                        g = p;
                        b = q;
                        break;
                }
            }
            red = Convert.ToInt32(r * 255);
            green = Convert.ToInt32(g * 255);
            blue = Convert.ToInt32(b * 255); ;
        }

        /// <summary>
        /// RGB与十六进制Hex表示的转换
        /// </summary>
        /// <param name="r"></param>
        /// <param name="g"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string RGB2Hex(int r, int g, int b)
        {
            return String.Format("#{0:x2}{1:x2}{2:x2}", (int)r, (int)g, (int)b);
        }
        public static Color Hex2Color(string hexColor)
        {
            string r, g, b;

            if (hexColor != String.Empty)
            {
                hexColor = hexColor.Trim();
                if (hexColor[0] == '#') hexColor = hexColor.Substring(1, hexColor.Length - 1);

                r = hexColor.Substring(0, 2);
                g = hexColor.Substring(2, 2);
                b = hexColor.Substring(4, 2);

                r = Convert.ToString(16 * GetIntFromHex(r.Substring(0, 1)) + GetIntFromHex(r.Substring(1, 1)));
                g = Convert.ToString(16 * GetIntFromHex(g.Substring(0, 1)) + GetIntFromHex(g.Substring(1, 1)));
                b = Convert.ToString(16 * GetIntFromHex(b.Substring(0, 1)) + GetIntFromHex(b.Substring(1, 1)));

                return Color.FromArgb(Convert.ToInt32(r), Convert.ToInt32(g), Convert.ToInt32(b));
            }

            return Color.Empty;
        }
        private static int GetIntFromHex(string strHex)
        {
            switch (strHex)
            {
                case ("A"):
                    {
                        return 10;
                    }
                case ("B"):
                    {
                        return 11;
                    }
                case ("C"):
                    {
                        return 12;
                    }
                case ("D"):
                    {
                        return 13;
                    }
                case ("E"):
                    {
                        return 14;
                    }
                case ("F"):
                    {
                        return 15;
                    }
                default:
                    {
                        return int.Parse(strHex);
                    }
            }
        }
        /// <summary>
        /// 检查网络文件是否存在
        /// </summary>
        /// <param name="uri">url地址</param>
        /// <returns></returns>
        public static bool IsNetFileExist(string uri)
        {
            HttpWebRequest req = null;
            HttpWebResponse res = null;
            try
            {
                req = (HttpWebRequest)WebRequest.Create(uri);
                req.Method = "HEAD";
                req.Timeout = 100;
                res = (HttpWebResponse)req.GetResponse();
                return (res.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                return false;
            }
            finally
            {
                if (res != null)
                {
                    res.Close();
                    res = null;
                }
                if (req != null)
                {
                    req.Abort();
                    req = null;
                }
            }
        }
        /// <summary>
        /// 将url转换为utf-8格式
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string FormatURLToUTF8(string url)
        {
            return HttpUtility.UrlDecode(url, Encoding.GetEncoding("UTF-8"));
        }


        /// <summary>
        /// 获取一个随机数
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static int GetRandNumber(int count)
        {
            Random random = new Random();
            int result = random.Next(0, count - 1);
            return result;
        }

        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="patch"></param>
        public static void CreatPatch(string patch)
        {
            Directory.CreateDirectory(patch);
        }


        /// <summary>
        /// 获取主板编号
        /// </summary>
        public static void GetBorad()
        {


        }
    }
}
