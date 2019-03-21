using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Controls;
using UrusTools.Config;
using Grpc.Core;
using Grpc;
using System.Threading.Tasks;
using Pb;
using grpc2slib;
using grpc2slib.BBL;
using TerzoChat.BBL;
using TerzoChat.Data;
using TerzoChat.Model;
using System.Threading;


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
        private readonly PubSubServiceClient<MessageViewModel> serviceClient;

        private MessageViewModel SELF;
        private ObservableCollection<MessageViewModel> _collection = new ObservableCollection<MessageViewModel>();
        private readonly object _lockObject = new object();
        public RChatViewModel(IStorage<MessageViewModel> storage) : base()
        {

            Title = "NBS群聊";
            initSelf();
            BindingOperations.EnableCollectionSynchronization(_collection, _lockObject);
            PubSubTask.PubSubTaskClient cli = new PubSubTask.PubSubTaskClient(GrpcBaseHelper.Instance().Channel);
            serviceClient = new PubSubServiceClient<MessageViewModel>(cli,_collection,SELF,true);
            StartRecving();

            this.AssignCommands();

            loadLocalHistory(storage,1);

        }

        private void StartRecving()
        {
            serviceClient.StartRecvListener(NBSTopic.NBSWorld, MessageModelUtils.buildRecvMessage).ConfigureAwait(true);
        }

        private void initSelf()
        {
            this.SELF = new MessageViewModel(true)
            {
                UID = AppState.Instance.CID,
                Nickname = "Me",
                AvatarName = "/avatars/logo.png",
                MsgType = Model.MessageType.text,
                MessageState = MessageState.Failure
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="beforeDays"></param>
        private void loadLocalHistory(IStorage<MessageViewModel> storage,int beforeDays)
        {

        }

        public ObservableCollection<MessageViewModel> MessageRecord
        {   get => _collection;
            set =>Set(ref _collection,value);
        }

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
            serviceClient.SendMessage(
                NBSTopic.NBSWorld, content, MessageModelUtils.buildSendMessage1);
        }
    }


}
