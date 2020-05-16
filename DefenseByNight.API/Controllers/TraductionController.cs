using System.Collections.Generic;
using System.Globalization;
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

        private const int LCID = 1036;

        public TraductionController(
            ITraductionRepository traductionRepository,
            IMapper mapper
            )
        {
            _mapper = mapper;
            _traductionRepository = traductionRepository;

        }

        [HttpGet]
        public async Task<IActionResult> Get(int lcid = LCID)
        {
            var tradDto = await _traductionRepository.GetAll(lcid);

            if (tradDto != null)
                return Ok(_mapper.Map<List<TraductionViewModel>>(tradDto));

            return BadRequest("Traductions failed");
        }

        [HttpGet("translate")]
        public async Task<IActionResult> Translate(string key)
        {
            var modelDto = new TraductionDto() { Key = key, LCID = LCID };
            var trad = await _traductionRepository.GetTranslate(modelDto);
            return Ok(_mapper.Map<TraductionViewModel>(trad));
        }
    }
}