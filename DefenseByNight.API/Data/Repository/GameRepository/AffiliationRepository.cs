using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Interfaces.IGameRepository;
using DefenseByNight.API.Dtos.GameDto;
using Microsoft.EntityFrameworkCore;

namespace DefenseByNight.API.Data.Repository.GameRepository
{
    public class AffiliationRepository: IAffilationRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AffiliationRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AffiliateDto>> GetAffiliations()
        {
            var affiList = await _context.Affiliates.ToListAsync();
            return _mapper.Map<List<AffiliateDto>>(affiList);
        }
    }
}