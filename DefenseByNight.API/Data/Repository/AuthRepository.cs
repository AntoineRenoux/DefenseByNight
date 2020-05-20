using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Identities;
using DefenseByNight.API.Data.Interfaces;
using DefenseByNight.API.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DefenseByNight.API.Data.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public AuthRepository(UserManager<User> userManager, IMapper mapper, DataContext context)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
        }

        public async Task<UserRegisterDto> RegisterAsync(UserRegisterDto newUser)
        {
            var user = _mapper.Map<User>(newUser);

            var result = await _userManager.CreateAsync(user, newUser.Password);

            var res = await _context.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName);

            return _mapper.Map<UserRegisterDto>(res);
        }

        public async Task<bool> UserExists(UserRegisterDto newUser) {
            return await _context.Users.AnyAsync(u => u.UserName == newUser.UserName);
        }

        public async Task<bool> EmailExists(UserRegisterDto newUser) {
            return await _context.Users.AnyAsync(u => u.Email == newUser.Email);
        }
    }
}