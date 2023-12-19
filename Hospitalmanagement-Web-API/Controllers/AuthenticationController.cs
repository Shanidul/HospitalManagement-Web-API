using Hospitalmanagement_Web_API.ViewModels.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hospitalmanagement_Web_API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        UserManager<IdentityUser> _userManager;
        IConfiguration _configuration;
        public AuthenticationController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegistrationViewModel registrationViewModel)
        {
            var userExists = await _userManager.FindByEmailAsync(registrationViewModel.Email);
            if (userExists != null)
            {
                return StatusCode(500, new { ErrorMessege = "User already exist!!" });
            }
            IdentityUser identityUser = new IdentityUser
            {
                Email = registrationViewModel.Email,
                UserName = registrationViewModel.Email
            };
            var user = await _userManager.CreateAsync(identityUser, registrationViewModel.Password);
            if (user.Succeeded)
            {
                return Ok("Registration is SuccessFull .");
            }
            else
            {

                return StatusCode(500, new { ErrorMessage = "User registraion is not done. Please contact administrator" });
            }
        }
        [HttpPost]
        [Route("/api/authentication/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
        {
            var userExists = await _userManager.FindByNameAsync(loginViewModel.Username);

            if (userExists != null && await _userManager.CheckPasswordAsync(userExists, loginViewModel.Password))
            {

                List<Claim> userClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,userExists.UserName)

                };


                var signInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

                var jwtSecurityToken = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(1),
                    claims: userClaims,
                    signingCredentials: new SigningCredentials(signInKey, SecurityAlgorithms.HmacSha256)
                    );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    expiration = jwtSecurityToken.ValidTo
                });

            }

            return Unauthorized("Invalid Credentials");
        }
    }
}
