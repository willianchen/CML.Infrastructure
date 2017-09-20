using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Mail.SendCloud
{
    /// <summary>
    /// Copyright (C) cml 版权所有。
    /// 类名：MailConfig.cs
    /// 类功能描述：MailConfig
    /// 创建标识：cml 2017/9/19 15:30:07
    /// </summary>
    public class MailConfig
    {
        public string apiUser { get; set; }
        public string apiKey { get; set; }
        public string from { get; set; }
        public string fromName { get; set; }
        public string to { get; set; }
        public string subject { get; set; }
        public string html { get; set; }

    }
}
