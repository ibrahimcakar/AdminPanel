using AdminPanel.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminPanel.Services.Mail
{
    public partial interface IEmailSender
    {
        void SendEmail(EmailAccount emailAccount, EmailProperties emailProperties);
    }
}
