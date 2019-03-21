using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pb;


/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoChat.Model
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/22 0:07:10													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoChat.ViewModel
{
    public class MessageModelUtils
    {
        public static MessageViewModel buildSendMessage(
            string content,
            string id,
            bool sendState) 
        {
            MessageViewModel model = new MessageViewModel(true)
            {
                UID = id,
                Content = content,
                Nickname = "Me",
                MessageTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                AvatarName = "/avatars/nbsstar.png",
                MessageState = sendState ? Model.MessageState.Normal : Model.MessageState.Failure,
                MsgType = Model.MessageType.text
            };

            return model;
        }

        public static MessageViewModel buildSendMessage1(
            MessageViewModel m,
            string content,bool sendState)
        {

            MessageViewModel nm = new MessageViewModel()
            {
                Content = content,
                UID = m.UID,
                AvatarName = m.AvatarName,
                MessageTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                MessageState = sendState ? Model.MessageState.Normal : Model.MessageState.Failure,
                Nickname = m.Nickname,
                MsgType = m.MsgType

            };
            return nm;
        }

        public static MessageViewModel buildRecvMessage(SubscribeResponse response)
        {
            MessageViewModel m = new MessageViewModel()
            {
                Content = response.MsgData.ToStringUtf8(),
                UID = response.From,
                AvatarName = "/avatars/nbsstar.png",
                MessageTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                MessageState = Model.MessageState.Normal ,
                Nickname = response.From,
                MsgType = Model.MessageType.text
            };
            return m;
        }
    }
}
