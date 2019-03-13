using System;
using System.IO;
using System.Windows;

using System.Diagnostics;
using TerzoChat.View;
using Demo;
using Google.Protobuf;
using Grpc;
using Grpc.Core;
using grpc2slib.BBL;
using Pb;
using UrusTools.Config;
using TerzoChat.Config;


namespace TerzoChat
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            bool b = CheckedNBSRunning();
           
            if (!b)
            {
                //MessageBox.Show("NBS 未启动");
                string startPath = Directory.GetCurrentDirectory();
                Console.WriteLine("====================>>>"+startPath);
                try
                {
                    Process nbsp = new Process();
                    nbsp.StartInfo.UseShellExecute = false;
                    nbsp.StartInfo.FileName = startPath + System.IO.Path.DirectorySeparatorChar + "nbs.exe";
                    nbsp.StartInfo.CreateNoWindow = true;

                    bool r = nbsp.Start();
                    
                    Console.WriteLine("启动》》" + r.ToString());
                   

                }catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }
            else
            {
                Console.WriteLine("==================》》NBS 已启动." );
            }
            InitializeComponent();
            this.LoadChat();
            string nick = this.getCurrent("lanbery");
            this.NickLabel.Text = nick; 
        }

        private void LoadChat()
        {
            this.RChatContainer.Children.Clear();
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

        private string getCurrent(string login)
        {
          
            BaseRPCService service = new BaseRPCService();
          
            try
            {
                string version = service.GetVersion();
                Console.WriteLine(" NBS version>>> " + version + ".");
                return version;
            }
            catch(Exception ex)
            {
                return login;
            }
        }
    }
}
