using Microsoft.Win32;
using System;
using System.Collections;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Tools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            monitor = new NetworkMonitor();
            this.adapters = monitor.Adapters;
            /* If the length of adapters is zero, then no instance 
             * exists in the networking category of performance console.*/
            if (adapters.Length == 0)
            {
                this.ListAdapters.Enabled = false;
                MessageBox.Show("No network adapters found on this computer.");
                return;
            }
            this.ListAdapters.Items.AddRange(this.adapters);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dateTime = dateTimePicker1.Text;

            //  DateTimeHelper.SetLocalDateTime(Convert.ToDateTime(dateTime));
            //SYSTEMTIME st = new SYSTEMTIME();
            //st.FromDateTime(Convert.ToDateTime(dateTime));
            //Win32API.SetLocalTime(ref st);

        }
        private NetworkAdapter[] adapters;
        private NetworkMonitor monitor;

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void ListAdapters_SelectedIndexChanged(object sender, EventArgs e)
        {
            monitor.StopMonitoring();
            // Start a timer to obtain new performance counter sample every second.
            monitor.StartMonitoring(adapters[this.ListAdapters.SelectedIndex]);
            this.TimerCounter.Start();
        }

        private void TimerCounter_Tick(object sender, EventArgs e)
        {
            NetworkAdapter adapter = this.adapters[this.ListAdapters.SelectedIndex];
            /* The DownloadSpeedKbps and UploadSpeedKbps are double values. You can also 
             * use properties DownloadSpeed and UploadSpeed, which are long values but 
             * are measured in bytes per second. */
            this.LableDownloadValue.Text = String.Format("{0:n} kbps", adapter.DownloadSpeedKbps);
            this.LabelUploadValue.Text = String.Format("{0:n} kbps", adapter.UploadSpeedKbps);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowNetworkInterfaceMessage();
        }


        public static void ShowNetworkInterfaceMessage()
        {
            NetworkInterface[] fNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in fNetworkInterfaces)
            {
                #region " 网卡类型 "  
                string fCardType = "未知网卡";
                string fRegistryKey = "SYSTEM\\CurrentControlSet\\Control\\Network\\{4D36E972-E325-11CE-BFC1-08002BE10318}\\" + adapter.Id + "\\Connection";
                RegistryKey rk = Registry.LocalMachine.OpenSubKey(fRegistryKey, false);
                if (rk != null)
                {
                    // 区分 PnpInstanceID   
                    // 如果前面有 PCI 就是本机的真实网卡  
                    // MediaSubType 为 01 则是常见网卡，02为无线网卡。  
                    string fPnpInstanceID = rk.GetValue("PnpInstanceID", "").ToString();
                    int fMediaSubType = Convert.ToInt32(rk.GetValue("MediaSubType", 0));

                    if (fPnpInstanceID.Length > 3 && fPnpInstanceID.Substring(0, 3) == "PCI")
                    {
                        fCardType = "物理网卡";
                        GetNetworkInfo(adapter, fCardType);
                    }

                    else if (fMediaSubType == 1)
                    {
                        fCardType = "虚拟网卡";
                    }

                    else if (fMediaSubType == 2)
                    {
                        fCardType = "无线网卡";
                        GetNetworkInfo(adapter, fCardType);
                    }

                }
                #endregion
            }
        }

        private static void GetNetworkInfo(NetworkInterface adapter, string fCardType)
        {
            Console.WriteLine("-----------------------------------------------------------");

            Console.WriteLine("-- " + fCardType);

            Console.WriteLine("-----------------------------------------------------------");

            Console.WriteLine("Id .................. : {0}", adapter.Id); // 获取网络适配器的标识符  

            Console.WriteLine("Name ................ : {0}", adapter.Name); // 获取网络适配器的名称  

            Console.WriteLine("Description ......... : {0}", adapter.Description); // 获取接口的描述  

            Console.WriteLine("Interface type ...... : {0}", adapter.NetworkInterfaceType); // 获取接口类型  

            Console.WriteLine("Is receive only...... : {0}", adapter.IsReceiveOnly); // 获取 Boolean 值，该值指示网络接口是否设置为仅接收数据包。  

            Console.WriteLine("Multicast............ : {0}", adapter.SupportsMulticast); // 获取 Boolean 值，该值指示是否启用网络接口以接收多路广播数据包。  

            Console.WriteLine("Speed ............... : {0}", adapter.Speed); // 网络接口的速度  

            Console.WriteLine("Physical Address .... : {0}", adapter.GetPhysicalAddress().ToString()); // MAC 地址  

            IPInterfaceProperties fIPInterfaceProperties = adapter.GetIPProperties();

            UnicastIPAddressInformationCollection UnicastIPAddressInformationCollection = fIPInterfaceProperties.UnicastAddresses;

            foreach (UnicastIPAddressInformation UnicastIPAddressInformation in UnicastIPAddressInformationCollection)
            {
                if (UnicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)

                    Console.WriteLine("Ip Address .......... : {0}", UnicastIPAddressInformation.Address); // Ip 地址  
            }
            Console.WriteLine();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }
    }

}

