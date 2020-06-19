using System;
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

        [HttpGet("GetXp")]
        public async Task<IActionResult> GetXp(string chronicleKey)
        {
            var chronicle = await _chroniclesRepository.GetByKeyAsync(chronicleKey);
            if (chronicle == null)
                return BadRequest("ERR_CHRONICLE_DOESNT_EXISTS");

            int baseXp = chronicle.BaseExperience;
            int xpPerMonth = chronicle.ExperiencePerMonth;

            int monthsApart = Math.Abs(12 * (chronicle.StartDate.Year - DateTime.Now.Year) + chronicle.StartDate.Month - DateTime.Now.Month);

            return Ok(baseXp + xpPerMonth * monthsApart);
        }
    }
}