//using HandyFix.Models.DTO;
using HandyFix.Models.DTO.Mappings;
using HandyFix.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HandyFix.BusinessLogic
{
    public class UserSystem : IUserSystem
    {
        private readonly UserManager<User> _userManager;

        public UserSystem(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> Update(string userId, UpdateUserRequest updateUser)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return true;
                }

                string errors = string.Empty;

                foreach (var error in result.Errors)
                {
                    errors += error.Description + Environment.NewLine;
                }

                throw new MissingMemberException(errors);
            }

            throw new ArgumentException("Resource not found");
        }

        public async Task<bool> DeleteUser(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return true;
                }

                string errors = string.Empty;

                foreach (var error in result.Errors)
                {
                    errors += error.Description + Environment.NewLine;
                }

                throw new MissingMemberException(errors);
            }
            throw new ArgumentException("Resource not found");

        }


        public async Task<UserResponse> GetUser(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                return UserMappings.GetUserResponse(user);
            }
            throw new ArgumentException("Resource not found");

        }
    }
}
