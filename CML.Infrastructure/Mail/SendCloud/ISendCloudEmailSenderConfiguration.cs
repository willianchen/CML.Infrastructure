using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Mail.SendCloud
{

    public interface ISendCloudEmailSenderConfiguration : IEmailSenderConfiguration
    {
        string SendUrl { get; }
        string ApiUser { get; }
        string ApiKey { get; }

    }
}
