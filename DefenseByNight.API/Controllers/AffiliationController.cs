using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Interfaces.IGameRepository;
using DefenseByNight.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DefenseByNight.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AffiliationController : ControllerBase
    {
        private readonly IAffilationRepository _affiliationRepository;
        private readonly IMapper _mapper;

        public AffiliationController(IAffilationRepository affiliationRepository, IMapper mapper)
        {
            _affiliationRepository = affiliationRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_mapper.Map<List<AffiliateViewModel>>(await _affiliationRepository.GetAffiliations()));
        }
    }
}