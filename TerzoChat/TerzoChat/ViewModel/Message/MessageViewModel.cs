using FluentValidation;
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
    public class MessageViewModel 
        : ValidObservableObject<MessageViewModel>
    {
        public MessageViewModel() : base(MessageValidator.Singleton.Value, ViewModelState.Valid) { }

        private string _nickname;
        private string _stime;
        private string _avatarName;
        private string content;
        private bool _isSelf;
        private MessageType _msgtype;

        private MessageState _state;

        public MessageState MessageState
        {
            get => _state;
            set => Set(ref _state, value);
        }

        private string _mid;

        public string UID
        {
            get => _mid;
            private set
            {
                _mid = Guid.NewGuid().ToString();
            }
        }

        public string Nickname { get => _nickname; set => Set(ref _nickname, value); }

        public string ShowTime { get => _stime; set => Set(ref _stime, value); }

        public string AvatarName { get => _avatarName; set => Set(ref _avatarName, value); }

        public string Content { get => content; set => Set(ref content, value); }

        public bool IsSelf { get => _isSelf; set => Set(ref _isSelf, value); }

        public MessageType MsgType
        {
            get => _msgtype;
            set => Set(ref _msgtype, value);
        }

    }
}
