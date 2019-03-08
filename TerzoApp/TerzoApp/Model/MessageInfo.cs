using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoApp.Models
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/8 16:40:23													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoApp.Model
{
    public class MessageInfo
    {
        /// <summary>
        /// 消息的唯一标示
        /// </summary>
        public string UID
        {
            get;set;
        }

        public string HashID { get; set; }

        public string ShowTime
        {
            get;set;
        }

        public DateTime? RealTime { get; set; }

        public string NickName { get; set; }

        public string MsgContent { get; set; }

        public string AvatarImg { get; set; }

        public bool IsSelf
        {
            get;set;
        }
    }
}
