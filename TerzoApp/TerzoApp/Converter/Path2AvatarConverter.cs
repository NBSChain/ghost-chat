using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;


/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoApp.Converter
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/8 23:19:07													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoApp.Converter
{
    class Path2AvatarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            string url = (string)value;
            //if (url.Length == 0)
            //{
            //    url = "pack://application:,,,/avatars/nbs100.png";
            //}

            //if (url.IndexOfAny(new char[] { '/', ',' }) > 0 || url.Contains("http"))
            //{

            //}
            //else
            //{
            //    url = "pack://application:,,,/avatars/" + url;
            //}
            BitmapImage img = new BitmapImage(new Uri(url));
            return img;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
