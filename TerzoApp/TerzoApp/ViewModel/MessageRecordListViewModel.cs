using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using TerzoApp.Model;


/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoApp.ViewModel
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/8 19:46:02													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoApp.ViewModel
{
    public class MessageRecordListViewModel : INotifyPropertyChanged
    {
        #region Fields
        private string _searchText;
        private ObservableCollection<ContactModel> _resultList;
        #endregion

        #region Properties
        public ObservableCollection<ContactModel> ContactList
        {
            get;
            private set;
        }
      
        /// <summary>
        /// 查询条件
        /// </summary>
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;

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

        public ICommand QueryCommand
        {
            get { return new QueryCommand(Searching, CanSearching); }
        }
        #endregion

        


        public event PropertyChangedEventHandler PropertyChanged;

        public bool CanSearching()
        {
            return true;
        }

        public void Searching()
        {
            ObservableCollection<ContactModel> contacts = null;
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                ResultList = ContactList;
            }
            else
            {
                contacts = new ObservableCollection<ContactModel>();
                foreach(ContactModel cm in ContactList)
                {
                    if (cm.Message.Contains(SearchText))
                    {
                        contacts.Add(cm);
                    }
                   
                }
                if (contacts != null)
                {
                    ResultList = contacts;
                }
            }
        }
        #region Methods
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
