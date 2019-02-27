using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Threading.Tasks;
using System.Xml;



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
        public void SetControlText(System.Windows.Controls.Control control, string text)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                if(control is System.Windows.Controls.Label)
                {
                    System.Windows.Controls.Label lbl = (System.Windows.Controls.Label)control;
                    lbl.Content = text;
                }else if(control is System.Windows.Controls.TextBox)
                {
                    System.Windows.Controls.TextBox txt = (System.Windows.Controls.TextBox)control;
                    txt.Text = text;
                }
            }));
        }

        /// <summary>
        /// 设置背景图
        /// </summary>
        /// <param name="border"></param>
        /// <param name="imgPath"></param>
        public void SetBackground(System.Windows.Controls.Border border,string imgPath)
        {
            if (String.IsNullOrEmpty(imgPath) || !File.Exists(imgPath))
            {
                return;
            }

            try
            {
                ImageBrush brush = new ImageBrush();
                brush.ImageSource = new BitmapImage(new Uri(imgPath));
                brush.Stretch = Stretch.Fill;
                border.Background = brush;
            }
            catch (Exception)
            {

            }
        }

        #endregion

        #region 加载XAML
        /// <summary>
        /// 从外部文件加载
        /// </summary>
        /// <param name="path">绝对路径</param>
        /// <param name="stack"></param>
        public void LoadXamlByFile(string path,System.Windows.Controls.StackPanel stack)
        {
            try
            {
                XmlTextReader reader = new XmlTextReader(path);
                UIElement obj = System.Windows.Markup.XamlReader.Load(reader) as UIElement;
      
                stack.Children.Add(obj);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region ComBox 项
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cbo"></param>
        /// <param name="dic"></param>
        public void  CBoxAdd(ComboBox cbo,Dictionary<string,string> dic)
        {
            if (dic == null || dic.Count == 0) return;

            cbo.DisplayMemberPath = "Name";
            cbo.SelectedValuePath = "Value";
           
            cbo.Items.Clear();

            MyItem mi;
            foreach (KeyValuePair<string,string> kvp in dic)
            {
                mi = new MyItem();
                mi.Name = kvp.Key;
                mi.Value = kvp.Value;
                cbo.Items.Add(mi);
            }
        }
        #endregion

        private struct MyItem
        {
            public string Name { get; set; }

            public string Value { get; set; }
        }
    }
}
