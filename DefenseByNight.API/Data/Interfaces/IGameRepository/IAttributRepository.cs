using System.Collections.Generic;
using System.Threading.Tasks;
using DefenseByNight.API.Dtos.GameDto;

namespace DefenseByNight.API.Data.Interfaces.IGameRepository
{
    public interface IAttributeRepository
    {
         Task<List<AttributeDto>> GetAllAsync();
    }
}