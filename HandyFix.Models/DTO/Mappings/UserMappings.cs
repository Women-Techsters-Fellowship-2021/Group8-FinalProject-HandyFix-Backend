using HandyFix.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HandyFix.Models.DTO.Mappings
{
    public class UserMappings
    {
        public static UserResponse GetUserResponse(User user)
        {
            return new UserResponse
            {
                Email = user.Email,
                Id = user.Id
            };
        }

        public static User GetUser(RegisterationRequest request)
        {
            return new User
            {
                Email = request.Email,
                UserName = string.IsNullOrWhiteSpace(request.UserName) ? request.Email : request.UserName,
            };
        }
    }
}
