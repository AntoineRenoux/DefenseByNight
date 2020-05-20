using System.Threading.Tasks;
using DefenseByNight.API.Dtos;

namespace DefenseByNight.API.Data.Interfaces
{
    public interface IAuthRepository
    {
         Task<UserRegisterDto> RegisterAsync(UserRegisterDto newUser);
         Task<bool> UserExists(UserRegisterDto newUser);
         Task<bool> EmailExists(UserRegisterDto newUser);
    }
}