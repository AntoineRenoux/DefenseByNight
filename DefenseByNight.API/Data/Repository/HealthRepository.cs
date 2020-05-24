using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Entities;
using DefenseByNight.API.Data.Interfaces;
using DefenseByNight.API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DefenseByNight.API.Data.Repository
{
    public class HealthRepository: IHealthRepository
    {
        private DataContext _context;
        private IMapper _mapper;

        public HealthRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<HealthDto> GetHealthForUserAsync(string userId)
        {
            var user = await _context.Users.Where(u => u.Id == userId)
                                    .Include(u => u.Health)
                                    .FirstOrDefaultAsync();

            if (user == null || user.Health == null)
                return new HealthDto();


            var health = await _context.Health
                                .Where(h => h.Id == user.Health.Id)
                                .FirstOrDefaultAsync();

            return _mapper.Map<HealthDto>(health);
        }

        public async Task<HealthDto> AddHealthAsync(string userId, HealthDto healthDto)
        {
            var user = await _context.Users.Where(u => u.Id == userId)
                                    .Include(u => u.Health)
                                    .FirstOrDefaultAsync();

            if (user == null)
                return new HealthDto();

            if (user.Health == null)
            {
                var newHealth = _mapper.Map<Health>(healthDto);
                await _context.Health.AddAsync(newHealth);
                _context.SaveChanges();
                user.Health = newHealth;
            }
            else 
            {
                user.Health.Allergies = healthDto.Allergies;
                user.Health.ContactLastName = healthDto.ContactLastName;
                user.Health.ContactFirstName = healthDto.ContactFirstName;
                user.Health.ContactLastName = healthDto.ContactLastName;
                user.Health.PhoneNumber = healthDto.PhoneNumber;
            }

            await _context.SaveChangesAsync();

            return await GetHealthForUserAsync(userId);
        }
    }
}