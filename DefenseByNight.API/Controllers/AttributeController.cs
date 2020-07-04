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
    public class AttributeController: ControllerBase
    {
        private readonly IAttributeRepository _attributeRepository;

        private readonly IMapper _mapper;

        public AttributeController(IAttributeRepository attributeRepository, IMapper mapper)
        {
            _attributeRepository = attributeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var attr = await _attributeRepository.GetAllAsync();
            return Ok(_mapper.Map<List<AttributeViewModel>>(attr));
        }
    }
}