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

        public async Task<Dictionary<string, string>> GetAll(int lcid)
        {
            return await _context.Traductions.Where(x => x.LCID == lcid).ToDictionaryAsync(k => k.Key, v => v.Text);
        }
    }
}