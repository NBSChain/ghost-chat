using System;
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
 * │CreatTime	: 2019/3/9 3:23:02													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoApp.ViewModel
{
    public class MessageRecordViewModel : INotifyPropertyChanged
    {
        public MessageRecordViewModel()
        {
            loadMessage();
        }

        private ObservableCollection<MessageInfo> _msgRecordList;

        public ObservableCollection<MessageInfo> MsgRecordList
        {
            get { return _msgRecordList; }
            set
            {
                _msgRecordList = value;
                RaisePropertyChanged("MsgRecordList");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void loadMessage()
        {
            string _prefix = "NBS-";
            GUIDHelper helper = GUIDHelper.getInstance();
            ObservableCollection<MessageInfo> initList = new ObservableCollection<MessageInfo>();
            for (int i = 0; i < 20; i = i + 1)
            {
                MessageInfo m = new MessageInfo();
                //m.HashID = "00000" + i;
                m.HashID = helper.GetGUID().Substring(10);
                m.NickName = _prefix + "" + i;
                m.RealTime = DateTime.Now.AddHours((20 - 20 / (i + 1))).AddMinutes(-25 / (i + 1));
                m.IsSelf = i % 3 == 0 ? false : true;
                if (i % 2 == 0)
                {
                    m.AvatarImg = @"pack://application:,,,/avatars/nbs100.png";
                }
                else
                {
                    m.AvatarImg = @"pack://application:,,,/avatars/fuchen200.png";
                }
                int a = i > 5 ? i : 5;
                StringBuilder sb = new StringBuilder();
                for (int j = 1; j < a; j++)
                {
                    sb.Append(j + "kKsvK" + j);
                }
                m.MsgContent = sb.ToString();

                initList.Add(m);
            }
            if (initList != null)
            {
                MsgRecordList = initList;
            }
        }
    }
}
