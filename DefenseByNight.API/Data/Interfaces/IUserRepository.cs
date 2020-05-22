using System.Threading.Tasks;
using DefenseByNight.API.Dtos;

namespace DefenseByNight.API.Data.Interfaces
{
    public interface IUserRepository
    {
        Task<UserDto> GetUserAsync(string userId);

        Task<UserDto> EditUserAsync(UserDto userDto);
    }
}