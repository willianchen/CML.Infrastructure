using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using CML.Infrastructure.Extension;
using System.Net;
using System.IO;

namespace CML.Infrastructure.Mail.Smtp
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：SmtpEmailSender.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：SmtpEmailSender
    /// 创建标识：cml 2017/7/3 16:16:46
    /// </summary>
    public class SmtpEmailSender : EmailSenderBase, ISmtpEmailSender
    {
        private ISmtpEmailSenderConfiguration _configuration;
        private readonly ILog _logger;  
        public SmtpEmailSender(ISmtpEmailSenderConfiguration configuration, ILogFactory loggerFactory) : base(configuration)
        {
            this._configuration = configuration;
            _logger = loggerFactory.Create(GetType().FullName);
        }

        public SmtpClient BuildClient()
        {
            var host = _configuration.Host;
            var port = _configuration.Port;
            var smtpClient = new SmtpClient(host, port);
            try
            {
                if (_configuration.EnableSsl)
                    smtpClient.EnableSsl = true;
                if (_configuration.UseDefaultCredentials)
                    smtpClient.UseDefaultCredentials = true;
                else
                {
                    smtpClient.UseDefaultCredentials = false;
                    var userName = _configuration.UserName;
                    if (!userName.IsNullOrEmpty())
                    {
                        var password = _configuration.Password;
                        var domain = _configuration.Domain;
                        smtpClient.Credentials = !domain.IsNullOrEmpty()
                            ? new NetworkCredential(userName, password, domain)
                             : new NetworkCredential(userName, password);
                    }
                }
                return smtpClient;
            }
            catch
            {
                smtpClient.Dispose();
                throw;
            }
        }

        /// <summary>
        /// 验证邮件内容
        /// </summary>
        /// <param name="mail"></param>
        protected virtual void NormalizeMail(MailMessage mail)
        {
            if (mail.From == null || mail.From.Address.IsNullOrEmpty())
            {
                mail.From = new MailAddress(
                    _configuration.DefaultFromAddress,
                    _configuration.DefaultFromDisplayName,
                    Encoding.UTF8
                    );
            }
        }

        protected override void SendEmail(MailInfo mail)
        {
            try
            {
                MailMessage mailMsg = new MailMessage(mail.From, mail.To, mail.Subject, mail.Body) { IsBodyHtml = mail.IsBodyHtml };
                NormalizeMail(mailMsg);
                if (mail.CC.IsNotNull() && mail.CC.Length > 0)
                {
                    foreach (var t in mail.CC.ToList())
                    {
                        mailMsg.CC.Add(t);
                    }
                }
                if (mail.AttachmentFile.IsNotNull() && mail.AttachmentFile.Length > 0)
                {
                    foreach (var t in mail.AttachmentFile)
                    {
                        FileStream fs = File.OpenRead(t);
                        Attachment data = new Attachment(fs, Path.GetFileName(t));
                        mailMsg.Attachments.Add(data);
                    }
                }
                using (var smtpClient = BuildClient())
                {
                    smtpClient.Send(mailMsg);
                }
            }
            catch (Exception ex)
            {
                _logger.Error("发送邮件异常", ex);
            }
        }

        protected override async Task SendEmailAsync(MailInfo mail)
        {
            MailMessage mailMsg = new MailMessage(mail.From, mail.To, mail.Subject, mail.Body) { IsBodyHtml = mail.IsBodyHtml };
            NormalizeMail(mailMsg);
            using (var smtpClient = BuildClient())
            {
                await smtpClient.SendMailAsync(mailMsg);
            }
        }
    }
}
