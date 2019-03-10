using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System;
using System.Windows.Input;

using TerzoChat.Data;


/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoChat.ViewModel
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/10 22:53:08													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoChat.ViewModel
{
    public class RChatViewModel : ViewModelBase
    {
        public RChatViewModel(IStorage<MessageViewModel> storage)
        {
            var msgs = storage.GetList();
            this.AssignCommands();
            Title = "今天聊个天";
            MessageRecord = new ObservableCollection<MessageViewModel>(msgs);
        }

        public ObservableCollection<MessageViewModel> MessageRecord { get; set; }

        public string Title { get; set; }

     
        public string SendText { get; set; }

        public ICommand Sending { get; private set; }

        private void AssignCommands()
        {
            Sending = new RelayCommand<string>(a =>
            {
                if (a == null || String.IsNullOrEmpty(a as string) || String.IsNullOrWhiteSpace(a as string)) return;
                MessageViewModel m = new MessageViewModel
                {
                    Nickname = "lanbery",
                    AvatarName = "/avatars/fuchen200.png",
                    IsSelf = true,
                    MessageState = Model.MessageState.Normal,
                    ShowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    MsgType = Model.MessageType.text,
                    Content = (a as string)
                };

                MessageRecord.Add(m);
                SendText = string.Empty;
            });
        }
    }
}