/// <summary>
/// The NetworkMonitor class monitors network speed for each network adapter on the computer,
/// using classes for Performance counter in .NET library.
/// </summary>
public class NetworkMonitor
{
    private System.Windows.Forms.Timer timer;               // The timer event executes every second to refresh the values in adapters.
    private ArrayList adapters;         // The list of adapters on the computer.
    private ArrayList monitoredAdapters;// The list of currently monitored adapters.

    public NetworkMonitor()
    {
        this.adapters = new ArrayList();
        this.monitoredAdapters = new ArrayList();
        EnumerateNetworkAdapters();

        timer = new System.Windows.Forms.Timer();
        timer.Interval = 1000;
        timer.Tick += Timer_Tick; ;
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        foreach (NetworkAdapter adapter in this.monitoredAdapters)
            adapter.refresh();
    }

    /// <summary>
    /// Enumerates network adapters installed on the computer.
    /// </summary>
    private void EnumerateNetworkAdapters()
    {
        PerformanceCounterCategory category = new PerformanceCounterCategory("Network Interface");

        foreach (string name in category.GetInstanceNames())
        {
            // This one exists on every computer.
            if (name == "MS TCP Loopback interface")
                continue;
            // Create an instance of NetworkAdapter class, and create performance counters for it.
            NetworkAdapter adapter = new NetworkAdapter(name);
            adapter.dlCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", name);
            adapter.ulCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", name);
            this.adapters.Add(adapter); // Add it to ArrayList adapter
        }
    }


    /// <summary>
    /// Get instances of NetworkAdapter for installed adapters on this computer.
    /// </summary>
    public NetworkAdapter[] Adapters
    {
        get { return (NetworkAdapter[])this.adapters.ToArray(typeof(NetworkAdapter)); }
    }
    /// <summary>
    /// Enable the timer and add all adapters to the monitoredAdapters list, 
    /// unless the adapters list is empty.
    /// </summary>
    public void StartMonitoring()
    {
        if (this.adapters.Count > 0)
        {
            foreach (NetworkAdapter adapter in this.adapters)
                if (!this.monitoredAdapters.Contains(adapter))
                {
                    this.monitoredAdapters.Add(adapter);
                    adapter.init();
                }

            timer.Enabled = true;
        }
    }
    /// <summary>
    /// Enable the timer, and add the specified adapter to the monitoredAdapters list
    /// </summary>
    public void StartMonitoring(NetworkAdapter adapter)
    {
        if (!this.monitoredAdapters.Contains(adapter))
        {
            this.monitoredAdapters.Add(adapter);
            adapter.init();
        }
        timer.Enabled = true;
    }
    /// <summary>
    /// Disable the timer, and clear the monitoredAdapters list.
    /// </summary>
    public void StopMonitoring()
    {
        this.monitoredAdapters.Clear();
        timer.Enabled = false;
    }
    /// <summary>
    /// Remove the specified adapter from the monitoredAdapters list, and 
    /// disable the timer if the monitoredAdapters list is empty.
    /// </summary>
    public void StopMonitoring(NetworkAdapter adapter)
    {
        if (this.monitoredAdapters.Contains(adapter))
            this.monitoredAdapters.Remove(adapter);
        if (this.monitoredAdapters.Count == 0)
            timer.Enabled = false;
    }
}

