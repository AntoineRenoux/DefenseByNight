using System.Threading.Tasks;
using AutoMapper;
using DefenseByNight.API.Data.Interfaces;
using DefenseByNight.API.Dtos;
using DefenseByNight.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace DefenseByNight.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController: ControllerBase
    {
        private IMapper _mapper;
        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserAsync(string userId) 
        {
            var user = await _userRepository.GetUserAsync(userId);
            if(user != null)
                return Ok(_mapper.Map<UserViewModel>(user));
            return BadRequest("ERR_USER_DOES_NOT_EXIST");
        }

        [HttpPost("edituser")]
        public async Task<IActionResult> EditUserAsync(UserViewModel userModel) 
        {
            var userDto = _mapper.Map<UserDto>(userModel);
            var newUser = await _userRepository.EditUserAsync(userDto);

            if (newUser == null) {
                return BadRequest("ERR_USER_DOESNT_EXISTS");
            }

            return Ok(_mapper.Map<UserViewModel>(newUser));
        }
    }
}