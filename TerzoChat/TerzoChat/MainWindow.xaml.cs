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
using System.Threading;

namespace TerzoChat
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        private string nbsProcessName = "";
        private GrpcBaseHelper GrpcBase;
        private bool Debug = true;
        private AccountServiceClient accountService;
        public MainWindow()
        {
            AppState.Instance.RPC_RUNNING = CheckedNBSRunning();
            GrpcBase = GrpcBaseHelper.Instance();
            if (!AppState.Instance.RPC_RUNNING)
            {
                CreateAccountOffline("123456");
                StartNBSGrpc();
            }
            //
            initialGrpcService();
            GetVersion();//
            GetAccount();
            InitializeComponent();
            this.LoadChat();
            
        }

        private void GetVersion()
        {
            if (this.accountService == null) return;
            try
            {
                string ver = accountService.GetCurrentVersion();
                Console.WriteLine("Version>>>>>>>>>>>>>>>>" + ver);
                AppState.Instance.GrpcServerVersion = ver;
                AppState.Instance.RPC_RUNNING = true;
            }
            catch
            {
                AppState.Instance.RPC_RUNNING = false;
            }
        }
        /// <summary>
        /// 加载GrpcService
        /// </summary>
        private void initialGrpcService()
        {
            //加载GrpcBase
            this.accountService = new AccountServiceClient(
                new VersionTask.VersionTaskClient(GrpcBase.Channel),
                new AccountTask.AccountTaskClient(GrpcBase.Channel),
                Debug
                );

        }

        private void GetAccount()
        {
            if (this.accountService == null) return;
            try
            {
                string acid = accountService.GetAccountHID();
                if (String.IsNullOrEmpty(acid))
                {
                    AppState.Instance.CID = "";
                }
                else
                {
                    AppState.Instance.CID = acid;
                }
            }
            catch
            {

            }
        }

        private void CreateAccountOffline(string pw)
        {
            Process ncp = new Process();
            ncp.StartInfo.UseShellExecute = false;
            ncp.StartInfo.FileName = AppState.Instance.START_PATH + System.IO.Path.DirectorySeparatorChar + "nbs.exe";
            Console.WriteLine(ncp.StartInfo.FileName);
            ncp.StartInfo.Arguments = @"account create" + @" " + pw + " -o";

            ncp.StartInfo.CreateNoWindow = true;

            ncp.EnableRaisingEvents = true;
            bool r = ncp.Start();
            ncp.WaitForExit();
        }

        /// <summary>
        /// TODO
        /// </summary>
        private void CreateAccount()
        {
            try
            {
                Channel channel = GrpcBase.NewChannel();
                var accountTask = new AccountTask.AccountTaskClient(channel);

                accountTask.CreateAccount(new CreateAccountRequest { Password = "123456" });
                Console.WriteLine(">>>>>>create success.");
            }catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadChat()
        {
            this.RChatContainer.Children.Clear();

            Console.WriteLine("==============================CID===>" + AppState.Instance.CID);
            this.RChatContainer.Children.Add(new ChatMainView(AppState.Instance.CID));
        }

        private bool CheckedNBSRunning()
        {
            Process[] processes;
            try
            {
                processes = Process.GetProcessesByName("nbs");
                Console.WriteLine("nbs process :" + processes.Length.ToString());
                foreach(Process p in processes)
                {
                    Console.WriteLine(" process Name:" + p.ProcessName);
                }
                return processes.Length > 0; 
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void StartNBSGrpc()
        {
            try
            {
                Process nbsp = new Process();
                nbsp.StartInfo.UseShellExecute = false;
                nbsp.StartInfo.FileName = AppState.Instance.START_PATH + System.IO.Path.DirectorySeparatorChar + "nbs.exe";
                Console.WriteLine("启动exe》》" + nbsp.StartInfo.FileName);
                nbsp.StartInfo.CreateNoWindow = true;
                
                nbsp.EnableRaisingEvents = ConfigManager.Instance.BackProcess();
                bool r = nbsp.Start();
                
                int i = 0;
                while(i<10 && !AppState.Instance.RPC_RUNNING)
                {
                    bool b = CheckedNBSRunning();
                    AppState.Instance.RPC_RUNNING = b;
                    i++;

                    Console.WriteLine(">>>" + i+"==="+b.ToString());
                    Thread.Sleep(1000);
                }

                nbsp.WaitForExit(1000);
                nbsProcessName = nbsp.ProcessName;
                Console.WriteLine("启动》》" + r.ToString()+">>>>"+nbsp.ProcessName);
                AppState.Instance.RPC_RUNNING = true;
            }
            catch (Exception e)
            {
                AppState.Instance.RPC_RUNNING = false;
                //TODO Toolbar
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
            if (this.accountService == null) return;
            
            try
            { 
                string acid = accountService.GetAccountHID();
                if (String.IsNullOrEmpty(acid))
                {
                    //bool cr = this.accountService.CreateAccount("123456");
                    var accountTask = new AccountTask.AccountTaskClient(GrpcBase.Channel);

                    CreateAccountResponse res = accountTask.CreateAccount(new CreateAccountRequest { Password = "123456" });

                    if (String.IsNullOrEmpty(res.Message))
                    {
                        AppState.Instance.CID = this.accountService.GetAccountHID();
                        AppState.Instance.RPC_RUNNING = true;
                    }
                    else
                    {
                        //为创建成功
                        throw new Exception("Create Account Error.");
                    }
                }
                AppState.Instance.CID = acid;
                AppState.Instance.RPC_RUNNING = true;
            }
            catch(Exception e)
            {
                Console.WriteLine("====================>>>Grpc error:" + e.Message);
            }
            finally
            {
                //
            }

        
        }
    }
}
