using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Identities;
using DefenseByNight.API.Data.Interfaces;
using DefenseByNight.API.Dtos;
using Microsoft.AspNetCore.Identity;

namespace DefenseByNight.API.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private IMapper _mapper;
        private readonly UserManager<User> _userManager;

        private readonly DataContext _context;

        public UserRepository(IMapper mapper, UserManager<User> userManager, DataContext context)
        {
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
        }

        public async Task<UserDto> GetUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> EditUserAsync(UserDto userDto)
        {
            var user = await _userManager.FindByIdAsync(userDto.Id);

            if (user != null)
            {
                user.FirstName = userDto.FirstName;
                user.LastName = userDto.LastName;
                user.City = userDto.City;
                user.Address = userDto.Address;
                user.ZipCode = userDto.Zipcode;
                user.BirthDate = user.BirthDate;

                await _userManager.SetEmailAsync(user, userDto.Email);
                await _userManager.SetPhoneNumberAsync(user, userDto.PhoneNumber);
                await _userManager.SetUserNameAsync(user, userDto.UserName);

                await _context.SaveChangesAsync();

                return _mapper.Map<UserDto>(user);
            }

            return null;
        }
    }
}