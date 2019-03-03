using System;
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
using Hardcodet.Wpf.TaskbarNotification;
using ChatCli.Utility;
using ChatCli.Model;
using System.ComponentModel;

namespace ChatCli
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public static TaskbarIcon TaskIcon { get; set; }
        private WindowState _lastWinState = WindowState.Normal;
        private bool _reallyExit = false;

        private Module.ChatCtrlModule _ccModule = new Module.ChatCtrlModule();
        public MainWindow()
        {
            InitializeComponent();
            TaskIcon = new TaskbarIcon();
            TaskIcon.Icon = Properties.Resources.logo128;
            
            XamlHelper.Instance.SetBackground(this.mainBorder, AOM.AppBgImg);

            if(this.brMain.Child != this._ccModule)
            {
                this.brMain.Child = this._ccModule;
            }
            TaskIcon.ShowBalloonTip("提示", "客户端启动成功", BalloonIcon.Info);
        }

        #region 私有函数
        private void Minimized()
        {
            this.WindowState = System.Windows.WindowState.Minimized;
            this.Visibility = Visibility.Hidden;

            //
            this.miShowWindow.Header = "显示窗口";
            //this.miShowWindow.Icon = "&#xf2d2;";
        }

        private void ShowWindow()
        {
            this.Show();
            if (this.WindowState == System.Windows.WindowState.Minimized)
            {
                this.WindowState = _lastWinState;
                this.miShowWindow.Header = "隐藏窗口";
                //this.miShowWindow.Icon = "&#xf17a;";
            }          
            this.Activate();           
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!_reallyExit)
            {
                e.Cancel = true;
                _lastWinState = this.WindowState;
                this.Hide();
            }
            if (this.tbNotifyIcon != null) this.tbNotifyIcon.Dispose();
        }
        #endregion



        #region 窗体操作
        private void Window_MouseLeftDownDrag(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }

        }

        private void Btn_Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.Minimized();
        }

        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            if (ChatCli.Model.AOM.MinToTray)
            {
                this.Minimized();
                return;
            }
            System.Windows.Application.Current.Shutdown();
        }

        private void Btn_Exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbIcon_TrayLeftMouseDown(object sender,RoutedEventArgs e)
        {
            this.ShowWindow();
        }

        private void TrayMeunItem_ShowOrHidden_Click(object sender, RoutedEventArgs e)
        {
            if(this.WindowState == System.Windows.WindowState.Minimized)
            {
                this.ShowWindow();
            }else
            {
                this.Minimized();
            }
        }
        #endregion
    }

}
