using CML.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Mail.SendCloud
{
    /// <summary>
    /// Copyright (C) 2017 cml 版权所有。
    /// 类名：SendCloudEmailSenderConfiguration.cs
    /// 类属性：公共类（非静态）
    /// 类功能描述：SendCloudEmailSenderConfiguration
    /// 创建标识：cml 2017/7/4 10:09:30
    /// </summary>
   public class SendCloudEmailSenderConfiguration : EmailSenderConfiguration, ISendCloudEmailSenderConfiguration
    {
        public string SendUrl
        {
            get
            {
                return ConfigUtil.GetValue(EmailSenderSetting.SendCloud.SendUrl);
            }
        }

        public string ApiUser
        {
            get
            {
                return ConfigUtil.GetValue(EmailSenderSetting.SendCloud.ApiUser);
            }
        }


        public string ApiKey
        {
            get
            {
                return ConfigUtil.GetValue(EmailSenderSetting.SendCloud.ApiKey);
            }
        }


    }
}
