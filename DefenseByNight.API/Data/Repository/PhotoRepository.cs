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
            var user = await _userManager.FindByIdAsync(userId);
            var photo = _mapper.Map<Photo>(photoDto);

            if (user == null)
                return null;

            user.Photo = photo;

            await _context.SaveChangesAsync();

            return _mapper.Map<PhotoDto>(photo);
        }

        public async Task<PhotoDto> GetPhotoAsync(int photoId)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(x => x.Id == photoId);

            return _mapper.Map<PhotoDto>(photo);
        }
    }
}