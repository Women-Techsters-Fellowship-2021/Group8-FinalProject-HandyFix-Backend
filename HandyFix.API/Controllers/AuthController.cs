using HandyFix.BusinessLogic;
using HandyFix.Models.DTO;
using HandyFix.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;

namespace Handyfix.API.Controllers
{
    //[EnableCors("ReactPolicy")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthentication _authentication;

        public AuthController(IAuthentication authentication)
        {
            _authentication = authentication;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequest userRequest)
        {
            try
            {
                return Ok(await _authentication.Login(userRequest));
            }
            catch (AccessViolationException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterationRequest registerationRequest)
        {
            try
            {
                var result = await _authentication.Register(registerationRequest);
            
                return Created("", result);
            }
            catch(MissingFieldException msex)
            {
                return BadRequest(msex.Message);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetUserEmail")]
        public async Task<IActionResult> GetUserEmail([FromQuery] UserEmailRequest userEmailRequest)
        {
            try
            {
                var user = await _authentication.GetUserEmail(userEmailRequest);
                if (!(user.Data == null))
                {
                    return Ok(user);
                }
                return NotFound(user);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
