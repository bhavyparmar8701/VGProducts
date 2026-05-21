using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VGProducts.Business.Interface
{
    public interface IEmailBusiness
    {
        Task SendEmail(string toEmail, string subject, string body);
    }
}
