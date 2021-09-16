//using HandyFix.Models.DTOS;
using System.Threading.Tasks;
using HandyFix.Models;

namespace HandyFix.BusinessLogic
{
    public interface IAuthentication
    {
        Task<UserResponse> Login(UserRequest userRequest);
        Task<UserResponse> Register(RegisterationRequest registerationRequest);
    }
}