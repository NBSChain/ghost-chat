using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
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
        
        private BaseRPCService service;
        public RChatViewModel(IStorage<MessageViewModel> storage)
        {
            var msgs = storage.GetList();
            this.AssignCommands();
            Title = "NBS群聊";
            MessageRecord = new ObservableCollection<MessageViewModel>(msgs);

            //Listening the World
            //ListenNBSWorld();
            Thread thread = new Thread(delegate ()
            {
                Channel c = new Channel("127.0.0.1", 10001, ChannelCredentials.Insecure);

                GetMsg(c, MessageRecord).Wait();
            });
            thread.IsBackground = true;
            thread.Start();
           
            //c.ShutdownAsync().Wait();
            service = new BaseRPCService();
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

        

        async static Task GetMsg(Channel channel,ObservableCollection<MessageViewModel> collection)
        {
            var client = new PubSubTask.PubSubTaskClient(channel);
            SubscribeRequest request = new SubscribeRequest { Topics = "NBSWorld" };
            string ts = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            Console.WriteLine("===>>>>>>>>>>>"+ts);
            try
            {
                using(var call = client.Subscribe(request))
                {
                    var respStream = call.ResponseStream;
                    while(await respStream.MoveNext())
                    {
                        var res = respStream.Current;
                        
                        Console.WriteLine("===>>>>>>>>>>>" + res.From);
                    }
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private async void ListenNBSWorld()
        {
            
            Thread listenThread = new Thread(delegate ()
            {
                SubscribeMessageService messageService = new SubscribeMessageService();
                try
                {
                    int a = 0;
                    while (a < 10)
                    {
                        a++;
                        Thread.Sleep(1000);
                        Console.WriteLine("Thread ..... " + a.ToString());
                    }
                    messageService.TaskReciver(MessageRecord).Wait();
                }
                catch(Exception r)
                {
                    Console.WriteLine(r.Message);
                }
                
           
                
            });
            listenThread.IsBackground = true;
            listenThread.Start();
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
