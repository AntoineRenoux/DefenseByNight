using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Identities;
using DefenseByNight.API.Data.Interfaces;
using DefenseByNight.API.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DefenseByNight.API.Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly SignInManager<User> _signInManager;

        public AuthRepository(UserManager<User> userManager, IMapper mapper, DataContext context, IConfiguration config, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _config = config;
            _signInManager = signInManager;
        }

        public async Task<UserDto> LoginAsync(UserLoginDto newUser) 
        {
            var user = await _userManager.FindByNameAsync(newUser.Username);

            if (user != null)
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, newUser.Password, false);

                if (result.Succeeded)
                {
                    return _mapper.Map<UserDto>(user);
                }
                else {
                    return null;
                }
            }
            else {
                return null;
            }
        }

        public async Task<UserDto> RegisterAsync(UserRegisterDto newUser)
        {
            var user = _mapper.Map<User>(newUser);

            var result = await _userManager.CreateAsync(user, newUser.Password);

            var res = await _context.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);

            return _mapper.Map<UserDto>(res);
        }

        public async Task<UserDto> UserExists(UserDto newUser) {
            var user = await _userManager.FindByNameAsync(newUser.UserName);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> EmailExists(UserDto newUser) {
            var user = await _userManager.FindByEmailAsync(newUser.Email);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<IList<string>> GetRolesAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            return await _userManager.GetRolesAsync(user);
        }
    }
}