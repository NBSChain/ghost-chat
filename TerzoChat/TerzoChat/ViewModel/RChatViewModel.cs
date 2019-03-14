using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System;
using System.Windows.Input;
using System.Windows.Controls;
using grpc2slib;
using grpc2slib.BBL;
using TerzoChat.Data;
using TerzoChat.Model;



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
        private BaseRPCService service = new BaseRPCService();
        public RChatViewModel(IStorage<MessageViewModel> storage)
        {
            var msgs = storage.GetList();
            this.AssignCommands();
            Title = "NBS群聊";
            MessageRecord = new ObservableCollection<MessageViewModel>(msgs);
        }

        public ObservableCollection<MessageViewModel> MessageRecord { get; set; }

        public string Title { get; set; }

     
        public string SendText { get; set; }

        public ICommand Sending { get; private set; }
        public ICommand SendTextEnter { get; private set; }

        private void AssignCommands()
        {
            Sending = new RelayCommand<TextBox>(a =>
            {
                if (a != null && a.Text.Length > 0)
                {
                    string c = a.Text;
                    Console.WriteLine(c);
                    this.SendContent(c);
                    a.Text = "";
                }
                else
                {
                    return;
                }
            });

            SendTextEnter = new RelayCommand<TextBox>(a => {
                if (a != null && a.Text.Length>0) {
                    string c = a.Text;
                    Console.WriteLine(c);
                    this.SendContent(c);
                    a.Text = "";
                }
                else
                {
                    return;
                }
            });
           
        }



        private void SendContent(string content)
        {
            bool flag = service.SendText2World(content);
            MessageViewModel m = new MessageViewModel
            {
                PID = AppState.Instance.CID,
                Nickname = "Me",
                AvatarName = "/avatars/nbsstar.png",
                IsSelf = true,
                ShowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                MsgType = Model.MessageType.text,
                Content = content,
                MessageState = flag ? MessageState.Normal : MessageState.Failure
            };
            MessageRecord.Add(m);
        }
    }


}
