using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: ChatCli.Model
 * │ 
 * │Comment	: Application Models
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/3 12:24:29													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace ChatCli.Model
{
    /// <summary>
    /// AppOptionModel
    /// </summary>
    public class AOM
    {
        public const string ApplicationName = "NBSChatClient";

        private static bool _mintoTray = false;//关闭按钮是否直接关闭

        private static string _appbgImg = String.Empty;

        public static bool MinToTray
        {
            get { return _mintoTray; }
            set { _mintoTray = value; }
        }

        public static string AppBgImg {
            get { return _appbgImg; }
            set { _appbgImg = value; }
        }

        /// <summary>
        /// 可执行文件启动目录
        /// </summary>
        public static string StartPath { get; set; }

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static string Config { get; set; }
    }
}
