using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;


/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoChat.Model
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/13 17:47:35													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoChat
{
    public class AppState
    {
        private static AppState appState;
        public bool RPC_RUNNING { get; set; }
        private string _cid = "";

        public static AppState Instance
        {
            get
            {
                if (appState == null) appState = Nested.Instace;
                return appState;
            }
        }

        public string START_PATH { get;}


        public string CID
        {
            get { return _cid; }
            set { _cid = String.IsNullOrEmpty(value) ? "" : value; }
        }
        /// <summary>
        /// nbs 服务版本
        /// </summary>
        public string GrpcServerVersion { get; set; }

        private AppState()
        {
            START_PATH = Directory.GetCurrentDirectory();
            RPC_RUNNING = false;
        }

        class Nested
        {
            Nested() { }
            internal static readonly AppState Instace = new AppState();
        }
    }
}
