using System;
using System.IO;
using System.Windows;

using System.Diagnostics;
using TerzoChat.View;
using Demo;
using Google.Protobuf;
using Grpc;
using Grpc.Core;
using grpc2slib;
using grpc2slib.BBL;
using Pb;
using UrusTools.Config;
using TerzoChat.Config;
using System.ComponentModel;

namespace TerzoChat
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private BaseRPCService rpcService;
        private string nbsProcessName = "";
        public MainWindow()
        {
           
            AppState.Instance.RPC_RUNNING = CheckedNBSRunning();
           
            if (!AppState.Instance.RPC_RUNNING)
            {
                StartNBSGrpc();
            }
            checkedAndCreateNBS();

            InitializeComponent();
            this.LoadChat();
            
        }

        private void LoadChat()
        {
            this.RChatContainer.Children.Clear();
            this.NickLabel.Text = AppState.Instance.CID;
            Console.WriteLine("==============================CID===>" + AppState.Instance.CID);
            this.RChatContainer.Children.Add(new ChatMainView());
        }

        private bool CheckedNBSRunning()
        {
            Process[] processes;
            try
            {
                processes = Process.GetProcessesByName("nbs");
                Console.WriteLine("nbs process :" + processes.Length.ToString());
                return processes.Length > 0; 
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void setNickName()
        {
            if (AppState.Instance.CID != null) this.NickLabel.Text = AppState.Instance.CID;
        }
  

        private void StartNBSGrpc()
        {
            try
            {
                Process nbsp = new Process();
                nbsp.StartInfo.UseShellExecute = false;
                nbsp.StartInfo.FileName = AppState.Instance.START_PATH + System.IO.Path.DirectorySeparatorChar + "nbs.exe";
                nbsp.StartInfo.CreateNoWindow = true;
                
                nbsp.EnableRaisingEvents = ConfigManager.Instance.BackProcess();
                bool r = nbsp.Start();
                nbsp.WaitForExit(5000);
                nbsProcessName = nbsp.ProcessName;
                Console.WriteLine("启动》》" + r.ToString()+">>>>"+nbsp.ProcessName);
                AppState.Instance.RPC_RUNNING = true;
            }
            catch (Exception e)
            {
                AppState.Instance.RPC_RUNNING = false;
                Console.WriteLine(e.Message);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            MessageBoxResult boxResult =
                MessageBox.Show("是否继续运行NBS服务?","询问" ,MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(boxResult == MessageBoxResult.No)
            {
                stopGRPCServer();
            }
            //base.OnClosing(e);
            e.Cancel = false;
        }
        //
        private void stopGRPCServer()
        {
            if (String.IsNullOrEmpty(nbsProcessName)) nbsProcessName = "nbs";
            Process[] kps = Process.GetProcessesByName(nbsProcessName);
            if (kps.Length > 0)
            {
                foreach(Process pk in kps)
                {
                    try {
                        pk.Kill();
                    } catch { }
                   
                }
            }
            
        }

        private void checkedAndCreateNBS()
        {
            rpcService = new BaseRPCService();
            //创建账户
            try
            {
                AppState.Instance.CID =rpcService.GetAccount();
                AppState.Instance.RPC_RUNNING = true;
            }
            catch(RpcException e)
            {
                AppState.Instance.RPC_RUNNING = false;
                Console.WriteLine("====================>>>Grpc error:"+e.Message);
            }
            catch (RpcResultException)
            {
                //create
                try
                {
                    rpcService.CreateAccountOffline();
                    AppState.Instance.CID = rpcService.GetAccount();
                    AppState.Instance.RPC_RUNNING = true;
                }
                catch(RpcException re)
                { 
                    AppState.Instance.RPC_RUNNING = false;
                    Console.WriteLine("====================>>>Grpc create error:" + re.Message);
                }
            }
            finally
            {
                if (!AppState.Instance.RPC_RUNNING)
                {
                    //TO taskbarmsg
                }
            }           
        }
    }
}
