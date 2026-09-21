using Project_Hrms.Interface.MasterDocuments;
using System.Net;
using System.Net.Mail;

namespace Project_Hrms.Services.MasterDocuments
{
    public class EmailServices : IEmailService
    {
         private readonly IConfiguration conf;
         public EmailServices(IConfiguration conf)
        {
            this.conf = conf;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body, List<string> filePaths)
        {
            using MailMessage mail = new MailMessage();

            mail.From = new MailAddress(conf["EmailSettings:Email"]);

            mail.To.Add(toEmail);
            mail.Subject = subject;
            mail.Body = body;

            foreach (var filePath in filePaths)
            {
                if (System.IO.File.Exists(filePath))
                {
                    mail.Attachments.Add(new Attachment(filePath));
                }
            }

            using SmtpClient client = new SmtpClient("smtp.gmail.com");

            client.Credentials = new NetworkCredential(
                conf["EmailSettings:Email"],
                conf["EmailSettings:Password"]);

            client.Port = 587;
            client.EnableSsl = true;

            await client.SendMailAsync(mail);
        }
    
    }
}
