using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Identities;
using DefenseByNight.API.Dtos;
using DefenseByNight.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;

namespace DefenseByNight.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public AuthController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config)
        {
            _config = config;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginViewModel userForLoginViewModel)
        {
            var user = await _userManager.FindByNameAsync(userForLoginViewModel.UserName);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginViewModel.Password, true);

                if (result.Succeeded)
                {
                    var appUser = _mapper.Map<UserDto>(user);
                    return Ok(new
                    {
                        token = GenerateJwtToken(user).Result,
                        appUser
                    });
                }

            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterViewModel userForRegisterViewModel)
        {
            var userToRegister = _mapper.Map<User>(userForRegisterViewModel);

            userToRegister.CreatedDate = DateTime.Now;
            userToRegister.LastActive = null;

            var result = await _userManager.CreateAsync(userToRegister, userForRegisterViewModel.Password);

            if (result.Succeeded)
            {
                var userToReturn = _mapper.Map<UserForRegisterViewModel>(userToRegister);

                return Ok(userToReturn);
            }
            return BadRequest(result.Errors);
        }

        #region Private
        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        #endregion
    }
}