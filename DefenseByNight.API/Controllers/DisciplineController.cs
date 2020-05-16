using System.Threading.Tasks;
using DefenseByNight.API.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DefenseByNight.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisciplineController : ControllerBase
    {
        private readonly IDisciplineRepository _disciplines;

        public DisciplineController(IDisciplineRepository disciplines)
        {
            _disciplines = disciplines;
        }

        [HttpGet]
        public async Task<IActionResult> GetDisciplines()
        {
            var disciplines = await _disciplines.GetDisciplinesAsync();

            return Ok(disciplines);
        }


    }
}