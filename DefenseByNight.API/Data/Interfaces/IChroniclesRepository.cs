using System.Collections.Generic;
using System.Threading.Tasks;
using DefenseByNight.API.Dtos;

namespace DefenseByNight.API.Data.Interfaces
{
    public interface IChroniclesRepository
    {
         Task<List<ChronicleDto>> GetAllAsync();
    }
}