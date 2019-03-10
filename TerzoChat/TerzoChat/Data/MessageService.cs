using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerzoChat.ViewModel;

/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoChat.Data
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/10 22:54:55													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoChat.Data
{
    class MessageService : IStorage<MessageViewModel>
    {
        public IEnumerable<MessageViewModel> GetList()
        {
            return initMessagesTest(6);
        }


        private List<MessageViewModel> initMessagesTest(int count)
        {
            string _prex = "NBS_NNS-";
            List<MessageViewModel> lst = new List<MessageViewModel>();
            if (count < 1) return lst;
            for(int a = 0; a < count; a++)
            {
                MessageViewModel m = new MessageViewModel {
                    Nickname = _prex + a.ToString(),
                    Content = "jsdhf哈地方了开始打对方sfdalksdfasdhfadsfads阿斯蒂芬哈市的福克斯回复的",
                    AvatarName = "/avatars/fuchen200.png",
                    ShowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                    IsSelf = a%3 == 0 ? true:false ,
                    MessageState = Model.MessageState.Normal,
                    MsgType = Model.MessageType.text
                };
                lst.Add(m);
            }
            return lst;
        }
    }
}
