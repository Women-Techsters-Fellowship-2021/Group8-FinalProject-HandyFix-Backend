using HandyFix.Models;
using System.Threading.Tasks;

namespace HandyFix.BusinessLogic
{
    public interface IUserSystem
    {
        Task<bool> DeleteUser(string userId);
        Task<UserResponse> GetUser(string userId);
        Task<bool> Update(string userId, UpdateUserRequest updateUser);
    }
}