using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: NBSChat.Model
 * │ 
 * │Comment	:App Manager
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/2/27 10:11:39													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace NBSChat.Model
{
    /// <summary>
    /// 应用全局变量
    /// </summary>
    class AppManager
    {
        public const string ApplicationName = "NBSChatCli";

        private static System.Collections.Hashtable audioHt;

        private static bool _mintoTray = true;//托盘是否最小

        private static string _appbgImg = String.Empty;

        private static char _spiderChar = '@';//字符分割符

        /// <summary>
        /// 窗体背景
        /// </summary>
        public static string AppBgImg
        {
            get { return _appbgImg; }
            set { _appbgImg = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public static char SpiderChar
        {
            get { return _spiderChar; }
            set { _spiderChar = value; }
        }

        public static bool MinToTray
        {
            get { return _mintoTray; }
            set { _mintoTray = value; }
        }
        /// <summary>
        /// 可执行文件启动目录
        /// </summary>
        public static string StartPath { get; set; }

        /// <summary>
        /// 配置文件路径
        /// </summary>
        public static string Config { get; set; }

        public static Hashtable AudioHt
        {
            get
            {
                if(audioHt == null || audioHt.Count == 0)
                {
                    string path = StartPath + "\\Audio\\";
                    if (System.IO.Directory.Exists(path))
                    {
                        string name = "";
                        audioHt = new System.Collections.Hashtable();

                        foreach(string f in System.IO.Directory.GetFileSystemEntries(path))
                        {
                            path = System.IO.Path.GetFullPath(f);
                            name = System.IO.Path.GetFileName(f);
                            name = name.Substring(0, name.LastIndexOf("."));

                            if (!audioHt.ContainsKey(name))
                                audioHt.Add(name, path);
                        }
                    }
                }
                return audioHt;
            }

        }

        public static bool IsMainWinShow { get; set; }

    }

}
