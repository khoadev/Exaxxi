using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Exaxxi.Common
{
    class Mailer
    {

        public void SendMail(string mailName, string to_email, string subject, string body)
        {
            
            var message = new MimeMessage();
            var builder = new BodyBuilder();
            builder.Attachments.Add(@"\Project\Exaxxi\Exaxxi\Exaxxi\wwwroot\images\2pH0xs.jpg");

            
            message.From.Add(new MailboxAddress(mailName, "tdd3107973@gmail.com"));
            message.To.Add(new MailboxAddress("Hi", to_email));
            message.Subject = subject;
            
            message.Body = new TextPart("plain")
            {
                Text = body
            };
            // Now we just need to set the message body and we're done
            message.Body = builder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("tdd3107973@gmail.com", "serqltuuwlbddnhb");
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
