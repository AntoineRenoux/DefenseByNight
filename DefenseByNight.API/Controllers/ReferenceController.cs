using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Interfaces;
using DefenseByNight.API.Helpers.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DefenseByNight.API.Controllers
{
    [ApiController]
    [Route("api/Reference")]
    [AllowAnonymous]
    public class ReferenceController : ControllerBase
    {
        private readonly IReferenceRepository _referenceRepository;
        private readonly IMapper _mapper;

        public ReferenceController(IReferenceRepository referenceRepository, IMapper mapper)
        {
            _referenceRepository = referenceRepository;
            _mapper = mapper;
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetReference(string key)
        {
            var reference = await _referenceRepository.GetReference(key);

            if (reference != null)
                return Ok(reference);

            return BadRequest();
        }
    }
}