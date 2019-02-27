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
            System.Windows.Application.Current.Shutdown();
        }

        /// <summary>
        /// 最小化窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_Min_Click(object sender,RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
            this.Visibility = Visibility.Hidden;
            
        }
        #endregion

        private void TbIcon_TrayLeftMouseDown(object sender, RoutedEventArgs e)
        {

        }
    }
}
