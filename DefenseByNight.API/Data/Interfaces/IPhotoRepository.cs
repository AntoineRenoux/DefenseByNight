using System.Threading.Tasks;
using DefenseByNight.API.Dtos;

namespace DefenseByNight.API.Data.Interfaces
{
    public interface IPhotoRepository
    {
         Task<PhotoDto> AddPhotoAsync(string userId, PhotoDto photoModel);
         Task<PhotoDto> GetPhotoAsync(int photoId);
    }
}