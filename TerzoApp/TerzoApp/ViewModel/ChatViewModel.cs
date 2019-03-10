using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using TerzoApp.Model;
using System.Windows.Media.Imaging;
using UrusLib;


/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoApp.ViewModel
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/8 20:41:18													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoApp.ViewModel
{
    public class ChatViewModel : INotifyPropertyChanged
    {
       
        #region 构造方法
        public ChatViewModel()
        {
            NickName = AOM.CURR_NICK;
            loadContacts();
            //loadMessage();
        }
        #endregion

        public string NickName
        {
            get { return _nick; }
            set
            {
                _nick = value;
                RaisePropertyChanged("NickName");
            }
        }

        #region Fields
        private string _nick;
        private string _serachText;
        private ObservableCollection<ContactModel> _resultList;
       // private ObservableCollection<MessageInfo> _msgRecordList;

        #endregion

        #region UIPage properties

        public ObservableCollection<ContactModel> ContactList
        {
            get;
            private set;
        }

        //public ObservableCollection<MessageInfo> MsgRecordList
        //{
        //    get { return _msgRecordList; }
        //    set
        //    {
        //        _msgRecordList = value;
        //        RaisePropertyChanged("MsgRecordList");
        //    }
        //}
        /// <summary>
        /// 查询条件,关键字
        /// </summary>
        public string SearchText
        {
            get { return _serachText; }
            set
            {
                _serachText = value;
                RaisePropertyChanged("SearchText");
            }
        }

        public ObservableCollection<ContactModel> ResultList
        {
            get { return _resultList; }
            set
            {
                _resultList = value;
                RaisePropertyChanged("ResultList");
            }
        }

        #endregion

        public ICommand QueryCommand
        {
            get { return new QueryCommand(Searching, CanSearching); }
        }

        public void Searching()
        {
            ObservableCollection<ContactModel> contactModels = null;
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                ResultList = ContactList;
            }
            else
            {
                contactModels = new ObservableCollection<ContactModel>();
                foreach(ContactModel cm in ContactList)
                {
                    if (cm.Message.Contains(SearchText))
                    {
                        contactModels.Add(cm);
                    }
                }
                if(contactModels != null)
                {
                    ResultList = contactModels;
                }

            }
        }

        public bool CanSearching()
        {
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #region Biz methods
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void loadContacts()
        {
            string _prefix = "NBS-";
            GUIDHelper helper = GUIDHelper.getInstance();
            ObservableCollection<ContactModel> initList = new ObservableCollection<ContactModel>();

            for(int i =0; i < 20;i=i+1)
            {
                ContactModel m = new ContactModel();
                //m.HashID = "00000" + i;
                m.HashID = helper.GetGUID().Substring(10);
                m.Message = _prefix + "" + i;
                m.AvatarHashID = "NBS-xxx-" + i;
                if (i % 2 == 0)
                {
                    m.AvatarName = new BitmapImage(new Uri("pack://application:,,,/avatars/nbs100.png"));
                }
                else
                {
                    m.AvatarName = new BitmapImage(new Uri("pack://application:,,,/avatars/fuchen200.png"));
                }
                initList.Add(m);
            }
            if (initList != null)
            {
                ResultList = initList;
            }

        }


        //private void loadMessage()
        //{
        //    string _prefix = "NBS-";
        //    GUIDHelper helper = GUIDHelper.getInstance();
        //    ObservableCollection<MessageInfo> initList = new ObservableCollection<MessageInfo>();
        //    for (int i = 0; i < 20; i = i + 1)
        //    {
        //        MessageInfo m = new MessageInfo();
        //        //m.HashID = "00000" + i;
        //        m.HashID = helper.GetGUID().Substring(10);
        //        m.NickName = _prefix + "" + i;
        //        m.RealTime = DateTime.Now.AddHours((20 - 20 / (i + 1))).AddMinutes(-25 / (i + 1));
        //        m.IsSelf = i % 3 == 0 ? false : true;
        //        if (i % 2 == 0)
        //        {
        //            m.AvatarImg = @"pack://application:,,,/avatars/nbs100.png";
        //        }
        //        else
        //        {
        //            m.AvatarImg = @"pack://application:,,,/avatars/fuchen200.png";
        //        }
        //        int a = i > 5 ? i : 5;
        //        StringBuilder sb = new StringBuilder();
        //        for (int j = 1; j < a; j++)
        //        {
        //            sb.Append(j + "kKsvK" + j);
        //        }
        //        m.MsgContent = sb.ToString();

        //        initList.Add(m);
        //    }
        //    if (initList != null)
        //    {
        //        MsgRecordList = initList;
        //    }
        //}
        #endregion
    }
}
