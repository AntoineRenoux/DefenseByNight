using System.Threading.Tasks;
using DefenseByNight.API.Dtos.GameDto;

namespace DefenseByNight.API.Data.Interfaces.IGameRepository
{
    public interface ICharacterRepository
    {
        Task<CharacterDto> GetById(string characterKey);
        Task<CharacterDto> Create(string userId, CharacterDto model);
    }
}