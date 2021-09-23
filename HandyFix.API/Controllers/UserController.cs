using HandyFix.BusinessLogic;
using HandyFix.Models.DTO;
using HandyFix.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Handyfix.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserSystem _userSystem;

        public UserController(IUserSystem userSystem)
        {
            _userSystem = userSystem;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser(string userId)
        {
            try
            {
                return Ok(await _userSystem.GetUser(userId));
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPatch("profile")]
        public async Task<IActionResult> Update(UpdateUserRequest updateUserRequest)
        {
            try
            {
                var userId = HttpContext.User.FindFirst(x => x.Type == ClaimTypes.NameIdentifier).Value;

                var result = await _userSystem.Update(userId, updateUserRequest);
                return NoContent();
            }
            catch (MissingMemberException mmex)
            {
                return BadRequest(mmex.Message);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Regular")]
        public async Task<IActionResult> Delete(string userId)
        {
            try
            {
                await _userSystem.DeleteUser(userId);
                return NoContent();
            }
            catch (MissingMemberException mmex)
            {
                return BadRequest(mmex.Message);
            }
            catch (ArgumentException argex)
            {
                return BadRequest(argex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
