using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Interfaces.IGameRepository;
using DefenseByNight.API.Dtos.GameDto;
using Microsoft.EntityFrameworkCore;

namespace DefenseByNight.API.Data.Repository.GameRepository
{
    public class ArchetypeRepository: IArchetypeRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ArchetypeRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ArchetypeDto>> GetAllAsync()
        {
            var result = await _context.Archetypes.ToListAsync();
            return _mapper.Map<List<ArchetypeDto>>(result);
        }

    }
}