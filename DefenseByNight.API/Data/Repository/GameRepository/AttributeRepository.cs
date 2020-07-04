using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Interfaces.IGameRepository;
using DefenseByNight.API.Dtos.GameDto;
using Microsoft.EntityFrameworkCore;

namespace DefenseByNight.API.Data.Repository.GameRepository
{
    public class AttributeRepository: IAttributeRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AttributeRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AttributeDto>> GetAllAsync()
        {
            var attributes = await _context.Attributes
                            .Include(f => f.Focus)
                            .ToListAsync();
            
            return _mapper.Map<List<AttributeDto>>(attributes);
        }
    }
}