using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Interfaces.IGameRepository;
using DefenseByNight.API.Dtos.GameDto;
using Microsoft.EntityFrameworkCore;

namespace DefenseByNight.API.Data.Repository.GameRepository
{
    public class ClanRepository: IClanRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ClanRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ClanDto>> GetAllAsync()
        {
            var clans = await _context.Clans
                        .Include(d => d.Disciplines)
                        .ToListAsync();

            return _mapper.Map<List<ClanDto>>(clans);
        }
    }
}