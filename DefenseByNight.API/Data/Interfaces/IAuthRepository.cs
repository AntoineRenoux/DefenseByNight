using System.Collections.Generic;
using System.Threading.Tasks;
using DefenseByNight.API.Dtos;

namespace DefenseByNight.API.Data.Interfaces
{
    public interface IAuthRepository
    {
         Task<UserDto> RegisterAsync(UserRegisterDto newUser);
         Task<UserDto> UserExists(UserDto userDto);
         Task<UserDto> EmailExists(UserDto userDto);

         Task<UserDto> LoginAsync(UserLoginDto newUser);
         Task<IList<string>> GetRolesAsync(UserDto userDto);
         Task<bool> ChangePasswordAsync(string userId, UserChangePasswordDto userChangePasswordDto);
    }
}