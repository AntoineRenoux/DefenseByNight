using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Entities;
using DefenseByNight.API.Data.Interfaces;
using DefenseByNight.API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DefenseByNight.API.Data.Repository
{
    public class TraductionRepository : ITraductionRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TraductionRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<TraductionDto>> GetAll(int lcid)
        {
            var trad = await _context.Traductions.Where(x => x.LCID == lcid).ToListAsync();

            return _mapper.Map<List<TraductionDto>>(trad);
        }

        public async Task<TraductionDto> GetTranslate(TraductionDto model)
        {
            var trad = await _context.Traductions.FirstOrDefaultAsync(tr => tr.Key == model.Key && tr.LCID == model.LCID);
            return _mapper.Map<TraductionDto>(trad);
        }
    }
}