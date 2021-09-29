using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HandyFix.Models;
using HandyFix.DataAcccess;
using HandyFix.BusinessLogic;
using Microsoft.AspNetCore.Identity;

namespace HandyFix.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ResetPasswordController : ControllerBase
    {
        
    }
} 