/// <summary>  
/// Represents a network adapter installed on the machine.  
/// Properties of this class can be used to obtain current network speed.  
/// </summary>  
public class NetworkAdapter
{
    /// <summary>  
    /// Instances of this class are supposed to be created only in an NetworkMonitor.  
    /// </summary>  
    internal NetworkAdapter(string name)
    {
        this.name = name;
    }

    private long dlSpeed, ulSpeed;       // Download/Upload speed in bytes per second.  
    private long dlValue, ulValue;       // Download/Upload counter value in bytes.  
    private long dlValueOld, ulValueOld; // Download/Upload counter value one second earlier, in bytes.  

    internal string name;                               // The name of the adapter.  
    internal PerformanceCounter dlCounter, ulCounter;   // Performance counters to monitor download and upload speed.  
                                                        /// <summary>  
                                                        /// Preparations for monitoring.  
                                                        /// </summary>  
    internal void init()
    {
        // Since dlValueOld and ulValueOld are used in method refresh() to calculate network speed, they must have be initialized.  
        this.dlValueOld = this.dlCounter.NextSample().RawValue;
        this.ulValueOld = this.ulCounter.NextSample().RawValue;
    }
    /// <summary>  
    /// Obtain new sample from performance counters, and refresh the values saved in dlSpeed, ulSpeed, etc.  
    /// This method is supposed to be called only in NetworkMonitor, one time every second.  
    /// </summary>  
    internal void refresh()
    {
        this.dlValue = this.dlCounter.NextSample().RawValue;
        this.ulValue = this.ulCounter.NextSample().RawValue;

        // Calculates download and upload speed.  
        this.dlSpeed = this.dlValue - this.dlValueOld;
        this.ulSpeed = this.ulValue - this.ulValueOld;

        this.dlValueOld = this.dlValue;
        this.ulValueOld = this.ulValue;
    }
    /// <summary>  
    /// Overrides method to return the name of the adapter.  
    /// </summary>  
    /// <returns>The name of the adapter.</returns>  
    public override string ToString()
    {
        return this.name;
    }
    /// <summary>  
    /// The name of the network adapter.  
    /// </summary>  
    public string Name
    {
        get { return this.name; }
    }
    /// <summary>  
    /// Current download speed in bytes per second.  
    /// </summary>  
    public long DownloadSpeed
    {
        get { return this.dlSpeed; }
    }
    /// <summary>  
    /// Current upload speed in bytes per second.  
    /// </summary>  
    public long UploadSpeed
    {
        get { return this.ulSpeed; }
    }
    /// <summary>  
    /// Current download speed in kbytes per second.  
    /// </summary>  
    public double DownloadSpeedKbps
    {
        get { return this.dlSpeed / 1024.0; }
    }
    /// <summary>  
    /// Current upload speed in kbytes per second.  
    /// </summary>  
    public double UploadSpeedKbps
    {
        get { return this.ulSpeed / 1024.0; }
    }
}

/// <summary>
/// 
/// </summary>
public struct SYSTEMTIME
{
    public ushort wYear;
    public ushort wMonth;
    public ushort wDayOfWeek;
    public ushort wDay;
    public ushort wHour;
    public ushort wMinute;
    public ushort wSecond;
    public ushort wMilliseconds;

