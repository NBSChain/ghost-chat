using FluentValidation;
using System;
using TerzoChat.ViewModel;


/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: TerzoChat.Model.Message
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/10 23:02:31													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace TerzoChat.Data
{
    public class MessageValidator : AbstractValidator<MessageViewModel>
    {
        public static Lazy<MessageValidator> Singleton = new Lazy<MessageValidator>();

        public MessageValidator()
        {

        }
    }
}
