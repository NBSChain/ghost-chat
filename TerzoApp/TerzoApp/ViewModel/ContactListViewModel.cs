using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using TerzoApp.Model;
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
    public class ContactListViewModel : INotifyPropertyChanged
    {
        #region 构造方法
        public ContactListViewModel()
        {
            NickName = AOM.CURR_NICK;
            loadContacts();
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
        #endregion

        #region UIPage properties

        public ObservableCollection<ContactModel> ContactList
        {
            get;
            private set;
        }
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
                m.HashID = helper.GetGUID();
                m.Message = _prefix + "" + i;
                m.AvatarHashID = "NBS-xxx-" + i;
                if (i % 2 == 0)
                {
                    m.AvatarName = "nbs100.png";
                }
                else
                {
                    m.AvatarName = "fuchen200.png";
                }
                initList.Add(m);
            }
            if (initList != null)
            {
                ResultList = initList;
            }

        }
        #endregion
    }
}
