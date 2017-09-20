using CML.Infrastructure.Extension;
using CML.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Mail.SendCloud
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：SendCloudEmailSender.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：SendCloudEmailSender
    /// 创建标识：cml 2017/7/4 11:24:42
    /// </summary>
    public class SendCloudEmailSender : EmailSenderBase
    {
        private ISendCloudEmailSenderConfiguration _configuaration;
        private MailConfig mailConfig;
        //private readonly ILog _logger;
        public SendCloudEmailSender(ISendCloudEmailSenderConfiguration configuaration) : base(configuaration)
        {
            _configuaration = configuaration;

        }

        public MailConfig PostEmail()
        {
            var mailConfig = new MailConfig();
            mailConfig.apiUser = _configuaration.ApiUser;
            mailConfig.apiKey = _configuaration.ApiKey;
            mailConfig.from = _configuaration.DefaultFromAddress;
            mailConfig.fromName = _configuaration.DefaultFromDisplayName;
            return mailConfig;
        }

        /// <summary>
        /// 发送邮件，不支持抄送
        /// </summary>
        /// <param name="mail"></param>
        protected override void SendEmail(MailInfo mail)
        {

            var mailConfig = PostEmail();
            mailConfig.to = mail.To;
            mailConfig.subject = mail.Subject;
            mailConfig.html = mail.Body;

            //暂不支持抄送
            LogUtil.Debug(mailConfig.ToJson());
            var res = HttpClientUtil.PostRequestAsync<MailConfig>(_configuaration.SendUrl, mailConfig).Result;
                //HttpUtil.RequestStr(_configuaration.SendUrl, dic, mail.AttachmentFile);
            LogUtil.Debug(res);
        }

        protected override async Task SendEmailAsync(MailInfo mail)
        {
          


        }
    }
}
