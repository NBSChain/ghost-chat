using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TerzoChat.Data;
using TerzoChat.Model;
using System;

/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoChat.Model.Message
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/10 22:58:01													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoChat.ViewModel
{
    public class MessageViewModel : ViewModelBase
    {
        public MessageViewModel(bool self):base()
        {
            IsSelf = self;
            MessageTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public MessageViewModel() : base()
        {
            IsSelf = false;
            MessageTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private string _nickname;
        private string _avatarName;

        private MessageState _state;

        public string MessageTime { get; set; }

        public MessageState MessageState
        {
            get => _state;
            set => Set(ref _state, value);
        }
        public string UID { get; set; }

        public string Nickname { get => _nickname; set => Set(ref _nickname, value); }


        public string AvatarName { get => _avatarName; set => Set(ref _avatarName, value); }

        public string Content { get; set; }

        public bool IsSelf { get; set; }

        public MessageType MsgType
        {
            get;
            set;
        }

    }
}
