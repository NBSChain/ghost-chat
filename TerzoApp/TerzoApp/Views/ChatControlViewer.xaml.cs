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
using UrusUI;
using TerzoApp.Model;

/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoApp.Views
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/5 22:37:05													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoApp.Views
{
    /// <summary>
    /// ChatControlViewer.xaml 的交互逻辑
    /// </summary>
    public partial class ChatControlViewer : UserControl
    {
        

        public ChatControlViewer()
        {
            InitializeComponent();
        }

        public string NICK
        {
            get
            {
                return String.IsNullOrEmpty(AOM.CURR_NICK) ? "" : AOM.CURR_NICK;
            }

            set { }
        }

        #region 窗体操作
        private void NavBtn_Click(object sender,RoutedEventArgs e)
        {
            this._swicthBtn(sender as FVButton);
        }

        private void _swicthBtn(FVButton btn)
        {
            string arg = (btn.CommandParameter).ToString();
            if (arg.Length == 0) return;
            bool _isSelected = btn.IsSelected;

            FVButton[] fvbs = { this.NavButtonMSG,this.NavButtonContacts,this.NavButtonFiles};
            //当前没选中

            foreach(FVButton fvb in fvbs)
            {
                if(btn.Name == fvb.Name)
                {
                    btn.IsSelected = true;
                }
                else
                {
                    fvb.IsSelected = false;
                }
            }

        }

        private void InitNick()
        {
            
        }
        #endregion
    }
}
