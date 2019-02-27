using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: IOL.Utility
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/2/27 10:51:28													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace IOL.Utility
{
    class XamlHelper
    {
        private static XamlHelper _instance;
        private static object _lock = new object();//使用static object作为互斥资源
        private static readonly object _obj = new object();

        #region 单例
        private XamlHelper() { }
        /// <summary>
        /// 单例
        /// </summary>
        public static XamlHelper Instance
        {
            get
            {
                if(_instance == null)
                {
                    lock (_obj)
                    {
                        _instance = new XamlHelper();
                    }
                }
                return _instance;
            }
        }
        #endregion

        #region 设置空间属性
        public void setcontroltext(System.Windows.Controls.Control control, string text)
        {

        }
        #endregion
    }
}
