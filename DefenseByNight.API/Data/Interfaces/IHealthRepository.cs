using System.Threading.Tasks;
using DefenseByNight.API.Dtos;

namespace DefenseByNight.API.Data.Interfaces
{
    public interface IHealthRepository
    {
         Task<HealthDto> GetHealthForUserAsync(string userId);
         Task<HealthDto> AddHealthAsync(string userId, HealthDto healthDto);
    }
}