using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Repository.Interface;

namespace VGProducts.Repository.Implementation
{
    public class EmailRepository : IEmailRepository
    {
        private readonly string _email = "viralgajera477@gmail.com";
        private readonly string _password = "erexxahudfbbnzzj";

    

        public async Task SendEmail(string toEmail, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(_email, _password),
                    EnableSsl = true,
                };

                var mail = new MailMessage
                {
                    From = new MailAddress(_email),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                };

                mail.To.Add(toEmail);

                await smtpClient.SendMailAsync(mail);

                Console.WriteLine(" Email Sent Successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine(" Email Failed: " + ex.Message);
            }
        }
    }
    
}
