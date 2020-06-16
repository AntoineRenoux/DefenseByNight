using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Interfaces;
using DefenseByNight.API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DefenseByNight.API.Data.Repository
{
    public class ReferenceRepository : IReferenceRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReferenceRepository(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ReferenceDto> GetReference(string key)
        {
            var reference = await _context.References.FirstOrDefaultAsync(r => r.Key == key);
            return _mapper.Map<ReferenceDto>(reference);
        }
    }
}