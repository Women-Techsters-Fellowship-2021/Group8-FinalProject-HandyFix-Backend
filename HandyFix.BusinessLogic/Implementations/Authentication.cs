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
    public class Authentication : IAuthentication
    {
        private readonly UserManager<User> _userManager;
        //private readonly ITokenGenerator _tokenGenerator;

        public Authentication(UserManager<User> userManager)
        {
            _userManager = userManager;
           // _tokenGenerator = tokenGenerator;
        }

        public async Task<UserResponse> Login(UserRequest userRequest)
        {
            User user = await _userManager.FindByEmailAsync(userRequest.Email);
            if (user != null)
            {
                if (await _userManager.CheckPasswordAsync(user, userRequest.Password))
                {
                    var response =  UserMappings.GetUserResponse(user);
                    //response.Token = await _tokenGenerator.GenerateToken(user);

                    return response;
                }

                throw new AccessViolationException("Invalid credentials");
            }
            throw new AccessViolationException("Invalid credentials");
        }


        public async Task<UserResponse> Register(RegisterationRequest registerationRequest)
        {
            User user = UserMappings.GetUser(registerationRequest);

            IdentityResult result = await _userManager.CreateAsync(user, registerationRequest.Password);
            if (result.Succeeded)
            {
                return UserMappings.GetUserResponse(user);
            }

            string errors = string.Empty;

            foreach (var error in result.Errors)
            {
                errors += error.Description + Environment.NewLine;
            }

            throw new MissingFieldException(errors);
        }
    }
}
