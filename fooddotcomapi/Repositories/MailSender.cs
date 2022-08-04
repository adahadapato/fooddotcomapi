using fooddotcomapi.Services;
using System;
using System.Net.Mail;

namespace fooddotcomapi.Repositories
{
    public class MailSender : IMailSender
    {
        private readonly IEmailConfiguration _emailConfiguration;
        public MailSender(IEmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }
        public (bool IsSuccess, string ErrorMessage) SendEmail(string userEmail, string subject, string confirmationLink)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("support@zayun.biz");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = "Confirm your email";
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = confirmationLink;
            mailMessage.Priority = MailPriority.High;




            SmtpClient client = new SmtpClient();

            client.Credentials = new System.Net.NetworkCredential("support@zayun.biz", "Emypat04me#@");
            client.Host = "mail.zayun.biz";
            client.Port = 25;

            try
            {
                client.Send(mailMessage);
                return (true, "Mail sent successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.InnerException.Message);
            }
        }

        public (bool IsSuccess, string ErrorMessage) SendEmail(string senderEmail, string userEmail, string subject, string confirmationLink)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(senderEmail, "Support");
            mailMessage.To.Add(new MailAddress(userEmail));

            mailMessage.Subject = subject;
            mailMessage.IsBodyHtml = true;
            mailMessage.Body = confirmationLink;
            mailMessage.Priority = MailPriority.High;



            SmtpClient client = new SmtpClient();

            client.Credentials = new System.Net.NetworkCredential(senderEmail, _emailConfiguration.SmtpPassword);
            client.Host = _emailConfiguration.SmtpServer;
            client.Port = _emailConfiguration.SmtpPort;
            client.EnableSsl = false;

            //client.EnableSsl = true;

            try
            {
                client.Send(mailMessage);
                return (true, "Mail sent successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.InnerException.Message);
            }
        }
    }
}
