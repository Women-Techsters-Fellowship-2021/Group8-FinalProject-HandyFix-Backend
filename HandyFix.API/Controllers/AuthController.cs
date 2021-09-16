using HandyFix.BusinessLogic;
using HandyFix.Models.DTO;
using HandyFix.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handyfix.API.Controllers
{
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

        [HttpPost]
        public async Task<IActionResult> Register(RegisterationRequest registerationRequest)
        {
            try
            {
                var result = await _authentication.Register(registerationRequest);
                //return CreatedAtAction(nameof(GetProduct), new { Id = result.Id }, result);
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
    }
}