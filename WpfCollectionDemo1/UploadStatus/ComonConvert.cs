
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UploadStatus
{
    public class ComonConvert
    {

        public static MainWindow MainWindow;


        public static object FindResource(string fileName)
        {
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                return MainWindow.FindResource(fileName);
            }
            return null;
        }
     
    }
}
