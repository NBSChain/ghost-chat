using System;
using System.Globalization;
using System.Windows.Data;

/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoApp.Converter
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/8 16:47:10													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoApp.Converter
{
    public class Date2FormatConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return "";
            DateTime now;
            if(DateTime.TryParse(value.ToString(),out now)){
                return now.ToString("yyyy-MM-dd HH:mm") + "";
            }
            return "";
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
