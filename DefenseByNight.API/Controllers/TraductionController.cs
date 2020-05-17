using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Interfaces;
using DefenseByNight.API.Dtos;
using DefenseByNight.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DefenseByNight.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class TraductionController : ControllerBase
    {
        private readonly ITraductionRepository _traductionRepository;
        private readonly IMapper _mapper;
        private readonly Dictionary<string, int> dictLang = new Dictionary<string, int>{
           {"en", 2057},
           {"fr", 1036}
        };
        public TraductionController(
            ITraductionRepository traductionRepository,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _traductionRepository = traductionRepository;

        }

        [HttpGet]
        public async Task<IActionResult> Get(string lang = "fr")
        {
            var trad = await _traductionRepository.GetAll(dictLang.FirstOrDefault(l => l.Key == lang).Value);

            if (trad != null)
                return Ok(trad);

            return BadRequest("Traductions failed");
        }
    }
}