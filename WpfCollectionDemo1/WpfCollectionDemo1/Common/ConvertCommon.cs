using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfCollectionDemo1.Common
{
    public class ConvertCommon
    {

        public static MainWindow MainWindow;


        public static object FindResource(string path) {

            return MainWindow.FindResource(path);
        }

    }
}
