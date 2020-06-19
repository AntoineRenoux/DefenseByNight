using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Interfaces;
using DefenseByNight.API.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DefenseByNight.API.Data.Repository
{
    public class ChroniclesRepository : IChroniclesRepository
    {
        private readonly DataContext context;
        private readonly IMapper _mapper;

        public ChroniclesRepository(DataContext context, IMapper mapper)
        {
            this.context = context;
            _mapper = mapper;
        }

        public async Task<List<ChronicleDto>> GetAllAsync()
        {
            var chronicles = await context.Chronicles.ToListAsync();
            return _mapper.Map<List<ChronicleDto>>(chronicles);
        }
    }
}