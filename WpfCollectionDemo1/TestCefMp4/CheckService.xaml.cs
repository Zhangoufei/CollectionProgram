﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TestCefMp4
{
    /// <summary>
    /// CheckService.xaml 的交互逻辑
    /// </summary>
    public partial class CheckService : Window
    {
        public CheckService()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
           var result=  await HttpService.GetUserInfo("001986030");
        }
    }
}
