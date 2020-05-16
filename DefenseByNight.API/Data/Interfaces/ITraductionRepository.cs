using System.Collections.Generic;
using System.Threading.Tasks;
using DefenseByNight.API.Data.Entities;
using DefenseByNight.API.Dtos;

namespace DefenseByNight.API.Data.Interfaces
{
    public interface ITraductionRepository
    {
        Task<List<TraductionDto>> GetAll(int lcid);

         Task<TraductionDto> GetTranslate(TraductionDto model);
    }
}