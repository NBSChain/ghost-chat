using System;
using System.Windows.Markup;



/**
 * ┌───────────────────────────────────────────────────────────────────────┐
 * │Project	: Naruto.WPF2Icon
 * │ 
 * │Comment	:
 * │
 * │Version	: V1.0.0.0
 * │Author	: lanbery
 * │CreatTime	: 2019/3/1 0:00:12													
 * ├───────────────────────────────────────────────────────────────────────┤
 * │Copyright © NBS-Tech Team 2019.All rights reserved.
 * └───────────────────────────────────────────────────────────────────────┘
 */
namespace Naruto.WPF2Icon
{
    public partial class IconExtension : MarkupExtension
    {
        public IconExtension()
        {

        }

        public IconExtension(FontAweIcons icon)
        {
            Icon = icon;
        }

        [ConstructorArgument("icon")]
        public FontAweIcons Icon { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return ((char)Icon).ToString();
        }
    }
}
