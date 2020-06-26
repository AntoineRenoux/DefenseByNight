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
    public class ClanController: ControllerBase
    {
        private readonly IClanRepository _clanRepository;
        private readonly IMapper _mapper;

        public ClanController(IClanRepository clanRepository, IMapper mapper)
        {
            _clanRepository = clanRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clan = await _clanRepository.GetAllAsync();

            return Ok(_mapper.Map<List<ClanCharacterCreationViewModel>>(clan));
        }
    }
}