using System;
using System.Windows;
using System.Windows.Data;
using System.Globalization;



/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: NBSChat.Converter
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/2/27 9:43:42													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace NBSChat.Converter
{
    class BooleanToVisibilityConverter : System.Windows.Data.IValueConverter
    {
        public object Convert(object value,Type targetType,object parameter,CultureInfo culture)
        {
            bool v = System.Convert.ToBoolean(value);
            Visibility result = v ? Visibility.Visible : Visibility.Collapsed;
            return result;
        }

        public object ConvertBack(object value,Type targetType,object parameter,CultureInfo culture)
        {
            Visibility v = (Visibility)value;
            return v == Visibility.Visible ? true : false;
        }
    }
}
