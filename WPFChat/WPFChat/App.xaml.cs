using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Windows;
using ChatModel;
using WPFChat.View;

namespace WPFChat
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        //单例运行
        Mutex mutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            bool startupFlag;

            Mutex m = new Mutex(true, GlobalOpts.ApplicationName, out startupFlag);

            if (!startupFlag)
            {
                if (!GlobalOpts.IsMainShow)
                {
                    GlobalOpts.IsMainShow = true;
                    //active run
                }
                MessageBox.Show("程序已经启动!");
                Environment.Exit(0);

            }
            else
            {
                if(e.Args.Length > 0)
                {

                }

                OnInit();
                //LoginWPF login = new LoginWPF();
                //login.Show();
                MainWindow window = new MainWindow();
                window.Show();
            }
        }
        /**
         * 启动前加载
         */ 
        private void OnInit()
        {

        }
    }
}
