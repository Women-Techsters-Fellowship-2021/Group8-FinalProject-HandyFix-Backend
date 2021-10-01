using HandyFix.Models;
using System.Threading.Tasks;

namespace HandyFix.BusinessLogic
{
    public interface IMailService
    {
        Task SendEmailAsync(EmailRequest emailRequest);
    }
}