using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGProducts.Service.Interface
{
    public interface IEmailService
    {
        Task SendEmail(string toEmail, string subject, string body);
    }
}
