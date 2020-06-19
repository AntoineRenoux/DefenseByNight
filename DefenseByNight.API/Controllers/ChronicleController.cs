using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Interfaces;
using DefenseByNight.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DefenseByNight.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChronicleController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IChroniclesRepository _chroniclesRepository;
        public ChronicleController(IMapper mapper, IChroniclesRepository chroniclesRepository)
        {
            _mapper = mapper;
            _chroniclesRepository = chroniclesRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _chroniclesRepository.GetAllAsync();
            return Ok(_mapper.Map<List<ChronicleViewModel>>(result));
        }
    }
}