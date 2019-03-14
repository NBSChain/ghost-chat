
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Grpc.Core;
using grpc2slib;
using grpc2slib.BBL;

using Pb;
using TerzoChat.Model;
using TerzoChat.ViewModel;
using UrusTools.Config;



/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: grpc2slib.BBL
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/14 15:51:12													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoChat.BBL
{
    public class SubscribeMessageService
    {
        private GrpcBaseHelper baseHelper;
        private int Port { get; set; }
        private Channel channel;

        private PubSubTask.PubSubTaskClient client;
        
        public SubscribeMessageService()
        {
            baseHelper = GrpcBaseHelper.Instance();
        }

        public async Task TaskReciver(ObservableCollection<MessageViewModel> c)
        {
            if (c == null) c = new ObservableCollection<MessageViewModel>();
            try
            {
                channel = baseHelper.NewChannel();
                client = new PubSubTask.PubSubTaskClient(channel);
                using(var call = client.Subscribe(
                    new SubscribeRequest {
                        Topics = NBSTopic.NBSWorld.ToString()
                    }))
                {

                    var responseStream = call.ResponseStream;
                    while (await responseStream.MoveNext<SubscribeResponse>())
                    {
                        SubscribeResponse resp = responseStream.Current;
                        Console.WriteLine("--------------------------------->");
                        string ts = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        Console.WriteLine("Time:" + ts);
                        Console.WriteLine("Content:" + resp.MsgData.ToString());
                        Console.WriteLine("<---------------------------------");
                        MessageViewModel m = new MessageViewModel
                        {
                            PID = resp.From,
                            Nickname = resp.From,
                            AvatarName = "/avatars/other.gif",
                            IsSelf = false,
                            ShowTime = ts,
                            MessageState = MessageState.Normal,
                            MsgType = MessageType.text,
                            Content = resp.MsgData.ToString()
                        };

                        c.Add(m);

                    }
                }

            }
            catch
            {
                throw;
            }
        }

        public void ShutdownChannel()
        {
            baseHelper.Shutdown(channel);
        }

    }
}
