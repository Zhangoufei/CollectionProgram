using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Xml.Serialization;

namespace Com.Zhang.Common
{
    public class CommonUntility
    {

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
                using (Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
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
            }
            catch (Exception)
            {

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
            //判断给定的路径是否存在,如果不存在则创建
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
            long len = 0;

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
        /// 根据文件路径检索 文件对象
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static FileInfo[] GetFileList(string path)
        {
            if (Directory.Exists(path))
            {
                return new DirectoryInfo(path).GetFiles();
            }
            else
            {
                return null;
            }
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


        //查找进程、结束进程
        public static void killProcess()
        {
            Process[] pro = Process.GetProcesses();//获取已开启的所有进程

            //遍历所有查找到的进程

            for (int i = 0; i < pro.Length; i++)
            {
                //判断此进程是否是要查找的进程
                if (pro[i].ProcessName.ToString().ToLower() == "pc_task")
                {
                    pro[i].Kill();//结束进程
                }
            }
        }


        //根据名称查找是否有当前进程
        public static bool FindProcess(string processName)
        {
            Process[] pro = Process.GetProcesses();//获取已开启的所有进程

            //遍历所有查找到的进程
            for (int i = 0; i < pro.Length; i++)
            {
                //判断此进程是否是要查找的进程
                if (pro[i].ProcessName.ToString().ToLower() == processName.ToLower())
                {
                    //pro[i].Kill();//结束进程
                    return true;
                }
            }
            return false;
        }
    }
}
