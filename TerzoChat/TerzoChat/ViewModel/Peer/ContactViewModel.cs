using FluentValidation;
using TerzoChat.Data;

/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoChat.Peer
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/10 20:13:36													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoChat.Peer
{
    public class ContactViewModel : ValidObservableObject<ContactViewModel>
    {
        public ContactViewModel() : base(ContactValidator.Singleton.Value, ViewModelState.Valid){  }

        private string _nickname;

        public string Nickname { get => _nickname; set => Set(ref _nickname, value); }

        private string _pw;

        public string Password { get => _pw; set => Set(ref _pw, value); }

        private string _avatarImg;

        public string AvatarImg { get => _avatarImg; set => Set(ref _avatarImg, value); }
    }
}
