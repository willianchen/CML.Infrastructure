using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Mail
{
    /// <summary>
    /// 邮件发送配置
    /// </summary>
    public interface IEmailSenderConfiguration
    {
        /// <summary>
        /// 发送Email
        /// </summary>
        string DefaultFromAddress { get; }

        /// <summary>
        /// 发送名称
        /// </summary>
        string DefaultFromDisplayName { get; }
    }
}
