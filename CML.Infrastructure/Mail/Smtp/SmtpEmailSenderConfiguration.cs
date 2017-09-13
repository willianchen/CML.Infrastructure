using CML.Infrastructure.Utils;
using System;
using CML.Infrastructure.Extension;

namespace CML.Infrastructure.Mail.Smtp
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：SmtpEmailSenderConfiguration.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：SmtpEmailSenderConfiguration
    /// 创建标识：cml 2017/7/3 15:48:51
    /// </summary>
    public class SmtpEmailSenderConfiguration : EmailSenderConfiguration, ISmtpEmailSenderConfiguration
    {
        public string Host
        {
            get
            {
                return ConfigUtil.GetValue(EmailSenderSetting.Smtp.Host);
            }
        }

        public int Port
        {
            get
            {
                return ConfigUtil.GetValue(EmailSenderSetting.Smtp.Port).ToInt32(0);
            }
        }

        public string UserName
        {
            get
            {
                return ConfigUtil.GetValue(EmailSenderSetting.Smtp.UserName);
            }
        }

        public string Password
        {
            get
            {
                return ConfigUtil.GetValue(EmailSenderSetting.Smtp.Password);
            }
        }

        public string Domain
        {
            get
            {
                return ConfigUtil.GetValue(EmailSenderSetting.Smtp.Domain);
            }
        }

        public bool EnableSsl
        {
            get
            {
                return ConfigUtil.GetValue(EmailSenderSetting.Smtp.EnableSsl).ToBool();
            }
        }

        public bool UseDefaultCredentials
        {
            get
            {
                return ConfigUtil.GetValue(EmailSenderSetting.Smtp.UseDefaultCredentials).ToBool();
            }
        }

    }
}
