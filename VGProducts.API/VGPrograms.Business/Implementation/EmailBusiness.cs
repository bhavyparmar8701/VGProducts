using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Business.Interface;
using VGProducts.Repository.Interface;

namespace VGProducts.Business.Implementation
{
    public class EmailBusiness : IEmailBusiness
    {
        private readonly IEmailRepository emailRepository;

        public EmailBusiness(IEmailRepository emailRepository) 
        {
            this.emailRepository = emailRepository;
        }
        public async Task SendEmail(string toEmail, string subject, string body)
        {
            await emailRepository.SendEmail(toEmail, subject, body);
        }
    }
}
