using CML.Infrastructure.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Mail
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：EmailSenderBase.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：EmailSenderBase
    /// 创建标识：cml 2017/7/3 13:49:04
    /// </summary>
    public abstract class EmailSenderBase : IEmailSender
    {
        private readonly IEmailSenderConfiguration _configuration;

        protected EmailSenderBase(IEmailSenderConfiguration configuaraion)
        {
            _configuration = configuaraion;
        }

        public void Send(string to, string subject, string body, string[] cc = null, string[] files = null, bool isBodyHtml = true)
        {
            Send(_configuration.DefaultFromAddress, to, subject, body, cc, files, isBodyHtml);
        }

        public void Send(string from, string to, string subject, string body, string[] cc = null, string[] files = null, bool isBodyHtml = true)
        {
            var mail = new MailInfo(from, to, subject, body, isBodyHtml) { CC = cc, AttachmentFile = files };
            SendEmail(mail);
        }


        public Task SendAsync(string to, string subject, string body, string[] cc, string[] files, bool isBodyHtml = true)
        {
            return SendAsync(_configuration.DefaultFromAddress, to, subject, body, cc, files, isBodyHtml);
        }

        public Task SendAsync(string from, string to, string subject, string body, string[] cc, string[] files, bool isBodyHtml = true)
        {
            var mail = new MailInfo(from, to, subject, body, isBodyHtml) { CC = cc, AttachmentFile = files };
            //NormalizeMail(mail);
            return SendEmailAsync(mail);
        }



        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        protected abstract Task SendEmailAsync(MailInfo mail);

        /// <summary>
        /// 发送一封邮件
        /// </summary>
        /// <param name="mail"></param>
        protected abstract void SendEmail(MailInfo mail);
    }
}
