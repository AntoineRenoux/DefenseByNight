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
    public class ArchetypeController: ControllerBase
    {
        private readonly IArchetypeRepository _archetypeRepository;
        private readonly IMapper _mapper;

        public ArchetypeController(IArchetypeRepository archetypeRepository, IMapper mapper)
        {
            _archetypeRepository = archetypeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var archetypes = await _archetypeRepository.GetAllAsync();
            return Ok(_mapper.Map<List<ArchetypeViewModel>>(archetypes));
        }
    }
}