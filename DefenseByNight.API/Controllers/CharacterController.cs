using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Interfaces.IGameRepository;
using DefenseByNight.API.Dtos.GameDto;
using DefenseByNight.API.Helpers.Enums;
using DefenseByNight.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DefenseByNight.API.Controllers
{
    [ApiController]
    [Route("api/{userId}/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly IMapper _mapper;
        public CharacterController(IMapper mapper, ICharacterRepository characterRepository)
        {
            _mapper = mapper;
            _characterRepository = characterRepository;
        }

        [HttpGet(Name="GetById")]
        public async Task<IActionResult> GetById(string userId, string characterKey)
        {
            if (userId != User.FindFirst(ClaimTypes.NameIdentifier).Value || User.FindFirst(ClaimTypes.Role).Value == EnumRoles.ORGANIZER)
                return Unauthorized();

            var character = await _characterRepository.GetById(characterKey);

            return Ok(_mapper.Map<CharacterViewModel>(character));
        }

        [HttpPost]
        public async Task<IActionResult> Create(string userId, CharacterViewModel model)
        {
            if (userId != User.FindFirst(ClaimTypes.NameIdentifier).Value || User.FindFirst(ClaimTypes.Role).Value == EnumRoles.ORGANIZER)
                return Unauthorized();

            var newCharacter = _mapper.Map<CharacterDto>(model);

            var result = await _characterRepository.Create(userId, newCharacter);
            return CreatedAtRoute("GetById", new { userId = userId, characterKey = result.CharacterKey }, result);
        }
    }
}