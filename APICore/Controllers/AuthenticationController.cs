
using ApiCore.Models;
using ApiCore.Models.ActionModel;
using ApiCore.Models.DAL;
using ApiCore.Models.DAL.IService;
using ApiCore.Models.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        IUserService _userService;
        public AuthenticationController(IUserService userService)
        {
            _userService = userService;

        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Post(LoginRequest request)
        {
            LoginResponse response = await _userService.LoginAsync(request);
            return Ok(response);
        }
        
    }
}
