using HandyFix.Models;
using System.Threading.Tasks;

namespace HandyFix.BusinessLogic
{
    public interface ITokenGenerator
    {
        Task<string> GenerateToken(User user);
    }
}