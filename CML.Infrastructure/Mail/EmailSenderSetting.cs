using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Mail
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：EmailSenderSetting.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：EmailSenderSetting
    /// 创建标识：cml 2017/7/3 15:39:36
    /// </summary>
    public class EmailSenderSetting
    {
        public const string DefaultFromAddress = "MAIL_FROM";

        public const string DefaultFromDisplayName = "MAIL_FROM_DISPLAY_NAME";

        /// <summary>
        /// SMTP related email settings.
        /// </summary>
        public static class Smtp
        {
            public const string Host = "SMTP_HOST";

            public const string Port = "SMTP_PORT";

            public const string UserName = "SMTP_USERNAME";

            public const string Password = "SMTP_PASSWORD";

            public const string Domain = "SMTP_DOMAIN";

            public const string EnableSsl = "SMTP_ENABLESSL";

            public const string UseDefaultCredentials = "SMTP_USEDEFAULTCREDENTIALS";
        }

        public static class SendCloud
        {
            public const string ApiUser = "SENDCLOUD_APIUSER";

            public const string ApiKey = "SENDCLOUD_KEY";

            public const string SendUrl = "SENDCLOUD_SENDURL";
        }
    }
}
