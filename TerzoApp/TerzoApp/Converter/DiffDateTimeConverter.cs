using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;


/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoApp.Converter
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/9 1:22:02													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoApp.Converter
{
    class DiffDateTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "";
            DateTime now;
            try
            {
                if (DateTime.TryParse(value.ToString(), out now))
                {
                    return transDiffTime(now);
                }
            }
            catch
            {
            }
            return "";

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private String transDiffTime(DateTime revTime)
        {
            TimeSpan ts = DateTime.Now - revTime;
            if(ts.Days>0||ts.Hours>0 || ts.Minutes > 1)
            {
                return revTime.ToString("yyyy-MM-dd HH:mm") + "";
            }
            else
            {
                return "刚刚";
            }

            
        }
    }
}
