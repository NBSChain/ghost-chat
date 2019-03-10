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
 * │CreatTime	: 2019/3/9 2:05:35													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoApp.Converter
{
    class Bool2VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool v = System.Convert.ToBoolean(value);
            Visibility result = v ? Visibility.Visible : Visibility.Collapsed;
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility v = (Visibility)value;
            return v == Visibility.Visible ? true : false;
        }
    }
}
