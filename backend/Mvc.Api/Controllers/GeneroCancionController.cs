using DtoModel.GeneroCancion;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mvc.Bussnies.GeneroCancion;

namespace Mvc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroCancionController : ControllerBase
    {
        private readonly IGeneroCancionBussnies _bussnies;

        public GeneroCancionController(IGeneroCancionBussnies bussnies)
        {
            _bussnies = bussnies;
        }

        [HttpGet]
        public async Task<ActionResult<List<GeneroCancionDto>>> Get()
        {
            return Ok(await _bussnies.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GeneroCancionDto>> GetById(int id)
        {
            GeneroCancionDto? genero = await _bussnies.GetById(id);

            if (genero == null)
            {
                return NotFound(new { message = "Género no encontrado" });
            }

            return Ok(genero);
        }
    }
}