using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Interfaces;
using DefenseByNight.API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DefenseByNight.API.Data.Repository
{
    public class DisciplineRepository : IDisciplineRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _map;

        public DisciplineRepository(DataContext context, IMapper map)
        {
            _map = map;
            _context = context;
        }
        public async Task<List<DisciplineDto>> GetDisciplinesAsync()
        {
            var disciplines = await _context.Disciplines
                            .Include(p => p.Powers)
                            .ThenInclude(f => f.Focus)
                            .ToListAsync();

            var disciplinesDto = _map.Map<List<DisciplineDto>>(disciplines);
            return disciplinesDto;
        }
    }
}