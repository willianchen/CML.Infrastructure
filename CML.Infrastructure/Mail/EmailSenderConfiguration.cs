using CML.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Mail
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：EmailSenderConfiguration.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：EmailSenderConfiguration
    /// 创建标识：cml 2017/7/3 15:38:03
    /// </summary>
    public abstract class EmailSenderConfiguration : IEmailSenderConfiguration
    {
        public string DefaultFromAddress
        {
            get
            {
                return ConfigUtil.GetValue(EmailSenderSetting.DefaultFromAddress);
            }
        }

        public string DefaultFromDisplayName
        {
            get
            {
                return ConfigUtil.GetValue(EmailSenderSetting.DefaultFromDisplayName);
            }
        }
    }
}
