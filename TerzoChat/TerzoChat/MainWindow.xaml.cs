using System;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using TerzoChat.View;

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
        }

        private void LoadChat()
        {
            this.RChatContainer.Children.Clear();
            this.RChatContainer.Children.Add(new ChatMainView());
        }

        private bool CheckedNBSRunning()
        {
            bool b = false;
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
    }
}
