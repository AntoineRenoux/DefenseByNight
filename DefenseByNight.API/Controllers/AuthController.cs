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
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;

        public AuthController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration config, IAuthRepository authRepository)
        {
            _config = config;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _authRepository = authRepository;
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
            var userToRegisterDto = _mapper.Map<UserRegisterDto>(userForRegisterViewModel);

            if(await _authRepository.UserExists(userToRegisterDto)){
                return BadRequest("ERR_USERNAME_EXISTS");
            }

            if(await _authRepository.EmailExists(userToRegisterDto)){
                return BadRequest("ERR_EMAIL_EXISTS");
            }

            var result = await _authRepository.RegisterAsync(userToRegisterDto);

            return Ok(result);
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