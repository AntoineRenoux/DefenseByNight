using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Identities;
using DefenseByNight.API.Data.Interfaces;
using DefenseByNight.API.Dtos;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DefenseByNight.API.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly DataContext _context;
        private readonly IPhotoRepository _photoRepository;

        public UserRepository(IMapper mapper, UserManager<User> userManager, DataContext context, IPhotoRepository photoRepository)
        {
            _mapper = mapper;
            _userManager = userManager;
            _context = context;
            _photoRepository = photoRepository;
        }

        public async Task<UserDto> GetUserAsync(string userId)
        {
            var user = await _context.Users
                        .Where(u => u.Id == userId)
                        .Include(u => u.Photo)
                        .FirstOrDefaultAsync();

            var userDto = _mapper.Map<UserDto>(user);

            if (userDto.Photo == null)
            {
                userDto.Photo = await _photoRepository.GetPhotoAsync(1);
            }

            return userDto;
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
                user.BirthDate = userDto.BirthDate;

                await _userManager.SetEmailAsync(user, userDto.Email);
                await _userManager.SetPhoneNumberAsync(user, userDto.PhoneNumber);
                await _userManager.SetUserNameAsync(user, userDto.UserName);

                await _context.SaveChangesAsync();

                return await GetUserAsync(userDto.Id);
            }

            return null;
        }
    }
}