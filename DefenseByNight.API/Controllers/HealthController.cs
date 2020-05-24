using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Interfaces;
using DefenseByNight.API.Dtos;
using DefenseByNight.API.Helpers.Enums;
using DefenseByNight.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DefenseByNight.API.Controllers
{
    [ApiController]
    [Route("api/user/{userId}/[controller]")]
    public class HealthController: ControllerBase
    {
        private IUserRepository _userRepository;
        private IHealthRepository _healthRepository;
        private IMapper _mapper;

        public HealthController(IUserRepository userRepository, IHealthRepository healthRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _healthRepository = healthRepository;
            _mapper = mapper;
        }

        [HttpGet(Name="GetHealthUser")]
        public async Task<IActionResult> GetHealthByUserAsync(string userId)
        {
            if (userId != User.FindFirst(ClaimTypes.NameIdentifier).Value || User.FindFirst(ClaimTypes.Role).Value == EnumRoles.ORGANIZER)
                return Unauthorized();

            var health = await _healthRepository.GetHealthForUserAsync(userId);

            return Ok(_mapper.Map<HealthAddViewModel>(health));
        }

        [HttpPost]
        public async Task<IActionResult> AddHealthAsync(string userId, HealthAddViewModel model)
        {
            if (userId != User.FindFirst(ClaimTypes.NameIdentifier).Value || User.FindFirst(ClaimTypes.Role).Value == EnumRoles.ORGANIZER)
                return Unauthorized();

            var healthDto = _mapper.Map<HealthDto>(model);

            healthDto = await _healthRepository.AddHealthAsync(userId, healthDto);

            if (healthDto != null)
                return CreatedAtRoute("GetHealthUser", new { userId = userId }, healthDto);

            return BadRequest();
        }
    }
}