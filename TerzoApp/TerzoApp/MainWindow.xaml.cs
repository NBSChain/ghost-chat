
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Hardcodet.Wpf.TaskbarNotification;
using TerzoApp.Views;
using TerzoApp.Model;

namespace TerzoApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static TaskbarIcon TaskIcon { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            this.InitNBSSvrInfo();
            TaskIcon = new TaskbarIcon();
            TaskIcon.Icon = Properties.Resources.logo;
            this._loadMainContainer();

            TaskIcon.ShowBalloonTip("提示", "客户端启动成功", BalloonIcon.Info);
        }

        private void InitNBSSvrInfo()
        {
            AOM.CURR_NICK = "lanbery";
        }


        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            
        }
        #region 内部方法
        private void _loadMainContainer()
        {
            this.SetContentControl(new ChatControlViewer());
        }

        private void Minimized()
        {
            this.WindowState = System.Windows.WindowState.Minimized;
            this.Visibility = Visibility.Hidden;

            //
            //this.miShowWindow.Header = "显示窗口";
            //this.miShowWindow.Icon = "&#xf2d2;";
        }
        private void ShowWindow()
        {
            this.Show();
            if (this.WindowState == System.Windows.WindowState.Minimized)
            {
                //this.WindowState = _lastWinState;
                //this.miShowWindow.Header = "隐藏窗口";
                //this.miShowWindow.Icon = "&#xf17a;";
            }
            this.Activate();
        }
        #endregion
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="control"></param>
        public void SetContentControl(object control)
        {
            if (control is Control)
            {
                this.mainContainer.Children.Clear();
                this.mainContainer.Children.Add(control as Control);
            }
        }



        #region 窗口操作
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftDownDrag(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }

        }
        private void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            if (TerzoApp.Model.AOM.MinToTray)
            {
                this.Minimized();
                return;
            }
            System.Windows.Application.Current.Shutdown();
        }

        private void Btn_Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.Minimized();
        }
        #endregion
    }
}
