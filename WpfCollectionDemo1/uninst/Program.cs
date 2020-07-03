using System;

namespace uninst
{
    class Program
    {
        static void Main(string[] args)
        {
            string sysroot = System.Environment.SystemDirectory;
            System.Diagnostics.Process.Start(sysroot + "//msiexec.exe", "/x {E2DC9D37-B80B-48F5-BCD2-EC89BA96C8A9} /qr");
        }
    }
}
