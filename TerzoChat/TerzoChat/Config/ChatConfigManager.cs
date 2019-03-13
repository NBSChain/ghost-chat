using System;
using System.Configuration;
using UrusTools.Config;


/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoChat.Config
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/13 13:53:36													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoChat.Config
{
    public class ChatConfigManager
    {
        const string CLR_APP_CONF = "cli.conf";
        private ChatConfigManager()
        {

        }

        public static ChatConfigManager Instance
        {
            get
            {
                return Nested.Instace;
            }
        }

        class Nested
        {
            static Nested()
            {

            }
            internal static readonly ChatConfigManager Instace = new ChatConfigManager();
        }

        public T GetConfigSection<T>(string section) 
            where T : System.Configuration.ConfigurationSection
        {
            if (section == null || section.Length == 0) return null;
            object o = ConfigurationManager.GetSection(section);
            return o==null? null: o as T;
        } 
    }
}
