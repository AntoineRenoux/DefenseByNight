using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Entities;
using DefenseByNight.API.Data.Identities;
using DefenseByNight.API.Data.Interfaces;
using DefenseByNight.API.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DefenseByNight.API.Data.Repository
{
    public class PhotoRepository : IPhotoRepository
    {
        private DataContext _context;
        private UserManager<User> _userManager;
        private IMapper _mapper;

        public PhotoRepository(DataContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<PhotoDto> AddPhotoAsync(string userId, PhotoDto photoDto)
        {
            var currentUser = await _context.Users
                                .Where(u => u.Id == userId)
                                .Include(i => i.Photo)
                                .FirstOrDefaultAsync();

            var photo = _mapper.Map<Photo>(photoDto);

            if (currentUser == null)
                return null;

            if (currentUser.Photo != null)
            {
                _context.Photos.Remove(currentUser.Photo);
            }

            currentUser.Photo = photo;

            await _context.SaveChangesAsync();

            return _mapper.Map<PhotoDto>(photo);
        }

        public async Task<PhotoDto> GetPhotoAsync(int photoId)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(x => x.Id == photoId);

            return _mapper.Map<PhotoDto>(photo);
        }

        public async Task<int> DeletePhotoAsync(string userId, int photoId)
        {
             var photo = await _context.Photos.FirstOrDefaultAsync(p => p.Id == photoId);
            _context.Photos.Remove(photo);

            return await _context.SaveChangesAsync();
        }
    }
}