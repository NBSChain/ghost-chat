﻿using System;
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

/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoChat.View
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/10 22:32:08													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoChat.View
{
    /// <summary>
    /// ChatMainView.xaml 的交互逻辑
    /// </summary>
    public partial class ChatMainView : UserControl
    {

        public ChatMainView(string acid)
        {
            InitializeComponent(); 
        }

        private void SizeChangedFuc(object sender, SizeChangedEventArgs e)
        {
            double d = MsgScroll.ActualHeight;
            MsgScroll.ScrollToVerticalOffset(d);
        }
    }
}
