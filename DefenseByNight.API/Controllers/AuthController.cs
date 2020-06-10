using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Dtos;
using DefenseByNight.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Collections.Generic;
using DefenseByNight.API.Data.Interfaces;

namespace DefenseByNight.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthController(IMapper mapper, IAuthRepository authRepository, IConfiguration config)
        {
            _mapper = mapper;
            _authRepository = authRepository;
            _config = config;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(UserForLoginViewModel userForLoginViewModel)
        {
            var userDto = _mapper.Map<UserLoginDto>(userForLoginViewModel);
            var user = await _authRepository.LoginAsync(userDto);

            if (user == null)
                return Unauthorized("ERR_USERNAME_PASSWORD_DONT_EXISTS");

            else
            {
                var appUser = _mapper.Map<UserDto>(user);
                return Ok(new
                {
                    token = GenerateJwtToken(appUser).Result,
                    appUser
                });
            }

        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(UserForRegisterViewModel userForRegisterViewModel)
        {
            var userDto = _mapper.Map<UserDto>(userForRegisterViewModel);
            var userToRegisterDto = _mapper.Map<UserRegisterDto>(userForRegisterViewModel);

            var today = DateTime.Today;
            var age = today.Year - userForRegisterViewModel.BirthDate.Year;
            if (userForRegisterViewModel.BirthDate > today.AddYears(-age))
                age--;

            if (await _authRepository.UserExists(userDto) != null)
            {
                return Unauthorized("ERR_USERNAME_EXISTS");
            }

            if (await _authRepository.EmailExists(userDto) != null)
            {
                return Unauthorized("ERR_EMAIL_EXISTS");
            }

            if (age < 18)
            {
                return Unauthorized("ERR_MAJORITY_REQUIRED");
            }

            var result = await _authRepository.RegisterAsync(userToRegisterDto);

            return Ok(result);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> ChangePasswordAsync(string userId, [FromBody]UserChangePasswordViewModel changePasswordViewModel)
        {
            if(userId != User.FindFirst(ClaimTypes.NameIdentifier).Value)
                return Unauthorized();

            if (changePasswordViewModel.NewPassword != changePasswordViewModel.ConfirmPassword)
                return BadRequest("ERR_CONFIRMPASSWORD_MATCH");

            var success = await _authRepository.ChangePasswordAsync(userId, _mapper.Map<UserChangePasswordDto>(changePasswordViewModel));

            if (!success)
                return BadRequest();

            return Ok("SUCCESS_CHANGE_PASSWORD");

        }

        #region Private
        private async Task<string> GenerateJwtToken(UserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _authRepository.GetRolesAsync(user);

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