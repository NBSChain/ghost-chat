using System.Collections.Generic;
using TerzoChat.Peer;
using System.Collections.ObjectModel;


/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoChat.Data
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/10 20:58:16													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoChat.Data
{
    public class ContactService : IStorage<ContactViewModel>
    {
        public IEnumerable<ContactViewModel> GetList()
        {
            List<ContactViewModel> lst = new List<ContactViewModel>(); 
            for(int a = 0; a < 5; a++)
            {
                ContactViewModel m =
                    new ContactViewModel {
                        Nickname = "NBS" + a.ToString(),
                        Password = "12pw234x"
                        //AvatarImg = "/avatars/fuchen200.png"
                    };

                lst.Add(m);
            }
            return lst;
        }
    }
}
