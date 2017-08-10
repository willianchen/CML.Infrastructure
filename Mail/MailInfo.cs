using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Mail
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：MailInfo.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：邮件发送类
    /// 创建标识：cml 2017/7/5 10:15:17
    /// </summary>
    public class MailInfo
    {
        /// <summary>
        /// Init
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public MailInfo(string from, string to, string subject, string body,bool isBodyHtml = true)
        {
            From = from;
            To = to;
            Subject = subject;
            Body = body;
            IsBodyHtml = isBodyHtml;
        }

        public string From { get; set; }

        public string FromAddress { get; set; }

        public string To { get; set; }

        public string[] CC { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string[] AttachmentFile { get; set; }

        public bool IsBodyHtml { get; set; }
    }
}
