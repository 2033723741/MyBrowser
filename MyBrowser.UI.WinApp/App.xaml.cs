﻿using MyBrowser.UI.WinApp.Navigator;
using MyBrowser.UI.WinApp.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MyBrowser.UI.WinApp
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public void App_Startup(object sender, StartupEventArgs e)
        {
            Window window = WindowNavigator.GetWindow<MainWindow>();
            window.Show();
        }
    }
}
