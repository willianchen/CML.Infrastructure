using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CML.Infrastructure.Mail.Smtp
{
    public interface ISmtpEmailSender : IEmailSender
    {
        SmtpClient BuildClient();
    }
}
