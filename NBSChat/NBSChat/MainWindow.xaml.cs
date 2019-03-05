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
using NBSChat.Model;
using IOL.Utility;
using System.Drawing;
using System.ComponentModel;

namespace NBSChat
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public static TaskbarIcon TaskIcon { get; set; }

        private WindowState _lastWinState = WindowState.Normal;//记录上一次窗口状态

        private bool _reallyExit = false;


        public MainWindow()
        {
            InitializeComponent();

            TaskIcon = new TaskbarIcon();

            XamlHelper.Instance.SetBackground(this.mainBorder, AppManager.AppBgImg);

            //TaskIcon.ShowBalloonTip("提示" ,"客户端启动成功." , BalloonIcon.Info);

        }

        /// <summary>
        /// 最小化窗口
        /// </summary>
        private void Minimized()
        {
            this.WindowState = System.Windows.WindowState.Minimized;
            this.Visibility = Visibility.Hidden;
            //修改托盘图标和tip
            this.miShowWindow.Header = "显示窗口";
            
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
            if (this.tbIcon != null) this.tbIcon.Dispose();
        }

        #region 窗体相关操作
        /// <summary>
        /// 拖动窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Close_Click(object sender,RoutedEventArgs e)
        {
            if (NBSChat.Model.AppManager.MinToTray)
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
        /// 最小化窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Min_Click(object sender,RoutedEventArgs e)
        {
            this.Minimized(); 
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
        {
            this.Show();
            this.miShowWindow.Header = "隐藏窗口";
            if(this.WindowState == System.Windows.WindowState.Minimized)
            {
                this.WindowState = _lastWinState;
            }
            this.Activate();
        }

        private void TrayMeunItem_ShowOrHidden_Click(object sender, RoutedEventArgs e)
        {
            if(this.WindowState == System.Windows.WindowState.Minimized)
            {
                this.Show();
                this.miShowWindow.Header = "隐藏窗口";
                this.WindowState = _lastWinState;
                this.Activate();
            }
            else
            {
                this.Minimized();
            }
        }
    }
}
