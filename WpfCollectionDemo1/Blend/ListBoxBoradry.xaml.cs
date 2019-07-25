﻿using Blend.ViewModel;
using Com.Zhang.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Blend
{
    /// <summary>
    /// ListBoxBoradry.xaml 的交互逻辑
    /// </summary>
    public partial class ListBoxBoradry : Page
    {

        ListBoxBoradryVM listBoxBoradryVM;

        public ListBoxBoradry()
        {
            InitializeComponent();


            DataContext = listBoxBoradryVM = new ListBoxBoradryVM();
        }




        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
            listBoxBoradryVM.InitObservable(CommonUntility.GetFileList(@"D:\picture\picture"));

        }
    }
}
