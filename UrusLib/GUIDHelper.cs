using System;
using System.Collections.Generic;
using System.Text;


/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: UrusLib
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/8 21:24:16													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace UrusLib
{
    public class GUIDHelper
    {
        private static GUIDHelper instance;

        private static readonly object locker = new object();

        private GUIDHelper() { }

        public static GUIDHelper getInstance()
        {
            if(instance == null)
            {
                lock (locker)
                {
                    if(instance == null)
                    {
                        instance = new GUIDHelper();
                    }
                }
            }

            return instance;
        }

        public string GetGUID()
        {
            System.Guid guid = Guid.NewGuid();
            string id = guid.ToString();
            return id;
        }
    }
}
