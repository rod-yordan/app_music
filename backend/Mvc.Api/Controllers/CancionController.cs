using DtoModel.Cancion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.Bussnies.Cancion;

namespace Mvc.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CancionController : ControllerBase
    {
        private readonly ICancionBussnies _cancionBussnies;

        public CancionController(ICancionBussnies cancionBussnies)
        {
            _cancionBussnies = cancionBussnies;
        }

        [HttpGet]
        public async Task<ActionResult<List<CancionDto>>> GetAll()
        {
            var list = await _cancionBussnies.GetAll();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CancionDto>> GetById(int id)
        {
            var cancion = await _cancionBussnies.GetById(id);

            if (cancion == null)
                return NotFound(new { message = "Canción no encontrada" });

            return Ok(cancion);
        }

        [HttpPost]
        public async Task<ActionResult<CancionDto>> Create([FromBody] CancionDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cancion = await _cancionBussnies.Create(request);

            return CreatedAtAction(nameof(GetById), new { id = cancion.Id }, cancion);
        }

        [HttpPut]
        public async Task<ActionResult<CancionDto>> Update([FromBody] CancionDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var cancion = await _cancionBussnies.Update(request);

            if (cancion == null)
                return NotFound(new { message = "Canción no encontrada" });

            return Ok(cancion);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var cancion = await _cancionBussnies.GetById(id);

            if (cancion == null)
                return NotFound(new { message = "Canción no encontrada" });

            await _cancionBussnies.Delete(id);

            return NoContent();
        }
    }
}