using AutoUpdaterDotNET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace 磁盘操作
{
    /// <summary>
    /// 读取磁盘信息.xaml 的交互逻辑
    /// </summary>
    public partial class 读取磁盘信息 : Page
    {
        public 读取磁盘信息()
        {
            InitializeComponent();

            dataGird.ItemsSource = DriveInfo.GetDrives();


            DriveInfo cDriveInfo = GetDriveInfo("C");
            ctextName.Text = FormatByteToG(cDriveInfo.TotalSize).ToString();
            leaveNameC.Text = FormatByteToG(cDriveInfo.TotalFreeSpace).ToString();

            DriveInfo dDriveInfo = GetDriveInfo("D");
            dtextName.Text = FormatByteToG(dDriveInfo.TotalSize).ToString();
            leaveNameD.Text = FormatByteToG(dDriveInfo.TotalFreeSpace).ToString();


            long tempSize = GetDirectorySpace(paht);

            //ntextName.Text = tempSize.ToString();
            //leaveNamen.Text = (GetDirectorySpace(AppDomain.CurrentDomain.BaseDirectory) - tempSize).ToString();


            ntextName.Text = GetDirectoryLength(paht).ToString();
            leaveNamen.Text = GetDirectoryLength(AppDomain.CurrentDomain.BaseDirectory).ToString();

            GetFils();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AutoUpdater.Mandatory = true;
            AutoUpdater.Start("http://192.168.18.106:9050/Info.xml");
            AutoUpdater.CheckForUpdateEvent += AutoUpdater_CheckForUpdateEvent;
        }

        private void AutoUpdater_CheckForUpdateEvent(UpdateInfoEventArgs args)
        {
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            // AutoUpdater.DownloadUpdate();
            //AutoUpdater.ShowUpdateForm();

            if (AutoUpdater.DownloadUpdate())
            {
                CommonTest.MainWindow.Close();
            }
        }

        public static long GetDirectoryLength(string dirPath)
        {
            //判断给定的路径是否存在,如果不存在则退出
            if (!Directory.Exists(dirPath))
                return 0;
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
            return number / 1024 / 1024;
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
                if (item.Name.Contains(findDriveName.ToUpper()))
                {
                    return item;
                }
            }
            return null;
        }
        private static string paht = AppDomain.CurrentDomain.BaseDirectory + "//file";
        //删除 三天之前的文件
        public static void GetFils()
        {

            FileInfo[] fileInfo = new DirectoryInfo(paht).GetFiles();
            DateTime dateTime = DateTime.Now;
            foreach (var item in fileInfo)
            {
                if (dateTime.Subtract(item.LastWriteTime).TotalDays > 3)
                {
                    File.Delete(item.FullName);
                }
            }
        }

        /// <summary>
        /// 获取指定路径的占用空间
        /// </summary>
        /// <param name="dirPath">路径</param>
        /// <returns></returns>
        public static long GetDirectorySpace(string dirPath)
        {
            //返回值
            long len = 0;
            //判断该路径是否存在（是否为文件夹）
            if (!Directory.Exists(dirPath))
            {
                //如果是文件，则调用
                len = FileSpace(dirPath);
            }
            else
            {
                //定义一个DirectoryInfo对象
                DirectoryInfo di = new DirectoryInfo(dirPath);
                //本机的簇值
                long clusterSize = GetClusterSize(di);
                //遍历目录下的文件，获取总占用空间
                foreach (FileInfo fi in di.GetFiles())
                {
                    //文件大小除以簇，余若不为0
                    if (fi.Length % clusterSize != 0)
                    {
                        decimal res = fi.Length / clusterSize;
                        //文件大小除以簇，取整数加1。为该文件占用簇的值
                        int clu = Convert.ToInt32(Math.Ceiling(res)) + 1;
                        long result = clusterSize * clu;
                        len += result;
                    }
                    else
                    {
                        //余若为0，则占用空间等于文件大小
                        len += fi.Length;
                    }
                }
                //获取di中所有的文件夹，并存到一个新的对象数组中，以进行递归
                DirectoryInfo[] dis = di.GetDirectories();
                if (dis.Length > 0)
                {
                    for (int i = 0; i < dis.Length; i++)
                    {
                        len += GetDirectorySpace(dis[i].FullName);
                    }
                }
            }
            return len;
        }
        //所给路径中所对应的文件占用空间
        public static long FileSpace(string filePath)
        {
            long temp = 0;
            //定义一个FileInfo对象，是指与filePath所指向的文件相关联，以获取其大小
            FileInfo fileInfo = new FileInfo(filePath);
            long clusterSize = GetClusterSize(fileInfo);
            if (fileInfo.Length % clusterSize != 0)
            {
                decimal res = fileInfo.Length / clusterSize;
                int clu = Convert.ToInt32(Math.Ceiling(res)) + 1;
                temp = clusterSize * clu;
            }
            else
            {
                return fileInfo.Length;
            }
            return temp;
        }
        /// <summary>
        /// 获取每簇的字节
        /// </summary>
        /// <param name="file">指定文件</param>
        /// <returns></returns>
        public static long GetClusterSize(FileInfo file)
        {
            long clusterSize = 0;
            DiskInfo diskInfo = new DiskInfo();
            diskInfo = GetDiskInfo(file.Directory.Root.FullName);
            clusterSize = (diskInfo.BytesPerSector * diskInfo.SectorsPerCluster);
            return clusterSize;
        }
        /// <summary>
        /// 获取每簇的字节
        /// </summary>
        /// <param name="dir">指定目录</param>
        /// <returns></returns>
        public static long GetClusterSize(DirectoryInfo dir)
        {
            long clusterSize = 0;
            DiskInfo diskInfo = new DiskInfo();
            diskInfo = GetDiskInfo(dir.Root.FullName);
            clusterSize = (diskInfo.BytesPerSector * diskInfo.SectorsPerCluster);
            return clusterSize;
        }
        //// <summary>
        /// 结构。硬盘信息
        /// </summary>
        public struct DiskInfo
        {
            public string RootPathName;
            //每簇的扇区数
            public int SectorsPerCluster;
            //每扇区字节
            public int BytesPerSector;
            public int NumberOfFreeClusters;
            public int TotalNumberOfClusters;
        }
        public static DiskInfo GetDiskInfo(string rootPathName)
        {
            DiskInfo diskInfo = new DiskInfo();
            int sectorsPerCluster = 0, bytesPerSector = 0, numberOfFreeClusters = 0, totalNumberOfClusters = 0;
            GetDiskFreeSpace(rootPathName, ref sectorsPerCluster, ref bytesPerSector, ref numberOfFreeClusters, ref totalNumberOfClusters);

            //每簇的扇区数
            diskInfo.SectorsPerCluster = sectorsPerCluster;
            //每扇区字节
            diskInfo.BytesPerSector = bytesPerSector;

            return diskInfo;
        }
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern bool GetDiskFreeSpace([MarshalAs(UnmanagedType.LPTStr)]string rootPathName,
ref int sectorsPerCluster, ref int bytesPerSector, ref int numberOfFreeClusters, ref int totalNumbeOfClusters);

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string path = @"D:\picture\视频资源\bandicam 2019-03-01 10-40-48-096.mp4";
            if (File.Exists(path)) {

                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Length < 1024) {

                }
            }
        }
    }
}
