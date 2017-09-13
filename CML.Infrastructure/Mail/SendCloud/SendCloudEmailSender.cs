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
        private readonly ILog _logger;
        public SendCloudEmailSender(ISendCloudEmailSenderConfiguration configuaration, ILogFactory loggerFactory) : base(configuaration)
        {
            _configuaration = configuaration;
            _logger = loggerFactory.Create(GetType().FullName);
        }

        public NameValueCollection PostEmail()
        {
            NameValueCollection dicParam = new NameValueCollection();
            dicParam.Add("apiUser", _configuaration.ApiUser);
            dicParam.Add("apiKey", _configuaration.ApiKey);
            dicParam.Add("from", _configuaration.DefaultFromAddress);
            dicParam.Add("fromName", _configuaration.DefaultFromDisplayName);
            return dicParam;
        }

        /// <summary>
        /// 发送邮件，不支持抄送
        /// </summary>
        /// <param name="mail"></param>
        protected override void SendEmail(MailInfo mail)
        {
            var dic = PostEmail();
            dic.Add("to", mail.To);
            dic.Add("subject", mail.Subject);
            dic.Add("html", mail.Body);
            //暂不支持抄送
            
            var res = HttpUtil.RequestTest(_configuaration.SendUrl, dic, mail.AttachmentFile);
            _logger.Debug(res);
        }

        protected override async Task SendEmailAsync(MailInfo mail)
        {
            var dic = PostEmail();
            dic.Add("to", mail.To);
            dic.Add("subject", mail.Subject);
            dic.Add("html", mail.Body);

            HttpUtil.RequestStr(_configuaration.SendUrl, dic);

        }
    }
}