    /// <summary>
    /// 从System.DateTime转换。
    /// </summary>
    /// <param name="time">System.DateTime类型的时间。</param>
    public void FromDateTime(DateTime time)
    {
        wYear = (ushort)time.Year;
        wMonth = (ushort)time.Month;
        wDayOfWeek = (ushort)time.DayOfWeek;
        wDay = (ushort)time.Day;
        wHour = (ushort)time.Hour;
        wMinute = (ushort)time.Minute;
        wSecond = (ushort)time.Second;
        wMilliseconds = (ushort)time.Millisecond;
    }
    /// <summary>
    /// 转换为System.DateTime类型。
    /// </summary>
    /// <returns></returns>
    public DateTime ToDateTime()
    {
        return new DateTime(wYear, wMonth, wDay, wHour, wMinute, wSecond, wMilliseconds);
    }
    /// <summary>
    /// 静态方法。转换为System.DateTime类型。
    /// </summary>
    /// <param name="time">SYSTEMTIME类型的时间。</param>
    /// <returns></returns>
    public static DateTime ToDateTime(SYSTEMTIME time)
    {
        return time.ToDateTime();
    }
}
public class Win32API
{
    [DllImport("Kernel32.dll")]
    public static extern bool SetLocalTime(ref SYSTEMTIME Time);
    [DllImport("Kernel32.dll")]
    public static extern void GetLocalTime(ref SYSTEMTIME Time);
}


public class DateTimeHelper
{
    /// <summary>
    /// 设置本地电脑的年月日
    /// </summary>
    /// <param name="year"></param>
    /// <param name="month"></param>
    /// <param name="day"></param>
    public static void SetLocalDate(int year, int month, int day)
    {
        //实例一个Process类，启动一个独立进程 
        Process p = new Process();
        //Process类有一个StartInfo属性 
        //设定程序名 
        p.StartInfo.FileName = "cmd.exe";
        //设定程式执行参数 “/C”表示执行完命令后马上退出
        p.StartInfo.Arguments = string.Format("/c date {0}-{1}-{2}", year, month, day);
        //关闭Shell的使用 
        p.StartInfo.UseShellExecute = false;
        //重定向标准输入 
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardOutput = true;
        //重定向错误输出 
        p.StartInfo.RedirectStandardError = true;
        //设置不显示doc窗口 
        p.StartInfo.CreateNoWindow = true;
        //启动 
        p.Start();
        //从输出流取得命令执行结果 
        p.StandardOutput.ReadToEnd();
    }

    /// <summary>
    /// 设置本机电脑的时分秒
    /// </summary>
    /// <param name="hour"></param>
    /// <param name="min"></param>
    /// <param name="sec"></param>
    public static void SetLocalTime(int hour, int min, int sec)
    {
        //实例一个Process类，启动一个独立进程 
        Process p = new Process();
        //Process类有一个StartInfo属性 
        //设定程序名 
        p.StartInfo.FileName = "cmd.exe";
        //设定程式执行参数 “/C”表示执行完命令后马上退出
        p.StartInfo.Arguments = string.Format("/c time {0}:{1}:{2}", hour, min, sec);
        //关闭Shell的使用 
        p.StartInfo.UseShellExecute = false;
        //重定向标准输入 
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardOutput = true;
        //重定向错误输出 
        p.StartInfo.RedirectStandardError = true;
        //设置不显示doc窗口 
        p.StartInfo.CreateNoWindow = true;
        //启动 
        p.Start();
        //从输出流取得命令执行结果 
        p.StandardOutput.ReadToEnd();
    }

    /// <summary>
    /// 设置本机电脑的年月日和时分秒
    /// </summary>
    /// <param name="time"></param>
    public static void SetLocalDateTime(DateTime time)
    {
        SetLocalDate(time.Year, time.Month, time.Day);
        SetLocalTime(time.Hour, time.Minute, time.Second);
    }







}

