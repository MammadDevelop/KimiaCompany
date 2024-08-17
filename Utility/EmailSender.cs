using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class EmailSender
    {
        public static void SendMail(ViewModel.Mail mail)
        {
            var message = new MailMessage
            {
                From = new MailAddress(mail.From),
                To = { mail.To },
                Subject = mail.To,
                Body = mail.Body,
                DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure,
                IsBodyHtml = true
            };
            using (SmtpClient smtpClient = new SmtpClient("webmail.robincode.ir"))
            {
                smtpClient.Credentials = new NetworkCredential(mail.From, mail.Password);
                smtpClient.Port = 25;
                smtpClient.EnableSsl = false;
                smtpClient.Send(message);
            }
        }
    }
}
