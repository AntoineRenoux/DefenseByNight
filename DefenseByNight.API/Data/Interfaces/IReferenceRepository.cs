using System.Threading.Tasks;
using DefenseByNight.API.Dtos;

namespace DefenseByNight.API.Data.Interfaces
{
    public interface IReferenceRepository
    {
        Task<ReferenceDto> GetReference(string key);
    }
}