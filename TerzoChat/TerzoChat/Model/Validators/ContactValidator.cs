using FluentValidation;
using System;



/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoChat.Peer
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/10 20:29:08													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoChat.Peer
{
    class ContactValidator : AbstractValidator<ContactViewModel>
    {
        public static Lazy<ContactValidator> Singleton = new Lazy<ContactValidator>(() => new ContactValidator());

        private ContactValidator()
        {
           
        }
    }
}
