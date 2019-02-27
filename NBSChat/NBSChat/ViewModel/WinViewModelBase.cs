using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: NBSChat.ViewModel
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	    : lanbery
 * │CreatTime	: 2019/2/26 14:17:06													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace NBSChat.ViewModel
{
    class WinViewModelBase : ViewModelBase
    {
        public WinViewModelBase(string windowName) : base()
        {
            this.WindowName = windowName;
        }

        #region 属性
        private bool _isShow;
        private string _title;

        /// <summary>
        /// 窗口注册的名称
        /// </summary>
        public string WindowName { get; private set; }

        public Window Window { get; private set; }

        public bool IsShow
        {
            get { return _isShow; }
            set
            {
                if (_isShow != value)
                {
                    _isShow = value;
                    this.RaisePropertyChanged("IsShow");
                }
            }
        }

        //<summary>
        //窗体的窗体
        //</summary>
        public virtual string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    this.RaisePropertyChanged("Title");
                }
            }
        }

        #endregion

        #region 命令
        private RelayCommand _showCommand;
        private RelayCommand _closeCommand;
        //private RelayCommand _minimizeCommand;
        //private RelayCommand _maximizeCommand;
        //private RelayCommand _normalCommand;
        //private RelayCommand _showDialogCommand;

        ///<summary>打开命令</summary>
        public RelayCommand ShowCommand
        {
            get
            {
                if (_showCommand == null)
                {
                    _showCommand = new RelayCommand(this.Show);
                }
                return _showCommand;
            }
        }
        /// <summary>
        /// 关闭命令
        /// </summary>
        public RelayCommand CloseCommand
        {
            get
            {
                if(_closeCommand == null)
                {
                    _closeCommand = new RelayCommand(this.Close);
                }
                return _closeCommand;
            }
        }
        #endregion

        #region 方法

        ///<summary>显示窗口</summary>
        public virtual void Show()
        {
            if (this.Window != null)
            {
                this.Window.Show();
            } else
            {
                
            }
            this.IsShow = true;
        }

        ///<summary>
        ///关闭窗口
        ///</summary>
        public virtual void Close()
        {
            if(this.Window != null)
            {
                this.Window.Close();
            }
        }

        /// <summary>
        /// 打开窗口模式
        /// </summary>
        public virtual void ShowDialog()
        {
            if(this.Window != null)
            {
                this.Window.ShowDialog();
            }
            else
            {

            }
            this.IsShow = true;
        }

        /// <summary>
        /// 使窗体最小化
        /// </summary>
        public virtual void Minimize()
        {
            this.Window.WindowState = WindowState.Minimized;
        }

        /// <summary>
        /// 使窗体最大化
        /// </summary>
        public virtual void Maximize()
        {
            this.Window.WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// 使窗体正常
        /// </summary>
        public virtual void Normal()
        {
            this.Window.WindowState = WindowState.Normal;
        }
        #endregion
    }



}