using VGProducts.Business.Interface;
using VGProducts.Service.Interface;

namespace VGProducts.Service.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly IEmailBusiness emailBusiness;

        public EmailService(IEmailBusiness emailBusiness)
        {
            this.emailBusiness = emailBusiness;
        }
        public async Task SendEmail(string toEmail, string subject, string body)
        {
            await emailBusiness.SendEmail(toEmail, subject, body);
        }
    }
}
