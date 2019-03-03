using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;


/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: ChatCli.Utility
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/3 14:16:03													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace ChatCli.Utility
{
    class XamlHelper
    {
        private static XamlHelper _instance;
        private static object _lock = new object();
        private static readonly object _obj = new object();

        #region 单例
        private XamlHelper() { }

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

        #region 设置控件属性
        public void SetControlText(System.Windows.Controls.Control control,string text)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if(control is System.Windows.Controls.Label)
                {
                    Label lbl = (Label)control;
                    lbl.Content = text;
                }
                else if(control is System.Windows.Controls.TextBox)
                {
                    TextBox txt = (TextBox)control;
                    txt.Text = text;
                }
            }));
        }

        public void SetBackground(System.Windows.Controls.Border border,string imgPath)
        {
            if (String.IsNullOrEmpty(imgPath) || !File.Exists(imgPath)) return;

            try
            {
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(imgPath));
                brush.Stretch = Stretch.Fill;
                border.Background = brush;
            }
            catch (Exception) { }
        }

        #endregion

        #region 加载Xaml
        public void LoadXamlByFile(String path,System.Windows.Controls.StackPanel control)
        {
            try
            {
                XmlTextReader reader = new XmlTextReader(path);
                UIElement obj = XamlReader.Load(reader) as UIElement;
                control.Children.Add((UIElement)obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        public void CboAdd(ComboBox cbo,Dictionary<string,string> dict)
        {
            if (dict == null || dict.Count == 0) return;
            cbo.DisplayMemberPath = "Name";
            cbo.SelectedValuePath = "Value";

            MyItem mi = new MyItem();
            cbo.Items.Clear();

            foreach(KeyValuePair<string,string> kvp in dict)
            {
                mi = new MyItem();
                mi.Name = kvp.Key;
                mi.Value = kvp.Value;
                cbo.Items.Add(mi);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private struct MyItem
        {
            public string Name { get; set; }
            public string Value { set; get; }
        }
    }
}
