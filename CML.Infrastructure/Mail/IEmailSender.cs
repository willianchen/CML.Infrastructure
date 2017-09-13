using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Mail
{
    /// <summary>
    /// IEmailSender
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// 发送一封邮件
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        /// <returns></returns>
        Task SendAsync(string to, string subject, string body, string[] cc = null, string[] files = null, bool isBodyHtml = true);

        /// <summary>
        /// 发送一封邮件
        /// </summary>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        void Send(string to, string subject, string body, string[] cc = null, string[] files = null, bool isBodyHtml = true);


        /// <summary>
        /// 发送一封邮件
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        /// <returns></returns>
        Task SendAsync(string from, string to, string subject, string body, string[] cc, string[] files = null, bool isBodyHtml = true);

        /// <summary>
        /// 发送一封邮件
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isBodyHtml"></param>
        void Send(string from, string to, string subject, string body, string[] cc, string[] files = null, bool isBodyHtml = true);
    }
}
