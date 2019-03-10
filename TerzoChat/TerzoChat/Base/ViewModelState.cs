using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoChat.Base
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/10 17:45:37													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoChat.Data
{
    public enum ViewModelState
    {
        /// <summary>
        /// 
        /// </summary>
        Original = 1,
        /// <summary>
        /// 
        /// </summary>
        Updated = 2,
        /// <summary>
        /// 
        /// </summary>
        New = 4,
        /// <summary>
        /// data will be deleted after next commit to the DAL
        /// </summary>
        Delete = 8,
        /// <summary>
        /// marks data sa valid/invalid, save button IsEnabled property can be bound to this property
        /// </summary>
        Valid = 16,
    }
}
