using ApiCore.Models.ActionModel;
using ApiCore.Models.DAL.IService;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiCore.Models.DAL.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<LoginResponse> LoginAsync(LoginRequest model)
        {
            //var existingUser = await _userManager.FindByEmailAsync(model.UserName);
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, lockoutOnFailure: true);
            //var isCorrect = await _userManager.CheckPasswordAsync(existingUser, model.Password);
            if (result.Succeeded)
            {
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name,model.UserName)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("WebCoreApi_2022_FPT_Software"));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddSeconds(30), // 5-10 
                    SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
                };
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var token = jwtTokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = jwtTokenHandler.WriteToken(token);
                return new LoginResponse()
                {
                    Success = true,
                    Token = jwtToken
                };
            }
            return new LoginResponse()
            {
                Success = false,
                Message = "Sai tên đăng nhập hoặc mật khẩu"
            };
        }
    }
}
